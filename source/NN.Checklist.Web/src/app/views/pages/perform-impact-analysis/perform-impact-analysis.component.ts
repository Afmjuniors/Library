import { SelectionModel } from '@angular/cdk/collections';
import { ChangeDetectorRef, Component, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { MatDialog, MatPaginator, MatSort } from '@angular/material';
import { Router } from '@angular/router';
import { Store } from '@ngrx/store';
import { TranslateService } from '@ngx-translate/core';
import { Observable, Subject, Subscription, timer } from 'rxjs';
import { distinctUntilChanged, finalize, skip, takeUntil, tap } from 'rxjs/operators';
import { AuthService, Logout, Permission } from '../../../core/auth';
import { OccurrencesAnalysisDataSource } from '../../../core/auth/_data-sources/occurrencesAnalysis.datasource';
import { Area } from '../../../core/auth/_models/area.model';
import { OccurrenceRecord } from '../../../core/auth/_models/occurenceRecord.model';
import { OccurrenceRecordFilter } from '../../../core/auth/_models/occurrenceRecordFilter.model';
import { Process } from '../../../core/auth/_models/process.model';
import { Status } from '../../../core/auth/_models/status.model';
import { AppService, ParameterService } from '../../../core/auth/_services';
import { AppState } from '../../../core/reducers';
import { LayoutUtilsService, MessageType, QueryResultsModel } from '../../../core/_base/crud';
import { BasePageComponent } from '../BasePage.component';
import { DetailsPerformImpactAnalysisComponent } from './details-perform-impact-analysis/details-perform-impact-analysis.component';
import { NGX_MAT_DATE_FORMATS } from '@angular-material-components/datetime-picker';

import { EmailComponent } from '../../components/email/email.component';
import { IdleService } from '../../../core/_base/layout/services/idle.service';
import { MessageHistoryComponent } from '../../components/message-history/message-history.component';

const DATE_TIME_FORMAT = {
	parse: {
		dateInput: 'YYYY-MM-DD HH:mm:ss',
	},
	display: {
		dateInput: 'YYYY-MM-DD HH:mm:ss',
		monthYearLabel: 'YYYY MMM',
		dateA11yLabel: 'DD',
		monthYearA11yLabel: 'YYYY MMM',
		enableMeridian: true,
		useUtc: true,
	}
};

@Component({
	selector: 'kt-perform-impact-analysis',
	templateUrl: './perform-impact-analysis.component.html',
	styleUrls: ['./perform-impact-analysis.component.scss'],
	providers: [
		{ provide: NGX_MAT_DATE_FORMATS, useValue: DATE_TIME_FORMAT },
	],
})
export class PerformImpactAnalysisComponent extends BasePageComponent implements OnInit {
	// Public properties
	hasUserAccess$: Observable<boolean>;
	hasUserAccessCriar$: Observable<boolean>;
	hasUserAccessEditar$: Observable<boolean>;
	hasUserAccessDesativar$: Observable<boolean>;
	currentUserPermission$: Observable<Permission[]>;

	//timer
	updateTimer: Observable<number> = timer(0, 60000);
	timerSubscription: Subscription;

	// Table fields
	dataSource: OccurrencesAnalysisDataSource;
	displayedColumns = ['selected', 'dateStart', 'dateEnd', 'id', 'process', 'area', 'impactedArea', 'tag', 'description', 'status', 'responsible', 'dateAnalysis', 'messages_history'];
	@ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;
	@ViewChild('sort1', { static: true }) sort: MatSort;

	// Selection
	OccurrencesResult: OccurrenceRecord[] = [];
	buscaForm: FormGroup;
	filter: OccurrenceRecordFilter = new OccurrenceRecordFilter();
	status: Status[] = [];
	processes: Process[] = [];
	areas: Area[] = [];

	typesOccurrences = [
		{ typeOccurrenceId: 0, description: ''},
		{ typeOccurrenceId: 1, description: this.translate.instant('MENU.ALARMS')},
		{ typeOccurrenceId: 2, description: this.translate.instant('MENU.EVENTS')}
	];
	error: Boolean = false;
	mensagemErro: string = '';

	// Screen
	title: string;
	loading: boolean = false;
	process: string;
	area: string;
	typeOccurrence: string

	loadingCSV: boolean = false;
	loadingPDF: boolean = false;

	selection = new SelectionModel<OccurrenceRecord>(true, []);

	//Datetime
	public date: moment.Moment;
	public showSpinners = true;
	public showSeconds = true;
	public touchUi = false;
	public enableMeridian = false;
	public minDate: moment.Moment;
	public maxDate: moment.Moment;
	public stepHour = 1;
	public stepMinute = 1;
	public stepSecond = 1;
	public date2: moment.Moment;

	private unsubscribe: Subject<any>;

	constructor(
		public store: Store<AppState>,
		public router: Router,
		private layoutUtilsService: LayoutUtilsService,
		public dialog: MatDialog,
		private fb: FormBuilder,
		auth: AuthService,
		public app: AppService,
		public translate: TranslateService,
		private detector: ChangeDetectorRef,
		public parameterService: ParameterService,
		public idleService: IdleService,
	) {
		super(auth, store, translate, router, "IMPACT_ANALYSIS", [], "IMPACT_ANALYSIS", parameterService, idleService)
		this.unsubscribe = new Subject();
	}

	ngOnInit() {
		this.loading = true;
		this.title = this.translate.instant("MENU.PERFORM_ANALYSIS");
		this.process = this.translate.instant("MENU.PROCESS");
		this.area = this.translate.instant("MENU.AREA");
		this.typeOccurrence = this.translate.instant("MENU.TYPE_OCCURRENCE");

		this.filter = new OccurrenceRecordFilter();
		this.filter.status = 99;
		this.filter.endDateFilled = true;
		this.filter.assessmentNeeded = true;

		this.loadListProcess();
		this.loadOccurrencesAnalysis();
		//this.checkTimer();
	}

	ngOnDestroy(): void {
		this.subscriptions.forEach(el => el.unsubscribe());

		if (this.timerSubscription) {
			this.timerSubscription.unsubscribe();
		}
	}

	loadOccurrencesAnalysis() {

		this.paginator.page.pipe(
			tap(() => {
				this.loading = true;
				this.loadOccurrences();
			}, error => {
				this.layoutUtilsService.showErrorNotification(error, MessageType.Create);
			}),
			takeUntil(this.unsubscribe),
			finalize(() => {
				this.loading = false;
				this.detector.detectChanges();
			})
		).subscribe();

		//Init DataSource
		this.dataSource = new OccurrencesAnalysisDataSource(this.app, this.store, this.filter);

		const entitiesSubscription = this.dataSource.entitySubject.pipe(
			skip(1),
			distinctUntilChanged()
		).subscribe(res => {
			this.OccurrencesResult = res;
			this.loading = false;
		}, error => {
			this.mensagemErro = '';
			this.error = true;
			this.mensagemErro = error.message;
			this.loading = false;
		});
		this.subscriptions.push(entitiesSubscription);
	}

	loadListProcess() {
		this.app.listProcesses().subscribe(x => {
			this.processes = x;
		},
			error => {
				this.layoutUtilsService.showErrorNotification(error);
			});
	}

	loadListAreas() {
		if(this.filter.process.toString() != '0'){
			this.app.listAreasByProcess(this.filter.process).subscribe(x => {
				this.areas = x;
			},
			error => {
				this.layoutUtilsService.showActionNotification(error);
			});
		} else {
			this.areas = [];
			this.filter.area = null;
			this.detector.detectChanges();
		}
	}

	loadOccurrences() {

		this.app.getAllOccurrencesAnalysis(this.paginator.pageIndex + 1, this.paginator.pageSize, this.filter).subscribe((response: QueryResultsModel) => {
			this.dataSource.paginatorTotalSubject.next(response.rowsCount);
			this.dataSource.entitySubject.next(response.entities);
			this.dataSource.loadingSubject.next(false);
			this.loading = false;
		}, error =>{
			this.dataSource.paginatorTotalSubject.next(0);
			this.dataSource.entitySubject.next(null);
			this.dataSource.loadingSubject.next(false);
			this.loading = false;
		});
	}

	applyFilters() {
		this.loading = true;
		this.paginator.pageIndex = 0;

		if(this.filter.idType == 0){
			this.filter.idType = null;
		}

		if(this.filter.process == 0){
			this.filter.process = null;
		}

		if(this.filter.area == 0){
			this.filter.area = null;
		}

		this.loadOccurrences();
	}

	clearFilter() {
		this.filter = new OccurrenceRecordFilter();
		this.filter.status = 0;
		this.filter.endDateFilled = true;
		this.filter.assessmentNeeded = true;
		this.paginator.pageIndex = 0;
		this.loadOccurrences();
	}

	viewDetails(occurrence) {
		if (occurrence)
		{
			this.selection.clear();
			this.selection.selected.push(occurrence);
		}
		const dialogRef = this.dialog.open(DetailsPerformImpactAnalysisComponent, { width: '80%', data: { occurrences: this.selection.selected } });
		dialogRef.afterClosed().subscribe(res => {
			this.selection.clear();

			this.applyFilters();
		});
	}

	selectOccurrence(data) {
		if (data) {
			let selected = this.selection.selected.findIndex(item => item.occurrenceRecordId == data.occurrenceRecordId && data.impactedAreaId == item['impactedAreaId'])
			selected == -1 ? this.selection.selected.push(data) : this.selection.selected.splice(selected,1);
		}
	}

	isSelected(data) {
		if (data) {
			let selected = this.selection.selected.find(item => item.occurrenceRecordId == data.occurrenceRecordId && data.impactedAreaId == item['impactedAreaId'])
			return selected;
		}
		return false;
	}

	isAllSelected() {
		const numSelected = this.selection.selected.length;
		const numRows = this.dataSource.entitySubject.value.length;
		return numSelected === numRows;
	}

	masterToggle() {
		this.isAllSelected() ?
			this.selection.clear() :
			this.dataSource.entitySubject.forEach(row => {
				row.forEach(occurrence => {
					this.selection.select(occurrence);
				})
			});
	}

	changeProcess() {
		this.areas = [];
		this.loadListAreas();
	}

	sendEmailPdf() {
		var self = this;
		this.dialog.open(EmailComponent, { width: '350px', data: {} }).afterClosed().subscribe(mail => {
			if (mail) {
				self.filter.sendMail = mail;
				this.app.exportOccurrenceAnalysisPdf(self.filter).subscribe(response => {
					this.loadingPDF = false;
					this.layoutUtilsService.showActionNotification(this.translate.instant('AUTH.QA_REPORT.EXPORT_MESSAGE'), MessageType.Create);
				},
					error => {
						this.loadingPDF = false;
						this.layoutUtilsService.showErrorNotification(error);
					})
			}
		});
	}

	sendEmailCSV() {
		var self = this;
		this.dialog.open(EmailComponent, { width: '350px', data: {} }).afterClosed().subscribe(mail => {
			if (mail) {
				self.filter.sendMail = mail;
				this.app.exportOccurrenceAnalysisCSV(self.filter).subscribe(response => {
					this.loadingCSV = false;
					this.layoutUtilsService.showActionNotification(this.translate.instant('AUTH.QA_REPORT.EXPORT_MESSAGE'), MessageType.Create);
				},
					error => {
						this.loadingCSV = false;
						this.layoutUtilsService.showErrorNotification(error);
					})
			}
		});
	}

	viewMessageHistory(id){
		const dialogRef = this.dialog.open(MessageHistoryComponent, {width:'80%', height:'80%', data: { occurrenceRecordId: id } });
		dialogRef.afterClosed().subscribe(res => {

		});
	}

	checkTimer() {
		const subs = this.updateTimer.subscribe(() => {
			this.applyFilters();
		});
		this.timerSubscription = subs;
	}
}

// Angular
import { Component, ChangeDetectionStrategy, OnInit, OnDestroy, ViewChild, ElementRef, ChangeDetectorRef } from '@angular/core';
import { Router } from '@angular/router';
// RxJS
import { Observable, Subscription, merge, fromEvent, of, Subject, timer } from 'rxjs';
// NGRX
import { Store, select } from '@ngrx/store';
// AppState
import { AppState } from '../../../core/reducers';
// Auth
import { Permission, currentUserPermissions, checkHasUserPermission, User, AuthService, Logout } from '../../../core/auth';
import { MatPaginator, MatSort, MatDialog, MatTableDataSource } from '@angular/material';
import { SelectionModel } from '@angular/cdk/collections';
import { debounceTime, distinctUntilChanged, tap, take, delay, skip, takeUntil, finalize } from 'rxjs/operators';
import { QueryParamsModel, LayoutUtilsService, MessageType, QueryResultsModel } from '../../../core/_base/crud';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ColaboradorFiltro } from '../../../core/auth/_models/ColaboradorFiltro.model';
import { BasePageComponent } from '../BasePage.component';
import { TranslateService } from '@ngx-translate/core';
import { OccurrenceRecordFilter } from '../../../core/auth/_models/occurrenceRecordFilter.model';
import { Status } from '../../../core/auth/_models/status.model';
import { AppService, ParameterService } from '../../../core/auth/_services';
import { MAT_MOMENT_DATE_FORMATS, MomentDateAdapter, MAT_MOMENT_DATE_ADAPTER_OPTIONS } from '@angular/material-moment-adapter';
import { DateAdapter, MAT_DATE_FORMATS, MAT_DATE_LOCALE } from '@angular/material/core';
import { NgxMatDateAdapter, NgxMatDateFormats, NGX_MAT_DATE_FORMATS } from '@angular-material-components/datetime-picker';
import { OccurrenceRecord } from '../../../core/auth/_models/occurenceRecord.model';
import { Process } from '../../../core/auth/_models/process.model';
import { Area } from '../../../core/auth/_models/area.model';
import { OccurrencesAnalysisDataSource } from '../../../core/auth/_data-sources/occurrencesAnalysis.datasource';
import { EmailComponent } from '../../components/email/email.component';
import { ImpactAnalysisDetailsComponent } from './impact-analysis-details/impact-analysis-details.component';
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
	selector: 'apta-overview',
	templateUrl: './impact-analysis-overview.component.html',
	styleUrls: ['./impact-analysis-overview.component.scss'],
	providers: [
		{ provide: NGX_MAT_DATE_FORMATS, useValue: DATE_TIME_FORMAT },
	],
})
export class ImpactAnalysisOverviewComponent extends BasePageComponent implements OnInit {

	// Public properties
	hasUserAccess$: Observable<boolean>;
	hasUserAccessCriar$: Observable<boolean>;
	hasUserAccessEditar$: Observable<boolean>;
	hasUserAccessDesativar$: Observable<boolean>;
	currentUserPermission$: Observable<Permission[]>;

	typesOccurrences = [
		{ typeOccurrenceId: 0, description: ''},
		{ typeOccurrenceId: 1, description: this.translate.instant('MENU.ALARMS')},
		{ typeOccurrenceId: 2, description: this.translate.instant('MENU.EVENTS')}
	];
	//timer
	updateTimer: Observable<number> = timer(0, 60000);
	timerSubscription: Subscription;

	// Table fields
	dataSource: OccurrencesAnalysisDataSource;
	displayedColumns = ['dateStart', 'dateEnd', 'id', 'process', 'impactedArea', 'tag', 'description', 'status', 'impact', 'responsible', 'dateAnalysis', 'messages_history'];
	@ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;
	@ViewChild('sort1', { static: true }) sort: MatSort;

	OccurrencesResult: OccurrenceRecord[] = [];
	permiteCriar: Boolean;
	permiteEditar: Boolean;
	permiteDesativar: Boolean;
	buscaForm: FormGroup;
	filter: OccurrenceRecordFilter = new OccurrenceRecordFilter();
	status: string[] = [];
	processes: Process[] = [];
	areas: Area[] = [];
	error: Boolean = false;
	mensagemErro: string = '';

	// Screen
	title: string;
	loading: boolean = false;
	process: string;
	area: string;

	pdfFile;
	csvFile;

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

	loadingCSV: boolean = false;
	loadingPDF: boolean = false;

	typeOccurrence: string;

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

		this.typeOccurrence = this.translate.instant("MENU.TYPE_OCCURRENCE");
	}

	ngOnInit() {
		this.title = this.translate.instant("MENU.OVERVIEW");
		this.process = this.translate.instant("MENU.PROCESS");
		this.area = this.translate.instant("MENU.AREA");

		this.filter = new OccurrenceRecordFilter();
		this.filter.assessmentNeeded = true;
		this.filter.status = 0;

		this.loadListStatus();
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
		this.loading = true;
		this.paginator.page.pipe(
			tap(() => {
				this.loading = true;
				this.loadList();
			}, error => {
				this.layoutUtilsService.showErrorNotification(error.message, MessageType.Create);
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

	loadListStatus() {
		this.status.push("")
		this.status.push(this.translate.instant("ANALISYS_STATUS_APPROVED"))
		this.status.push(this.translate.instant("ANALISYS_STATUS_REPROVED"))
	}

	loadListProcess() {
		this.app.listProcesses().subscribe(x => {
			this.processes = x;
		},
			error => {
				this.layoutUtilsService.showActionNotification(error.message);
			});
	}

	loadListAreas() {
		if(this.filter.process.toString() != '0'){
			this.app.listAreasByProcess(this.filter.process).subscribe(x => {
				this.areas = x;
			},
			error => {
				this.layoutUtilsService.showActionNotification(error.message);
			});
		} else {
			this.areas = [];
			this.filter.area = null;
			this.detector.detectChanges();
		}
	}

	loadList() {
		this.loading = true;
		//this.filter.status = this.filter.status == null ? 0 : this.filter.status;

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

	applyFilter() {
		if (this.filter.statusName == "") {
			this.filter.status = null;
		}

		if (this.filter.statusName == this.translate.instant("ALL")) {
			this.filter.status = null;
		}
		else if (this.filter.statusName == this.translate.instant("ANALISYS_STATUS_APPROVED")) {
			this.filter.status = 3;
		}
		else if (this.filter.statusName == this.translate.instant("ANALISYS_STATUS_REPROVED")) {
			this.filter.status = 4;
		}

		if(this.filter.idType == 0){
			this.filter.idType = null;
		}

		if(this.filter.process == 0){
			this.filter.process = null;
		}

		if(this.filter.area == 0){
			this.filter.area = null;
		}

		this.paginator.pageIndex = 0
		this.loadList();
	}

	clearFilters() {
		this.filter = new OccurrenceRecordFilter();
		this.filter.assessmentNeeded = true;
		this.filter.status = 0;
		this.loadList();
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
						this.layoutUtilsService.showErrorNotification(error.message);
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
						this.layoutUtilsService.showErrorNotification(error.message);
					})
			}
		});
	}

	viewDetails(occurrence) {
		const dialogRef = this.dialog.open(ImpactAnalysisDetailsComponent, { width: '80%', data: { occurrenceId: occurrence['occurrenceRecordId'], impactedAreaId: occurrence['impactedAreaId'] } });
		dialogRef.afterClosed().subscribe(res => {

		});
	}

	changeProcess() {
		this.areas = [];
		this.loadListAreas();
	}

	viewMessageHistory(id){
		const dialogRef = this.dialog.open(MessageHistoryComponent, {width:'80%', height:'80%', data: { occurrenceRecordId: id } });
		dialogRef.afterClosed().subscribe(res => {

		});
	}

	checkTimer() {
		const subs = this.updateTimer.subscribe(() => {
			this.applyFilter();
		});
		this.timerSubscription = subs;
	}

}

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
import { AlarmsDataSource } from '../../../core/auth/_data-sources/alarms.datasource';
import { OccurrenceRecordFilter } from '../../../core/auth/_models/occurrenceRecordFilter.model';
import { Status } from '../../../core/auth/_models/status.model';
import { AppService, ParameterService } from '../../../core/auth/_services';
import { MAT_MOMENT_DATE_FORMATS, MomentDateAdapter, MAT_MOMENT_DATE_ADAPTER_OPTIONS } from '@angular/material-moment-adapter';
import { DateAdapter, MAT_DATE_FORMATS, MAT_DATE_LOCALE } from '@angular/material/core';
import { OccurrenceRecord } from '../../../core/auth/_models/occurenceRecord.model';
import { Process } from '../../../core/auth/_models/process.model';
import { EventsDataSource } from '../../../core/auth/_data-sources/events.datasource';
import { Area } from '../../../core/auth/_models/area.model';
import { EmailComponent } from '../../components/email/email.component';
import { NgxMatDateAdapter, NgxMatDateFormats, NGX_MAT_DATE_FORMATS } from '@angular-material-components/datetime-picker';
import { NGX_MAT_MOMENT_DATE_ADAPTER_OPTIONS } from '@angular-material-components/moment-adapter';

import { NewOccurrenceRecordComponent } from '../../components/new-occurrence-record/new-occurrence-record.component';
import { PdfViewComponent } from '../../components/pdf-view/pdf-view.component';
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
	selector: 'kt-events',
	templateUrl: './events.component.html',
	styleUrls: ['./events.component.scss'],
	providers: [
		{ provide: NGX_MAT_DATE_FORMATS, useValue: DATE_TIME_FORMAT },
	],
})
export class EventsComponent extends BasePageComponent implements OnInit {
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
	dataSource: EventsDataSource;
	displayedColumns = ['date', 'date_start', 'lastCondition', 'process', 'area', 'tag', 'message', 'responsible', 'old_value', 'new_value', 'unit', 'comment', 'manuallyAdded', 'responsibleAdd', 'messages_history'];
	@ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;
	@ViewChild('sort1', { static: true }) sort: MatSort;

	// Selection
	selection = new SelectionModel<OccurrenceRecord>(true, []);
	OccurrencesResult: OccurrenceRecord[] = [];
	permiteCriar: Boolean;
	permiteEditar: Boolean;
	permiteDesativar: Boolean;
	buscaForm: FormGroup;
	filter: OccurrenceRecordFilter = new OccurrenceRecordFilter();
	lastConditions: string[] = [];
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

	loadingPDF: boolean = false;
	loadingCSV: boolean = false;

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

	public createNewEvent: boolean = false;

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
		super(auth, store, translate, router, "EVENTS", ["CREATE_NEW_EVENT_RECORD"], "EVENTS", parameterService, idleService)
		this.createNewEvent = this.readPermission("CREATE_NEW_EVENT_RECORD");
		this.unsubscribe = new Subject();
	}

	ngOnInit() {
		this.title = this.translate.instant("MENU.EVENTS");
		this.process = this.translate.instant("MENU.PROCESS");
		this.area = this.translate.instant("MENU.AREA");
		this.filter.idType = 2;
		this.loadListLastConditions();
		this.loadListProcess();
		this.initEvents();
		//this.checkTimer();
	}

	initEvents() {
		this.loading = true;
		this.paginator.page.pipe(
			tap(() => {
				this.loading = true;
				this.loadEvents();
			}, error => {
				this.loading = false;
				this.layoutUtilsService.showErrorNotification(error.message, MessageType.Create);
			}),
			takeUntil(this.unsubscribe),
			finalize(() => {
				this.loading = false;
				this.detector.detectChanges();
			})
		).subscribe();


		//Init DataSource
		this.dataSource = new EventsDataSource(this.app, this.store);

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

	loadEvents(): void {
		this.loading = true;
		if(this.filter.process == 0 ){
			this.filter.process = null
		}

		if(this.filter.area == 0){
			this.filter.area = null;
		}

		this.app.getAllEvents(this.paginator.pageIndex + 1, this.paginator.pageSize, this.filter).subscribe((response: QueryResultsModel) => {
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

	ngOnDestroy(): void {
		this.subscriptions.forEach(el => el.unsubscribe());

		if (this.timerSubscription) {
			this.timerSubscription.unsubscribe();
		}
	}

	filtrar() {
		this.loading = true;
		this.paginator.pageIndex = 0;
		this.loadEvents();
	}

	limparFiltros() {
		this.filter = new OccurrenceRecordFilter();
		this.filter.idType = 2;
		this.loadEvents();
	}

	loadListLastConditions() {
		this.lastConditions.push("");
		this.lastConditions.push("ALM");
		this.lastConditions.push("ACK");
		this.lastConditions.push("OK");
	}

	loadListProcess() {
		this.app.listProcesses().subscribe(x => {
			this.processes = x;
		},
		error => {
			this.loading = false;
			this.layoutUtilsService.showActionNotification(error.message);
		});
	}

	loadListAreas(processId){
		if(processId != '0'){
			this.app.listAreasByProcess(processId).subscribe(x => {
				this.areas = x;
				this.detector.detectChanges();
			},
			error =>
			{
				this.loading = false;
				this.layoutUtilsService.showErrorNotification(error.message, MessageType.Create);
			});
		} else {
			this.areas = [];
			this.filter.area = null;
			this.detector.detectChanges();
		}
	}


	sendMailPdf() {
		const dialogRef = this.dialog.open(EmailComponent, { width: '350px', data: {} });
		dialogRef.afterClosed().subscribe(mail => {
			if (mail) {
				this.filter.sendMail = mail;
				if (mail != "" && mail != undefined && mail != null) {
					this.loading = true;
					this.app.exportOccurrencesPdf(this.filter).subscribe(x => {
						this.loading = false;
						this.detector.detectChanges();
						this.pdfFile = x;
						this.layoutUtilsService.showActionNotification(this.translate.instant("WAITGENERATION"), MessageType.Create);
					},
					error => {
						this.loading = false;
						this.detector.detectChanges();
						this.layoutUtilsService.showActionNotification(error);
					});
				}
			}
		});
	}

	exportPdf() {
		this.loadingPDF = true;
		this.app.exportOccurrencesPdf(this.filter).subscribe(response => {
			this.viewPdf(response);
			this.loadingPDF = false;
			this.detector.detectChanges();
		},
			error => {
				this.loadingPDF = false;
				this.layoutUtilsService.showErrorNotification(error);
			});
	}

	sendMailCSV() {
		const dialogRef = this.dialog.open(EmailComponent, { width: '350px', data: {} });
		dialogRef.afterClosed().subscribe(res => {
			if (res) {
				this.filter.sendMail = res;
				if (res != "" && res != undefined && res != null) {
					this.loading = true;
					this.loadingCSV = true;
					this.app.ExportOccurrenceCSV(this.filter).subscribe(x => {
						this.loading = false;
						this.detector.detectChanges();
						this.layoutUtilsService.showActionNotification(this.translate.instant("WAITGENERATION"), MessageType.Create);
						this.csvFile = x;
						this.loadingCSV = false;
					},
					error => {
						this.loading = false;
						this.loadingCSV = false;
						this.detector.detectChanges();
						this.layoutUtilsService.showActionNotification(error.error);
					});
				}
			}
		});
	}

	newEvent() {
		const dialogRef = this.dialog.open(NewOccurrenceRecordComponent, { width: '70%', data: { type: 2 } });
		dialogRef.afterClosed().subscribe(res => {
			this.loadEvents();
		});
	}

	viewPdf(item: string) {
		const dialogRef = this.dialog.open(PdfViewComponent, { width: '80%', height: '80%', data: { item: item } });
		dialogRef.afterClosed().subscribe(res => {
		});
	}

	viewMessageHistory(id){
		const dialogRef = this.dialog.open(MessageHistoryComponent, {width:'80%', height:'80%', data: { occurrenceRecordId: id } });
		dialogRef.afterClosed().subscribe(res => {

		});
	}

	checkTimer() {
		const subs = this.updateTimer.subscribe(() => {
			this.filtrar();
		});
		this.timerSubscription = subs;
	}
}

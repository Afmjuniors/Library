// Angular
import { Component, ChangeDetectionStrategy, OnInit, OnDestroy, ViewChild, ElementRef, ChangeDetectorRef } from '@angular/core';
import { Router } from '@angular/router';
// RxJS
import { Observable, Subscription, merge, fromEvent, of, Subject } from 'rxjs';
// NGRX
import { Store, select } from '@ngrx/store';
// AppState
import { AppState } from '../../../core/reducers';
// Auth
import { Permission, currentUserPermissions, checkHasUserPermission, User, AuthService, Logout } from '../../../core/auth';
import { MatPaginator, MatSort, MatDialog, MatTableDataSource } from '@angular/material';
import { debounceTime, distinctUntilChanged, tap, take, delay, skip, takeUntil, finalize } from 'rxjs/operators';
import { QueryParamsModel, LayoutUtilsService, MessageType, QueryResultsModel } from '../../../core/_base/crud';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { BasePageComponent } from '../BasePage.component';
import { TranslateService } from '@ngx-translate/core';
import { AppService, ParameterService } from '../../../core/auth/_services';
import { MAT_MOMENT_DATE_FORMATS, MomentDateAdapter, MAT_MOMENT_DATE_ADAPTER_OPTIONS } from '@angular/material-moment-adapter';
import { EmailComponent } from '../../components/email/email.component';
import {DateAdapter, MAT_DATE_FORMATS, MAT_DATE_LOCALE} from '@angular/material/core';
import { NgxMatDateAdapter, NgxMatDateFormats, NGX_MAT_DATE_FORMATS } from '@angular-material-components/datetime-picker';
import { NGX_MAT_MOMENT_DATE_ADAPTER_OPTIONS } from '@angular-material-components/moment-adapter';
import { AuditTrailDataSource } from '../../../core/auth/_data-sources/audit-trail.datasource';
import { AuditTrailFilter } from '../../../core/auth/_models/auditTrailFilter.model';
import { SystemFunctionality } from '../../../core/auth/_models/systemFunctionality.model';
import { IdleService } from '../../../core/_base/layout/services/idle.service';
import { ViewAnalysisComponent } from '../../components/view-analysis/view-analysis.component';
import { ViewTextComponent } from '../../components/view-text/view-text.component';

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
	selector: 'apta-audit-trail',
	templateUrl: './audit-trail.component.html',
	styleUrls: ['./audit-trail.component.scss'],
	providers: [
		{provide: NGX_MAT_DATE_FORMATS, useValue: DATE_TIME_FORMAT},
	],
})
export class AuditTrailComponent extends BasePageComponent implements OnInit {
	// Public properties
	hasUserAccess$: Observable<boolean>;
	hasUserAccessCriar$: Observable<boolean>;
	hasUserAccessEditar$: Observable<boolean>;
	hasUserAccessDesativar$: Observable<boolean>;
	currentUserPermission$: Observable<Permission[]>;

	// Table fields
	dataSource: AuditTrailDataSource;
	displayedColumns = ['systemRecordId', 'date', 'systemFunctionality', 'user', 'description', 'comments'];
	@ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;

	filter: AuditTrailFilter = new AuditTrailFilter();
	systemFunctionalities: SystemFunctionality[] = [];

	// Screen
	title: string;
	loading: boolean = false;
	errorMessage: string;
	error: boolean = false;

	csvFile;

	document;

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
	)	{
		super(auth, store, translate, router, "AUDIT_TRAIL" ,[], "AUDIT_TRAIL", parameterService, idleService)
		this.unsubscribe = new Subject();
	}

	ngOnInit() {
		this.title  = this.translate.instant("MENU.AUDIT_TRAIL");
		this.listSystemFunctionalities();
		this.initAuditTrail();
	}

	initAuditTrail(){
		this.loading = true;

		this.paginator.page.pipe(
			tap(() => {
				this.idleService.restartTimer();
				this.loading = true;
				this.load();
			}, error => {
				this.layoutUtilsService.showErrorNotification(error, MessageType.Create);
			}),
			takeUntil(this.unsubscribe),
			finalize(() => {
				this.idleService.restartTimer();
				this.loading = false;
				this.detector.detectChanges();
			})
		).subscribe();

		//Init DataSource
		this.dataSource = new AuditTrailDataSource(this.app, this.store);

		const entitiesSubscription = this.dataSource.entitySubject.pipe(
			skip(1),
			distinctUntilChanged()
		).subscribe(res => {
			this.idleService.restartTimer();
			this.loading = false;
		},error =>{
			this.errorMessage = '';
			this.error = true;
			this.errorMessage = error.message;
			this.loading = false;
		});
		this.subscriptions.push(entitiesSubscription);
	}

	load(): void {
		this.loading = true;
		this.idleService.restartTimer();
		if(this.filter.functionalityId == 0){
			this.filter.functionalityId = null;
		}

		this.app.searchAuditTrail(this.paginator.pageIndex + 1, this.paginator.pageSize, this.filter).subscribe((response: QueryResultsModel) => {
			this.dataSource.paginatorTotalSubject.next(response.rowsCount);
			this.dataSource.entitySubject.next(response.entities);
			this.dataSource.loadingSubject.next(false);
			this.loading = false;
		}, error => {
			this.dataSource.paginatorTotalSubject.next(0);
			this.dataSource.entitySubject.next(null);
			this.dataSource.loadingSubject.next(false);
			this.loading = false;
		});
	}

	ngOnDestroy(): void {
		this.subscriptions.forEach(el => el.unsubscribe());
		this.idleService.stopTimer();
	}

	filtrar() {
		this.paginator.pageIndex = 0
		this.load();
	}

	limparFiltros() {
		this.filter = new AuditTrailFilter();
		this.load();
	}

	listSystemFunctionalities(){
		this.idleService.restartTimer();
		this.app.listSystemsFunctionalities().subscribe(x => {
			this.systemFunctionalities = x;
		},
		error =>
		{
			this.layoutUtilsService.showErrorNotification(error.message, MessageType.Create);
		});
	}


	sendEmailCSV(){
		this.idleService.restartTimer();
		const dialogRef = this.dialog.open(EmailComponent, {width:'350px', data: {  } });
		dialogRef.afterClosed().subscribe(res => {
			//this.filter.sendMail = res;
			this.idleService.restartTimer();
			if(res != "" && res != undefined && res != null)
			{
				this.layoutUtilsService.showActionNotification(this.translate.instant("WAITGENERATION"), MessageType.Create);
				this.app.ExportOccurrenceCSV(this.filter).subscribe(x => {
					this.csvFile = x;
				},
				error =>
				{
					this.layoutUtilsService.showErrorNotification(error.message, MessageType.Create);
				});
			}
		});
	}

	exportCSV() {
		this.idleService.restartTimer();
		this.loadingCSV = true;
		this.app.ExportOccurrenceCSV(this.filter).subscribe(
			response => {
				this.download(response);
				this.loadingCSV = false;
				this.detector.detectChanges();
			},
			error => {
				this.loadingCSV = false;
				this.layoutUtilsService.showErrorNotification(error.message, MessageType.Create);
			}
		);
	}

	download(item: string) {
		this.idleService.restartTimer();
		this.loading = true
		this.app.downloadFile(item)
			.subscribe(x => {
				if (x && x.body) {
					var newBlob = new Blob([x.body], { type: x.contentType });

					if (window.navigator && window.navigator.msSaveOrOpenBlob) {
						window.navigator.msSaveOrOpenBlob(newBlob);
						return;
					}

					const data = window.URL.createObjectURL(newBlob);
					this.document = data;

					// this.viewPdf(data);

					var link = document.createElement('a');
					link.href = data;
					link.download = item;
					// this is necessary as link.click() does not work on the latest firefox
					link.dispatchEvent(new MouseEvent('click', { bubbles: true, cancelable: true, view: window }));

					setTimeout(function () {
						// For Firefox it is necessary to delay revoking the ObjectURL
						window.URL.revokeObjectURL(data);
						link.remove();
					}, 100);
				}
				this.loading = false;
				this.idleService.restartTimer();
			},
				error => {
					this.loading = false;
					this.layoutUtilsService.showErrorNotification(error.message, MessageType.Delete, 10000, true, false);
				}
			);

	}

	viewDetails(item){
		const dialogRef = this.dialog.open(ViewTextComponent, {
			minHeight: '300px',
			width: '40%',
			data: {text: item}
		});
		dialogRef.afterClosed().subscribe(res => {
			if(res){

			}
		});
	}
}

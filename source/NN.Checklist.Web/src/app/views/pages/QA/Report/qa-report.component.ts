// Angular
import { Component, ChangeDetectionStrategy, OnInit, OnDestroy, ViewChild, ElementRef, ChangeDetectorRef } from '@angular/core';
import { Router } from '@angular/router';
// RxJS
import { Observable, Subscription, merge, fromEvent, of, Subject } from 'rxjs';
// NGRX
import { Store, select } from '@ngrx/store';
// AppState
import { AppState } from '../../../../core/reducers';
// Auth
import { Permission, currentUserPermissions, checkHasUserPermission, User, AuthService, Logout } from '../../../../core/auth';
import { MatPaginator, MatSort, MatDialog, MatTableDataSource } from '@angular/material';
import { SelectionModel } from '@angular/cdk/collections';
import { debounceTime, distinctUntilChanged, tap, take, delay, skip, takeUntil, finalize } from 'rxjs/operators';
import { QueryParamsModel, LayoutUtilsService, MessageType, QueryResultsModel } from '../../../../core/_base/crud';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ColaboradorFiltro } from '../../../../core/auth/_models/ColaboradorFiltro.model';
import { BasePageComponent } from '../../BasePage.component';
import { TranslateService } from '@ngx-translate/core';
import { OccurrenceRecordFilter } from '../../../../core/auth/_models/occurrenceRecordFilter.model';
import { Status } from '../../../../core/auth/_models/status.model';
import { AppService, ParameterService } from '../../../../core/auth/_services';
import { MAT_MOMENT_DATE_FORMATS, MomentDateAdapter, MAT_MOMENT_DATE_ADAPTER_OPTIONS } from '@angular/material-moment-adapter';
import { DateAdapter, MAT_DATE_FORMATS, MAT_DATE_LOCALE } from '@angular/material/core';
import { OccurrenceRecord } from '../../../../core/auth/_models/occurenceRecord.model';
import { Process } from '../../../../core/auth/_models/process.model';
import { Area } from '../../../../core/auth/_models/area.model';
import { SignatureComponent } from '../../../components/signature/signature.component';
import { EmailComponent } from '../../../components/email/email.component';
import { QAService } from '../../../../../app/core/auth/_services/qa.service';
import { QAReport } from '../../../../core/auth/_models/QAReport.model';
import { QAApprovalReportComponent } from '../../../components/QA/report/qa-approval-report.component';
import { QARecordsDataSource } from '../../../../core/auth/_data-sources/qaRecords.datasource';
import { NgxMatDateAdapter, NgxMatDateFormats, NGX_MAT_DATE_FORMATS } from '@angular-material-components/datetime-picker';


import { PdfViewComponent } from '../../../components/pdf-view/pdf-view.component';
import { QAFilter } from '../../../../core/auth/_models/QAFilter.model';
import { IdleService } from '../../../../core/_base/layout/services/idle.service';

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
	selector: 'apta-qa-report',
	templateUrl: './qa-report.component.html',
	styleUrls: ['./qa-report.component.scss'],
	providers: [
		{ provide: NGX_MAT_DATE_FORMATS, useValue: DATE_TIME_FORMAT },
	],
})
export class QAReportComponent extends BasePageComponent implements OnInit {
	// Table fields
	dataSource: QARecordsDataSource;
	displayedColumns = ['createdDate', 'endDate', 'process', 'area', 'alarms', 'messages', 'date', 'responsible', 'status'];
	@ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;
	@ViewChild('sort1', { static: true }) sort: MatSort;

	stages = [
		{ id: 999, description: '' },
		{ id: 0, description: this.translate.instant("PENDING_IMPACT_ANALYSIS") },
		{ id: 1, description: this.translate.instant("FINISHED_IMPACT_ANALYSIS") },
		{ id: 2, description: this.translate.instant("FINISHED_QA_ANALYSIS") }
	]


	//General vars
	reportData: QAReport;
	status: any = [];

	// Selection control
	SelectedOccurrences: OccurrenceRecord[] = [];
	filter: QAFilter = new QAFilter();
	selectedIds: number[] = [];

	OccurrencesResult: OccurrenceRecord[] = [];

	processes: Process[] = [];
	areas: Area[] = [];
	error: Boolean = false;
	mensagemErro: string = '';

	loading: boolean = false;
	loadingCSV: boolean = false;
	loadingPDF: boolean = false;

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
		public qaService: QAService,
		public app: AppService,
		public translate: TranslateService,
		private detector: ChangeDetectorRef,
		public parameterService: ParameterService,
		public idleService: IdleService,
	) {
		super(auth, store, translate, router, "QA_REPORT", [], "QA_REPORT", parameterService, idleService)
		this.unsubscribe = new Subject();
	}

	ngOnInit() {
		this.reportData = new QAReport();
		var today = new Date();
		var priorDate = new Date(new Date().setDate(today.getDate() - 30));

		this.filter.startDate = priorDate;
		this.filter.endDate = null;
		this.loadListProcess();

		this.loading = true;
		this.paginator.page.pipe(
			tap(() => {
				this.loading = true;
				this.load();
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
		this.dataSource = new QARecordsDataSource(this.qaService, this.store, this.filter);

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

	load() {
		this.loading = true;

		this.qaService.SearchQAData(this.paginator.pageIndex + 1, this.paginator.pageSize, this.filter).subscribe((response: QueryResultsModel) => {
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
		this.paginator.pageIndex = 0;

		if(this.filter.process == 0){
			this.filter.process = null;
		}

		if(this.filter.area == 0){
			this.filter.area = null;
		}

		if(this.filter.status == 999){
			this.filter.status = null;
		}

		this.load();
	}

	clearFilters() {
		this.filter = new OccurrenceRecordFilter();
		this.load();
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

	sendEmailPdf() {
		this.dialog.open(EmailComponent, { width: '350px', data: {} }).afterClosed().subscribe(mail => {
			if (mail) {
				this.loading = true;
				this.filter.sendMail = mail;
				this.qaService.GenerateApprovalPDF(this.filter).subscribe(response => {
					this.loading = false;
					this.detector.detectChanges();
					this.layoutUtilsService.showActionNotification(this.translate.instant('AUTH.QA_REPORT.EXPORT_MESSAGE'), MessageType.Create);
				},
					error => {
						this.layoutUtilsService.showErrorNotification(error);
					})
			}
		});
	}

	exportPdf() {
		this.loadingPDF = true;
		this.qaService.GenerateApprovalPDF(this.filter).subscribe(response => {
			this.viewPdf(response.path);
			this.loadingPDF = false;
			this.detector.detectChanges();
		},
		error => {
			this.loadingPDF = false;
			this.layoutUtilsService.showErrorNotification(error, MessageType.Create);
			});
	}

	viewPdf(item: string) {
		const dialogRef = this.dialog.open(PdfViewComponent, { width: '80%', height: '80%', data: { item: item } });
		dialogRef.afterClosed().subscribe(res => {
		});
	}

	sendEmailCSV() {
		this.dialog.open(EmailComponent, { width: '350px', data: {} }).afterClosed().subscribe(mail => {
			if (mail) {
				this.loading = true;
				this.filter.sendMail = mail;
				this.qaService.GenerateApprovalXLS(this.filter).subscribe(response => {
					this.loading = false;
					this.detector.detectChanges();
					this.layoutUtilsService.showActionNotification(this.translate.instant('AUTH.QA_REPORT.EXPORT_MESSAGE'), MessageType.Create);
				},
				error => {
					this.layoutUtilsService.showErrorNotification(error);
				})
			}
		});
	}

	exportCSV() {
		this.loadingCSV = true;
		this.qaService.GenerateApprovalXLS(this.filter).subscribe(response => {
			this.download(response.path);
			this.loadingCSV = false;
			this.detector.detectChanges();
		},
			error => {
				this.loadingCSV = false;
				this.layoutUtilsService.showErrorNotification(error, MessageType.Create);
			});
	}

	download(item: string) {
		this.loading = true
		this.app.downloadFile(item)
			.subscribe(x => {
				var newBlob = new Blob([x.body], { type: x.contentType });

				if (window.navigator && window.navigator.msSaveOrOpenBlob) {
					window.navigator.msSaveOrOpenBlob(newBlob);
					return;
				}

				const data = window.URL.createObjectURL(newBlob);

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
				this.loading = false
			},
				error => {
					this.loading = false;
					this.layoutUtilsService.showErrorNotification(error, MessageType.Delete, 10000, true, false);
				});

	}

	changeProcess() {
		this.areas = [];
		this.loadListAreas();
	}
}

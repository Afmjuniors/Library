import { Component, OnInit, ViewChild, ChangeDetectorRef, OnDestroy } from '@angular/core';
import { BasePageComponent } from '../../BasePage.component';
import { Observable, merge, Subject, Subscription, timer } from 'rxjs';
import { Permission, AuthService } from '../../../../core/auth';
import { MatPaginator, MatSort, MatDialog } from '@angular/material';
import { LayoutUtilsService, MessageType, QueryParamsModel, QueryResultsModel } from '../../../../core/_base/crud';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { select, Store } from '@ngrx/store';
import { AppState } from '../../../../core/reducers';
import { Router } from '@angular/router';
import { TranslateService } from '@ngx-translate/core';
import { currentUser, Logout, User } from '../../../../../app/core/auth';

import { SignatureComponent } from '../../../components/signature/signature.component';
import { QAApprovedDataSource } from '../../../../core/auth/_data-sources/qaApproved.datasource';
import { QAWaitingInspectionDataSource } from '../../../../core/auth/_data-sources/qaWaitingInspection.datasource';
import { QAService } from '../../../../core/auth/_services/qa.service';
import { QAFilter } from '../../../../core/auth/_models/QAFilter.model';
import { distinctUntilChanged, filter, finalize, skip, takeUntil, tap } from 'rxjs/operators';
import { QADetailsComponent } from '../../../components/QA/detail/qa-details.component';
import { QAWaitingApprovationDataSource } from '../../../../core/auth/_data-sources/qaWaitingApprovation.datasource';
import { QARecordsDataSource } from '../../../../core/auth/_data-sources/qaRecords.datasource';
import { NgxMatDateAdapter, NgxMatDateFormats, NGX_MAT_DATE_FORMATS } from '@angular-material-components/datetime-picker';
import { OccurrenceRecordFlux } from '../../../../core/auth/_models/occurrenceRecordFlux.model';
import { ViewAnalysisComponent } from '../../../components/view-analysis/view-analysis.component';
import { EmailComponent } from '../../../components/email/email.component';
import { SendMailComponent } from '../../../components/send-mail/send-mail.component';
import { ParameterService } from '../../../../core/auth/_services';
import moment from 'moment';
import { DetailsQAAnalysisComponent } from '../Analysis/details-analysis/details-qa-analysis.component';
import { OccurrenceRecord } from '../../../../core/auth/_models/occurenceRecord.model';
import { IdleService } from '../../../../core/_base/layout/services/idle.service';
import { MessageHistoryComponent } from '../../../components/message-history/message-history.component';

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
	selector: 'apta-qa-overview',
	templateUrl: './qa-overview.component.html',
	styleUrls: ['./qa-overview.component.scss'],
	providers: [
		{provide: NGX_MAT_DATE_FORMATS, useValue: DATE_TIME_FORMAT},
	],
})
export class QAOverviewComponent extends BasePageComponent implements OnInit, OnDestroy { //extends BasePageComponent

	filter1: QAFilter = new QAFilter();
	filter2: QAFilter = new QAFilter();
	filter3: QAFilter = new QAFilter();

	//timer
	updateTimer: Observable<number> = timer(0, 60000);
	timerSubscription: Subscription;

	panelWithApproval:boolean = false;
	panelWithAnalysis:boolean = false;
	panelWithoutAnalysis:boolean = false;

	analysisdDS: QARecordsDataSource;
	waitingImpactAnalysisDS: QARecordsDataSource;
	pendingDS: QARecordsDataSource;

	columnApproved = ['startDate', 'endDate', 'id', 'process', 'impactedArea', 'alarms', 'description', 'analysisDate', 'responsible', 'result', 'comments', 'review', 'messages_history'];
	columnWaitingApprovation = ['startDate', 'endDate', 'id', 'process', 'impactedArea', 'alarms', 'description', 'analysisDate', 'responsible', 'result', 'comments', 'messages_history'];
	columnWaitingInspection = ['startDate', 'endDate', 'id', 'process', 'impactedArea', 'alarms', 'description', 'notify', 'messages_history'];

	error: Boolean = false;
	errorMessage: string = '';

	loading: boolean = false;
	permissionTag: string = '';
	mensagemErro: string = '';
	sendToReview: boolean = false;

	private unsubscribe1: Subject<any>;
	private unsubscribe2: Subject<any>;
	private unsubscribe3: Subject<any>;

	@ViewChild(MatPaginator, { static: true }) paginator1: MatPaginator;
	@ViewChild('paginator2', { static: true }) paginator2: MatPaginator;
	@ViewChild('paginator3', { static: true }) paginator3: MatPaginator;

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

	/**
	 * Component constructor
	 *
	 * @param store: Store<AppState>
	 * @param router: Router
	 */
	constructor(public store: Store<AppState>,
		router: Router,
		public dialog: MatDialog,
		private ref: ChangeDetectorRef,
		public translateService: TranslateService,
		public qaService: QAService,
		private layoutUtilsService: LayoutUtilsService,
		private detector: ChangeDetectorRef,
		public parameterService: ParameterService,
		public idleService: IdleService,
		auth: AuthService) {
		super(auth, store, translateService, router,'QA_OVERVIEW', ["SEND_APPROVED_ANALYSIS_REVIEW"], 'QA_OVERVIEW', parameterService, idleService)
		this.sendToReview = this.readPermission("SEND_APPROVED_ANALYSIS_REVIEW");
		this.unsubscribe1 = new Subject();
		this.unsubscribe2 = new Subject();
		this.unsubscribe3 = new Subject();
		
	}

	/**
	 * On init
	 */
	ngOnInit() {
		this.initDatasources();
		//this.checkTimer();
	}
	/**
	 * On Destroy
	 */
	 ngOnDestroy() {
		this.subscriptions.forEach(el => el.unsubscribe());

		if (this.timerSubscription) {
			this.timerSubscription.unsubscribe();
		}
	}

	/**
	 * Flags
	 */
	onAlertClose($event) {
		this.error = false;
	}

	PolicyParameter_Edit(){

		this.dialog.open(SignatureComponent, {
			minHeight: '300px',
			width: '400px'
		}).afterClosed().subscribe(x => {

		});
	}

	initDatasources()
	{
		this.loading = true;
		this.filter1 = new QAFilter();
		this.filter2 = new QAFilter();
		this.filter3 = new QAFilter();

		var today = new Date();

		this.filter1.status = 2;
		this.filter2.status = 1;
		this.filter3.status = 0;

		//Init DataSource
		this.paginator1.page.pipe(
			tap(() => {
				this.loading = true;
				this.loadFilter1();
			}, error => {
				this.loading = false;
				this.layoutUtilsService.showErrorNotification(error.message, MessageType.Create);
			}),
			takeUntil(this.unsubscribe1),
			finalize(() => {
				this.loading = false;
				this.detector.detectChanges();
			})
		).subscribe();

		this.analysisdDS = new QARecordsDataSource(this.qaService, this.store, this.filter1);

		const entitiesSubscription1 = this.analysisdDS.entitySubject.pipe(
			skip(1),
			distinctUntilChanged()
		).subscribe(res => {
			this.loading = false;
			this.mensagemErro = '';
		},error =>{
			this.error = true;
			this.mensagemErro = error.message;
			this.loading = false;
		});
		this.subscriptions.push(entitiesSubscription1);

		this.pendingDS = new QARecordsDataSource(this.qaService, this.store, this.filter2);

		 //Init DataSource
		 this.paginator2.page.pipe(
			tap(() => {
				this.loading = true;
				this.loadFilter2();
			}, error => {
				this.loading = false;
				this.layoutUtilsService.showErrorNotification(error.message, MessageType.Create);
			}),
			takeUntil(this.unsubscribe2),
			finalize(() => {
				this.loading = false;
				this.detector.detectChanges();
			})
		).subscribe();


		const entitiesSubscription2 = this.pendingDS.entitySubject.pipe(
			skip(1),
			distinctUntilChanged()
		).subscribe(res => {
			this.loading = false;
			this.mensagemErro = '';
		},error =>{
			this.error = true;
			this.mensagemErro = error.message;
			this.loading = false;
		});
		 this.subscriptions.push(entitiesSubscription2);

		this.waitingImpactAnalysisDS = new QARecordsDataSource(this.qaService, this.store, this.filter3);

		//Init DataSource
		this.paginator3.page.pipe(
			tap(() => {
				this.loadFilter3();
			}, error => {
				this.loading = false;
				this.layoutUtilsService.showErrorNotification(error.message, MessageType.Create);
			}),
			takeUntil(this.unsubscribe3),
			finalize(() => {
				this.loading = false;
				this.detector.detectChanges();
			})
		).subscribe();

		const entitiesSubscription3 = this.pendingDS.entitySubject.pipe(
			skip(1),
			distinctUntilChanged()
		).subscribe(res => {
			this.loading = false;
			this.mensagemErro = '';
			this.detector.detectChanges();
		},error =>{
			this.error = true;
			this.mensagemErro = error.message;
			this.loading = false;
			this.detector.detectChanges();
		});

		this.subscriptions.push(entitiesSubscription3);

	}

	loadFilter1()
	{

		this.qaService.SearchQAData(this.paginator1.pageIndex + 1, this.paginator1.pageSize, this.filter1).subscribe((response: QueryResultsModel) => {
			this.analysisdDS.paginatorTotalSubject.next(response.rowsCount);
			this.analysisdDS.entitySubject.next(response.entities);
			this.analysisdDS.loadingSubject.next(false);
			this.loading = false;
		}, error =>{
			this.analysisdDS.paginatorTotalSubject.next(0);
			this.analysisdDS.entitySubject.next(null);
			this.analysisdDS.loadingSubject.next(false);
			this.loading = false;
		});
	}

	loadFilter2()
	{
		this.qaService.SearchQAData(this.paginator2.pageIndex + 1, this.paginator2.pageSize, this.filter2).subscribe((response: QueryResultsModel) => {
			this.pendingDS.paginatorTotalSubject.next(response.rowsCount);
			this.pendingDS.entitySubject.next(response.entities);
			this.pendingDS.loadingSubject.next(false);
			this.loading = false;
		}, error =>{
			this.pendingDS.paginatorTotalSubject.next(0);
			this.pendingDS.entitySubject.next(null);
			this.pendingDS.loadingSubject.next(false);
			this.loading = false;
		});
	}

	loadFilter3()
	{
		
		this.qaService.SearchQAData(this.paginator3.pageIndex + 1, this.paginator3.pageSize, this.filter3).subscribe((response: QueryResultsModel) => {
			this.waitingImpactAnalysisDS.paginatorTotalSubject.next(response.rowsCount);
			this.waitingImpactAnalysisDS.entitySubject.next(response.entities);
			this.waitingImpactAnalysisDS.loadingSubject.next(false);
			this.loading = false;
		}, error =>{
			this.waitingImpactAnalysisDS.paginatorTotalSubject.next(0);
			this.waitingImpactAnalysisDS.entitySubject.next(null);
			this.waitingImpactAnalysisDS.loadingSubject.next(false);
			this.loading = false;
		});
	}

	loadFilters()
	{
		this.loading = true;
		this.paginator1.pageIndex = 0;
		this.paginator2.pageIndex = 0;
		this.paginator3.pageIndex = 0;

		this.filter2.startDate = this.filter1.startDate;
		this.filter2.endDate = this.filter1.endDate;

		this.filter3.startDate = this.filter1.startDate;
		this.filter3.endDate = this.filter1.endDate;
3
		this.loadFilter1();
		this.loadFilter2();
		this.loadFilter3();
	}




	clearFilters(){
		this.filter1 = new QAFilter();
		this.filter2 = new QAFilter();
		this.filter3 = new QAFilter();
		this.filter1.status = 2;
		this.filter2.status = 1;
		this.filter3.status = 0;
		this.paginator1.pageIndex = 0;
		this.paginator2.pageIndex = 0;
		this.paginator3.pageIndex = 0;
		this.loadFilters();
	}

	viewDetails(occurrence){
		const dialogRef = this.dialog.open(QADetailsComponent, {width:'80%', data: occurrence });
		dialogRef.afterClosed().subscribe(res => {

		});
	}

	OpenDetailsModal(row:any){
		this.qaService.GetQADetailData(row.occurrenceRecordId).subscribe(x=> {
			this.dialog.open(QADetailsComponent, {
				width: '80%',
				data: x,
				panelClass: 'full-with-dialog',
			}).afterClosed().subscribe(x => {

			});
		});
	}

	sendNotification(alarm)
	{
		const dialogRef = this.dialog.open(SendMailComponent, {width:'350px', data: {occurrenceRecordId: alarm.occurrenceRecordId, impactedAreaId: alarm.impactedAreaId, includeQas: false  } });
		dialogRef.afterClosed().subscribe(res => {

		});
	}

	viewSignatureDetails(item){
		let analysis: OccurrenceRecordFlux = new OccurrenceRecordFlux();
		analysis.initials = item.responsible;
		analysis.dateTime = item.analysisDate;
		analysis.comments = item.comments;

		const dialogRef = this.dialog.open(ViewAnalysisComponent, {
			minHeight: '300px',
			width: '40%',
			data: {analysis: analysis}
		});
		dialogRef.afterClosed().subscribe(res => {
			if(res){

			}
		});
	}

	review(alarm: OccurrenceRecord) {
		let occurrences: OccurrenceRecord[] = [ alarm ];
		const dialogRef = this.dialog.open(DetailsQAAnalysisComponent, {width:'80%', data: { review: true, occurrences: occurrences } });
		dialogRef.afterClosed().subscribe(res => {
			this.loadFilter1();
		});
	}

	checkTimer() {
		const subs = this.updateTimer.subscribe(() => {
			this.loadFilters();
		});
		this.timerSubscription = subs;
	}

	viewMessageHistory(id){
		const dialogRef = this.dialog.open(MessageHistoryComponent, {width:'80%', height:'80%', data: { occurrenceRecordId: id } });
		dialogRef.afterClosed().subscribe(res => {

		});
	}
	
}

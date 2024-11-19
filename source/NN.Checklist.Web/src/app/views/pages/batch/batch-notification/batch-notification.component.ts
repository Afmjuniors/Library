import { ChangeDetectorRef, Component, Inject, OnInit, ViewChild } from '@angular/core';
import { QARecordsDataSource } from '../../../../core/auth/_data-sources/qaRecords.datasource';
import { AppState } from '../../../../core/reducers';
import { Store } from '@ngrx/store';
import { Router } from '@angular/router';
import { MAT_DIALOG_DATA, MatDialog, MatDialogRef, MatPaginator } from '@angular/material';
import { TranslateService } from '@ngx-translate/core';
import { QAService } from '../../../../core/auth/_services/qa.service';
import { LayoutUtilsService, MessageType } from '../../../../core/_base/crud';
import { AuthService, ParameterService } from '../../../../core/auth/_services';
import { IdleService } from '../../../../core/_base/layout/services/idle.service';
import { QAFilter } from '../../../../core/auth/_models/QAFilter.model';
import { distinctUntilChanged, finalize, skip, takeUntil, tap } from 'rxjs/operators';
import { Subject } from 'rxjs';
import { BasePageComponent } from '../../BasePage.component';
import { QADetailsComponent } from '../../../components/QA/detail/qa-details.component';
import { SendMailComponent } from '../../../components/send-mail/send-mail.component';
import { OccurrenceRecordFlux } from '../../../../core/auth/_models/occurrenceRecordFlux.model';
import { ViewAnalysisComponent } from '../../../components/view-analysis/view-analysis.component';
import { OccurrenceRecord } from '../../../../core/auth/_models/occurenceRecord.model';
import { DetailsQAAnalysisComponent } from '../../QA/Analysis/details-analysis/details-qa-analysis.component';
import { MessageHistoryComponent } from '../../../components/message-history/message-history.component';

@Component({
	selector: 'kt-batch-notification',
	templateUrl: './batch-notification.component.html',
	styleUrls: ['./batch-notification.component.scss'],
	providers:[QAService]
})
export class BatchNotificationComponent extends BasePageComponent implements OnInit {
	
	filter2: QAFilter = new QAFilter();
	filter3: QAFilter = new QAFilter();
	filterStartDate: Date = new Date();
	filterEndDate: Date = new Date();
	processId: number;
	areaId: number;

	panelWithApproval:boolean = false;
	panelWithAnalysis:boolean = false;
	panelWithoutAnalysis:boolean = false;

	waitingImpactAnalysisDS: QARecordsDataSource;
	pendingDS: QARecordsDataSource;

	columnWaitingApprovation = ['startDate', 'endDate', 'id', 'process', 'impactedArea', 'alarms', 'description', 'analysisDate', 'responsible', 'result', 'comments', 'notify', 'messages_history'];
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
		public dialogRef: MatDialogRef<BatchNotificationComponent>,
		@Inject(MAT_DIALOG_DATA)
		public data: any,
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
		console.log("chegou data", this.data)
		if(this.data.filtro != null){
			this.processId = this.data.filtro.processId != null ? this.data.filtro.processId : null
			this.areaId = this.data.filtro.areaId != null ? this.data.filtro.areaId : null
		}

		if(this.data.date != null){
			this.filterStartDate = this.data.date;
			this.filter2.startDate = this.data.date;
			this.filter3.startDate = this.data.date;
			
			let nextDay: Date = new Date(this.filterStartDate.getFullYear(),this.filterStartDate.getMonth(),this.filterStartDate.getDate(),0,0,0);
			nextDay.setDate(nextDay.getDate()+1);

			this.filter2.endDate = nextDay;
			this.filter3.endDate = nextDay;
			this.filterEndDate = nextDay;

			this.initDatasources();
		}
	}
	/**
	 * On Destroy
	 */
	 ngOnDestroy() {
		this.subscriptions.forEach(el => el.unsubscribe());
	}

	/**
	 * Flags
	 */
	onAlertClose($event) {
		this.error = false;
	}

	initDatasources()
	{
		this.loading = true;

		this.filter2 = new QAFilter();
		this.filter3 = new QAFilter();

		var today = new Date();

		this.filter2.status = 1;
		this.filter3.status = 0;

		this.filter2.area = this.areaId;
		this.filter3.area = this.areaId;

		this.filter2.process = this.processId;
		this.filter3.process = this.processId;

		this.filter2.release = true;
		this.filter3.release = true;

		this.filter2.startDate = this.filterStartDate;
		this.filter2.endDate = this.filterEndDate;

		this.filter3.startDate = this.filterStartDate;
		this.filter3.endDate = this.filterEndDate;
		
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

	loadFilter2()
	{
		this.pendingDS.load(this.paginator2.pageIndex, this.paginator2.pageSize, this.filter2);
	}

	loadFilter3()
	{
		this.waitingImpactAnalysisDS.load(this.paginator3.pageIndex, this.paginator3.pageSize, this.filter3);
	}

	loadFilters()
	{
		this.loading = true;
		this.paginator2.pageIndex = 0;
		this.paginator3.pageIndex = 0;

		this.loadFilter2();
		this.loadFilter3();
	}

	clearFilters(){
		this.filter2 = new QAFilter();
		this.filter3 = new QAFilter();
		this.filter2.status = 1;
		this.filter3.status = 0;
		this.filter2.release = true;
		this.filter3.release = true;
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

	sendNotification(alarm, qas: boolean)
	{
		const dialogRef = this.dialog.open(SendMailComponent, {width:'350px', data: {occurrenceRecordId: alarm.occurrenceRecordId, impactedAreaId: alarm.impactedAreaId, includeQas: qas } });
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
			//this.loadFilter1();
		});
	}

	viewMessageHistory(id){
		const dialogRef = this.dialog.open(MessageHistoryComponent, {width:'80%', height:'80%', data: { occurrenceRecordId: id } });
		dialogRef.afterClosed().subscribe(res => {

		});
	}
	
}

import { Component, OnInit, ViewChild, ChangeDetectorRef } from '@angular/core';
import { BasePageComponent } from '../../BasePage.component';
import { Observable, merge, Subject, timer, Subscription } from 'rxjs';
import { Permission, AuthService } from '../../../../core/auth';
import { MatPaginator, MatSort, MatDialog } from '@angular/material';
import { LayoutUtilsService, MessageType, QueryParamsModel, QueryResultsModel } from '../../../../core/_base/crud';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { select, Store } from '@ngrx/store';
import { AppState } from '../../../../core/reducers';
import { Router } from '@angular/router';
import { TranslateService } from '@ngx-translate/core';
import { currentUser, Logout, User } from '../../../../core/auth';

import { SignatureComponent } from '../../../components/signature/signature.component';
import { QAWaitingApprovationDataSource } from '../../../../core/auth/_data-sources/qaWaitingApprovation.datasource';
import { QAService } from '../../../../core/auth/_services/qa.service';
import { QAFilter } from '../../../../core/auth/_models/QAFilter.model';
import { distinctUntilChanged, filter, finalize, skip, takeUntil, tap } from 'rxjs/operators';
import { QAApprovalComponent } from '../../../components/QA/approval/qa-approval.component';
import { OccurrenceRecord } from '../../../../core/auth/_models/occurenceRecord.model';
import { BaseComponent } from '../../../theme/base/base.component';
import { QARecordsDataSource } from '../../../../core/auth/_data-sources/qaRecords.datasource';
import { NgxMatDateAdapter, NgxMatDateFormats, NGX_MAT_DATE_FORMATS } from '@angular-material-components/datetime-picker';
import { DetailsQAAnalysisComponent } from './details-analysis/details-qa-analysis.component';
import { ViewAnalysisComponent } from '../../../components/view-analysis/view-analysis.component';
import { OccurrenceRecordFlux } from '../../../../core/auth/_models/occurrenceRecordFlux.model';
import { OccurrenceAnalysisDetailsItem } from '../../../../core/auth/_models/occurrenceAnalysisDetailsItem.model';
import { SelectionModel } from '@angular/cdk/collections';
import { AppService, ParameterService } from '../../../../core/auth/_services';
import { IdleService } from '../../../../core/_base/layout/services/idle.service';
import { Process } from '../../../../core/auth/_models/process.model';
import { Area } from '../../../../core/auth/_models/area.model';
import { MessageHistoryComponent } from '../../../components/message-history/message-history.component';
import { PolicyParameter } from '../../../../core/auth/_models/_parameter/PolicyParameter.models';


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
	selector: 'apta-qa-analysis',
	templateUrl: './qa-analysis.component.html',
	styleUrls: ['./qa-analysis.component.scss'],
	providers: [
		{provide: NGX_MAT_DATE_FORMATS, useValue: DATE_TIME_FORMAT},
	],
})
export class QAAnalysisComponent extends BasePageComponent implements OnInit {

	@ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;

	filter: QAFilter = new QAFilter();
	panelWithAnalyse:boolean = true;
	dataSource: QARecordsDataSource;
	columnWithAnalyse = ['selected', 'startDate', 'endDate', 'id', 'process', 'impactedArea', 'alarms', 'description', 'analysisDate', 'responsible', 'result', 'comments', 'messages_history'];

	processes: Process[] = [];
	areas: Area[] = [];

	process: string;
	area: string;

	error: Boolean = false;
	errorMessage: string = '';

	loading: boolean = false;
	permissionTag: string = '';
	selectedIds: OccurrenceAnalysisDetailsItem[] = [];

	private unsubscribe: Subject<any>;

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

	selection = new SelectionModel<OccurrenceRecord>(true, []);

		//timer
	updateTimer: Observable<number> = timer(0, 60000);
	timerSubscription: Subscription;

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
		auth: AuthService,
		public parameterService: ParameterService,
		public idleService: IdleService,
		public app: AppService) {
		super(auth, store, translateService, router, "QA_ANALYSIS", [], "QA_ANALYSIS", parameterService, idleService)
		this.unsubscribe = new Subject();
	}

	/**
	 * On init
	 */
	ngOnInit() {
		this.loading = true;
		this.process = this.translate.instant("MENU.PROCESS");
		this.area = this.translate.instant("MENU.AREA");

		this.init();
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

	init()
	{
		this.loading = true;
		this.loadListProcess();

		this.filter = new QAFilter();
		this.filter.status = 1;

		this.dataSource = new QARecordsDataSource(this.qaService, this.store, this.filter);

		 //Init DataSource
		 this.paginator.page.pipe(
			tap(() => {
				this.loading = true;
				this.load();
			}, error => {
				this.layoutUtilsService.showErrorNotification(error.message, MessageType.Create);
			}),
			takeUntil(this.unsubscribe),
			finalize(() => {
				this.loading = false;
				this.detector.detectChanges();
			})
		).subscribe();


		const entitiesSubscription2 = this.dataSource.entitySubject.pipe(
			skip(1),
			distinctUntilChanged()
		).subscribe(res => {
			this.loading = false;
			this.errorMessage = '';
		},error =>{
			this.error = true;
			this.errorMessage = error.message;
			this.layoutUtilsService.showErrorNotification(error.message, MessageType.Create);
			this.loading = false;
		});
		 this.subscriptions.push(entitiesSubscription2);
	}

	/**
	 * Flags
	 */
	onAlertClose($event) {
		this.error = false;
	}

	PolicyParameter_Edit()
	{
		this.dialog.open(SignatureComponent, {
			minHeight: '300px',
			width: '400px'
		}).afterClosed().subscribe(x => {

		});
	}

	load()
	{
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

	loadFilter()
	{
		this.loading = true;
		this.paginator.pageIndex = 0;

		if(this.filter.process == 0){
			this.filter.process = null;
		}

		if(this.filter.area == 0){
			this.filter.area = null;
		}

		this.load();
	}

	clearFilters(){
		this.filter = new QAFilter();
		this.filter.status = 1;
		this.load()
	}

	openApproveModal(){
		let occurrences = this.selection.selected;
		const dialogRef = this.dialog.open(DetailsQAAnalysisComponent, {width:'80%', data: { occurrences: occurrences } });
		dialogRef.afterClosed().subscribe(res => {
			this.loading = true;
			this.selectedIds = [];
			this.selection.clear();
			this.loadFilter();
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
		const numRows = this.dataSource.paginatorTotalSubject.value;
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

	viewDetails(occurrence) {
		const dialogRef = this.dialog.open(DetailsQAAnalysisComponent, { width: '80%', data: { occurrences: [ occurrence ] } });
		dialogRef.afterClosed().subscribe(res => {


		});
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
			this.loadFilter();
		});
		this.timerSubscription = subs;
	}
}

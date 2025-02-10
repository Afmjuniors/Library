// Angular
import { Component, ChangeDetectionStrategy, OnInit, OnDestroy, ViewChild, ElementRef, ChangeDetectorRef } from '@angular/core';
import { Router } from '@angular/router';
// RxJS
import { Observable, Subscription, merge, fromEvent, of, Subject, timer } from 'rxjs';
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
import { AppService, ParameterService } from '../../../../core/auth/_services';
import { EmailComponent } from '../../../components/email/email.component';
import { NGX_MAT_DATE_FORMATS } from '@angular-material-components/datetime-picker';
import { PdfViewComponent } from '../../../components/pdf-view/pdf-view.component';

import { IdleService } from '../../../../core/_base/layout/services/idle.service';
import { ChecklistDataSource } from '../../../../core/auth/_data-sources/checklist.datasource';
import { ChecklistTemplate } from '../../../../core/auth/_models/checklistTemplate.model';
import { ChecklistFilter } from '../../../../core/auth/_models/checklistFilter.model';
import { NewChecklistForm } from '../../../components/new-checklist-form/newChecklistForm.component';
import { ChecklistModel } from '../../../../core/auth/_models/checklist.model';
import { UpdateCheklistForm } from '../../../components/update-checklist-form/update-checklist.component';
import { VersionChecklistTemplate } from '../../../../core/auth/_models/versionChecklistTemplate.model';
import { FieldChecklist } from '../../../../core/auth/_models/fieldChecklist.model';

const DATE_TIME_FORMAT = {
	parse: {
		dateInput: 'YYYY-MM-DD HH:mm:ss',
	},
	display: {
		dateInput: 'YYYY-MM-DD HH:mm:ss',
		monthYearLabel: 'YYYY MMM',
		dateA11yLabel: 'DD',
		monthYearA11yLabel: 'YYYY MMM',
		enableMeridian: false,
		useUtc: true
	},
};

@Component({
	selector: 'td-checklist',
	templateUrl: './checklists.component.html',
	styleUrls: ['./checklists.component.scss'],
	providers: [
		{ provide: NGX_MAT_DATE_FORMATS, useValue: DATE_TIME_FORMAT },
	],
})
export class ChecklistComponent extends BasePageComponent implements OnInit {
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
	dataSource: ChecklistDataSource;
	displayedColumns = ['edit',  'Checklist','key-title' ,'key', 'batch_date', 'is_batch_released'];
	@ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;
	@ViewChild('sort1', { static: true }) sort: MatSort;

	// Selection
	checklistResult: ChecklistTemplate[] = [];
	permiteCriar: Boolean;
	permiteEditar: Boolean;
	permiteDesativar: Boolean;
	buscaForm: FormGroup;
	lastConditions: string[] = [];
	error: Boolean = false;
	mensagemErro: string = '';

	filter: ChecklistFilter = new ChecklistFilter();

	// Screen
	title: string;
	loading: boolean = false;
	checklist: string;
	versions: string;

	pdfFile;
	csvFile;

	document;
	visible = false;

	versionListTemplate:VersionChecklistTemplate[] = [];

	InactivityTimeLimit: number = 60;
	timer: any;
	public checklistsFileter: ChecklistTemplate[] = [];

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
	public createNewChecklist: boolean = false;


	private unsubscribe: Subject<any>;

	private loadingStr:string;

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
		super(auth, store, translate, router, "CHECKLIST", ["CREATE_NEW_EDIT_CHECKLIST"], "CHECKLIST", parameterService, idleService)
		this.createNewChecklist = this.readPermission("CREATE_NEW_EDIT_CHECKLIST");
		this.unsubscribe = new Subject();
	}

	ngOnInit() {
		this.title = this.translate.instant("MENU.CHECKLIST");
		this.checklist = this.translate.instant("FILTERS.CHECKLIST");
		this.versions = this.translate.instant("FILTERS.VERSIONS");
		this.loadingStr = this.translate.instant("CHECKLISTS.LOADING");
		this.loadListLastConditions();
		this.loadListChecklist();
		this.initChecklist();
		//this.checkTimer();
	}

tranlateIfIsCompleted(isCompleted:boolean){
	if(isCompleted){
		return this.translate.instant("CHECKLISTS.IS_COMPLETED");

	}else{
		return this.translate.instant("CHECKLISTS.IS_NOT_COMPLETED");
	}
}

	loadListChecklist() {
		this.loading = true;
		this.app.listChecklistTemplate().subscribe(x => {
			this.checklistsFileter = x.result;
			this.loading = false;
		},
			error => {
				this.layoutUtilsService.showErrorNotification(error, MessageType.Create);
				this.loading = false;
			});
	}

	initChecklist() {

		this.paginator.page.pipe(
			tap(() => {
				this.loading = true;
				this.loadChecklist();
			}, error => {
				this.loading = false;
				this.layoutUtilsService.showErrorNotification(error, MessageType.Create);
			}),
			takeUntil(this.unsubscribe),
			finalize(() => {
				this.loading = false;
				this.detector.detectChanges();
			})
		).subscribe();


		//Init DataSource
		this.dataSource = new ChecklistDataSource(this.app, this.store);

		const entitiesSubscription = this.dataSource.entitySubject.pipe(
			skip(1),
			distinctUntilChanged()
		).subscribe(res => {
			this.checklistResult = res;
			this.loading = false;
		}, error => {
			this.mensagemErro = '';
			this.error = true;
			this.mensagemErro = error.message;
			this.loading = false;
		});
		this.subscriptions.push(entitiesSubscription);
	}

	changeTemplate() {
		this.loading = true;

		this.app.getChecklistVersionsList(this.filter.checklistTemplateId)
			.subscribe((response) => {
				this.versionListTemplate = response.result
				this.loading = false;


			}, error => {

				this.loading = false;
			});
	}

	loadChecklist(): void {
		this.loading = true;
		if (this.filter.versionChecklistTemplateId == 0) {
			this.filter.versionChecklistTemplateId = null;
		}

		this.app.getAllChecklists(this.paginator.pageIndex + 1, this.paginator.pageSize, this.filter).subscribe((response: QueryResultsModel) => {
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

	consoleShowChelistPage() {
		console.log(this.dataSource);
	}

	ngOnDestroy(): void {
		this.subscriptions.forEach(el => el.unsubscribe());

		if (this.timerSubscription) {
			this.timerSubscription.unsubscribe();
		}
	}

	filtrar() {
		this.paginator.pageIndex = 0
		this.loadChecklist();
	}

	limparFiltros() {
		this.filter = new ChecklistFilter();
		this.loadChecklist();
	}

	loadListLastConditions() {
		this.lastConditions.push("");
		this.loadChecklist();

	}
	getKeyValue(fields: FieldChecklist[]):string{
		if(!fields){
			return this.loadingStr;
		}
		var field = fields.find(x=>x.fieldVersionChecklistTemplate.isKey);
		if(!field){
			return "KeyValue";
		}
		return field.value;

	}
	getKeyName(fields: FieldChecklist[]):string{
		if(!fields){
			return this.loadingStr;
		}
		var field = fields.find(x=>x.fieldVersionChecklistTemplate.isKey);
		if(!field){
			return "KeyTitle";
		}
		return field.fieldVersionChecklistTemplate.title;

	}


	newChecklist() {
		const dialogRef = this.dialog.open(NewChecklistForm, { width: '60%', data: { type: 1 } });
		dialogRef.afterClosed().subscribe(res => {
			this.loading = true;
			this.loadChecklist();
		});
	}

	editChecklList(checklist: ChecklistModel) {
		const dialogRef = this.dialog.open(UpdateCheklistForm, { width: '60%', data: checklist });
		dialogRef.afterClosed().subscribe(res => {
			this.loading = true;
			this.loadChecklist();
		});
	}

	checkTimer() {
		const subs = this.updateTimer.subscribe(() => {
			this.filtrar();
		});
		this.timerSubscription = subs;
	}


}

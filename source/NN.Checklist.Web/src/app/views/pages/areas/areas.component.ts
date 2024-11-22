import { NewAreaComponent } from './new-area/new-area.component';
import { SelectionModel } from '@angular/cdk/collections';
import { ChangeDetectorRef, Component, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { MatDialog, MatPaginator, MatSort } from '@angular/material';
import { Router } from '@angular/router';
import { Store } from '@ngrx/store';
import { TranslateService } from '@ngx-translate/core';
import { Observable, Subject } from 'rxjs';
import { AppService, ParameterService } from '../../../core/auth/_services';
import { AppState } from '../../../core/reducers';
import { LayoutUtilsService, MessageType, QueryResultsModel } from '../../../core/_base/crud';
import { AuthService, Logout, Permission } from '../../../core/auth';
import { AreasDataSource } from '../../../core/auth/_data-sources/areas.datasource';
import { Area } from '../../../core/auth/_models/area.model';
import { BasePageComponent } from '../BasePage.component';
import { distinctUntilChanged, finalize, skip, takeUntil, tap } from 'rxjs/operators';
import { Process } from '../../../core/auth/_models/process.model';
import { AreaPhonesComponent } from './area-phones/area-phones.component';
import { IdleService } from '../../../core/_base/layout/services/idle.service';


@Component({
	selector: 'td-areas',
	templateUrl: './areas.component.html',
	styleUrls: ['./areas.component.scss']
})
export class AreasComponent extends BasePageComponent implements OnInit {
	// Public properties
	hasUserAccess$: Observable<boolean>;
	hasUserAccessCriar$: Observable<boolean>;
	hasUserAccessEditar$: Observable<boolean>;
	hasUserAccessDesativar$: Observable<boolean>;
	currentUserPermission$: Observable<Permission[]>;

	// Table fields
	dataSource: AreasDataSource;
	displayedColumns = ['name','edit', 'description', 'process', 'action'];
	@ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;
	@ViewChild('sort1', { static: true }) sort: MatSort;

	// Selection
	selection = new SelectionModel<Area>(true, []);
	AreasResult: Area[] = [];
	permiteCriar: Boolean;
	permiteEditar: Boolean;
	permiteDesativar: Boolean;
	buscaForm: FormGroup;
	filter: Area = new Area();
	lastConditions: string[] = [];
	error: Boolean = false;
	errorMessage: string = '';
	processes: Process[] = [];

	// Screen
	title: string;
	loading: boolean = false;
	process: string;
	area: string;

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
		super(auth, store, translate, router, "MANAGE_AREAS" ,[], "MANAGE_AREAS", parameterService, idleService)
		this.unsubscribe = new Subject();
	}

	ngOnInit() {
		this.initAreas();

		this.title  = this.translate.instant("MENU.AREAS");
		this.loadListProcess();
	}

	initAreas(){
		this.loading = true;
		this.paginator.page.pipe(
			tap(() => {
				this.loading = true;
				this.loadAreas();
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
		this.dataSource = new AreasDataSource(this.app, this.store);

		const entitiesSubscription = this.dataSource.entitySubject.pipe(
			skip(1),
			distinctUntilChanged()
		).subscribe(res => {
			this.AreasResult = res;
			this.loading = false;
		},error =>{
			this.errorMessage = '';
			this.error = true;
			this.errorMessage = error.message;
			this.loading = false;
		});
		this.subscriptions.push(entitiesSubscription);
	}

	loadAreas(): void {
		this.loading = true;

		this.app.getAllAreas(this.paginator.pageIndex + 1, this.paginator.pageSize, this.filter).subscribe((response: QueryResultsModel) => {
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
	}

	applyFilters() {
		this.paginator.pageIndex = 0

		if(this.filter.processId == 0){
			this.filter.process = null;
		}

		this.loadAreas();
	}

	limparFiltros() {
		this.filter = new Area();
		this.loadAreas();
	}

	loadListProcess(){
		this.app.listProcesses().subscribe(x => {
			this.processes = x;
		},
		error =>
		{
			this.layoutUtilsService.showErrorNotification(error, MessageType.Create);
		});
	}

	viewDetailsPhones(area){
		const dialogRef = this.dialog.open(AreaPhonesComponent, {width:'30%', data: { area: area } });
		dialogRef.afterClosed().subscribe(res => {

			this.applyFilters();
		});
	}

	addArea(){
		const dialogRef = this.dialog.open(NewAreaComponent, {width:'60%', data: { areaId: null } });
		dialogRef.afterClosed().subscribe(res => {

			this.applyFilters();
		});
	}

	editArea(area){
		const dialogRef = this.dialog.open(NewAreaComponent, {width:'60%', data: { area: area } });
		dialogRef.afterClosed().subscribe(res => {

			this.applyFilters();
		});
	}
}

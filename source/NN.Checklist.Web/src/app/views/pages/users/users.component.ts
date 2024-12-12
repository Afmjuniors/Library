import { SelectionModel } from '@angular/cdk/collections';
import { ChangeDetectorRef, Component, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { MatDialog, MatPaginator, MatSort } from '@angular/material';
import { Router } from '@angular/router';
import { Store } from '@ngrx/store';
import { TranslateService } from '@ngx-translate/core';
import { Observable, Subject } from 'rxjs';
import { distinctUntilChanged, finalize, skip, takeUntil, tap } from 'rxjs/operators';
import { AuthService, Logout, Permission, User } from '../../../core/auth';
import { UsersDataSource } from '../../../core/auth/_data-sources/users.datasource';
import { AppService, ParameterService } from '../../../core/auth/_services';
import { AppState } from '../../../core/reducers';
import { LayoutUtilsService, MessageType, QueryResultsModel } from '../../../core/_base/crud';
import { BasePageComponent } from '../BasePage.component';
import { UserEditComponent } from './user-edit/user-edit.component';
import { UserControlComponent } from './user-control/user-control.component';

import { IdleService } from '../../../core/_base/layout/services/idle.service';

@Component({
	selector: 'kt-users',
	templateUrl: './users.component.html',
	styleUrls: ['./users.component.scss']
})
export class UsersComponent extends BasePageComponent implements OnInit {
	// Public properties
	hasUserAccess$: Observable<boolean>;
	hasUserAccessCriar$: Observable<boolean>;
	hasUserAccessEditar$: Observable<boolean>;
	hasUserAccessDesativar$: Observable<boolean>;
	currentUserPermission$: Observable<Permission[]>;

	// Table fields
	dataSource: UsersDataSource;
	displayedColumns = ['initials',  'status', 'active'];
	@ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;
	@ViewChild('sort1', { static: true }) sort: MatSort;

	// Selection
	selection = new SelectionModel<User>(true, []);
	UserResult: User[] = [];
	permiteCriar: Boolean;
	permiteEditar: Boolean;
	permiteDesativar: Boolean;
	buscaForm: FormGroup;
	filter: User = new User();
	error: Boolean = false;
	mensagemErro: string = '';

	// Screen
	title: string;
	loading: boolean = false;
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
		super(auth, store, translate, router, "MANAGE_USERS" ,[], "MANAGE USERS", parameterService, idleService)
		this.unsubscribe = new Subject();
	}

	ngOnInit() {
		this.title  = this.translate.instant("MENU.USERS");
		this.filter.initials = "";
		this.initUsers();
	}

	initUsers(){
		this.loading = true;
		this.idleService.restartTimer();

		this.paginator.page.pipe(
			tap(() => {
				this.loading = true;
				this.loadUsers();
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
		this.dataSource = new UsersDataSource(this.app, this.store);

		const entitiesSubscription = this.dataSource.entitySubject.pipe(
			skip(1),
			distinctUntilChanged()
		).subscribe(res => {
			this.idleService.restartTimer();
			this.UserResult = res;
			this.loading = false;
		},error =>{
			this.mensagemErro = '';
			this.error = true;
			this.mensagemErro = error.message;
			this.loading = false;
		});
		this.subscriptions.push(entitiesSubscription);
	}

	loadUsers(): void {
		this.loading = true;
		this.idleService.restartTimer();

		this.app.getAllUsers(this.paginator.pageIndex + 1, this.paginator.pageSize, this.filter).subscribe((response: QueryResultsModel) => {
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
		this.loadUsers();
	}

	clear() {
		this.filter = new User();
		this.filter.initials = "";
		this.loadUsers();
	}

	viewDetails(user){
		this.idleService.restartTimer();
		const dialogRef = this.dialog.open(UserEditComponent, {width:'80%', data: { user: user } });
		dialogRef.afterClosed().subscribe(res => {
			this.idleService.restartTimer();
			this.applyFilters();
		});
	}



	controlUser(user){
		this.idleService.restartTimer();
		const dialogRef = this.dialog.open(UserControlComponent, {width:'60%', data: { user: user } });
		dialogRef.afterClosed().subscribe(res => {
			this.idleService.restartTimer();
			this.applyFilters();
		});
	}


}

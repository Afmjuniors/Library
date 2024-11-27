import { AdGroup } from './../../../core/auth/_models/adGroup.model';
import { AdGroupsDataSource } from './../../../core/auth/_data-sources/adGroups.datasource';
import { ChangeDetectorRef, Component, OnInit, ViewChild } from '@angular/core';
import { Observable, Subject } from 'rxjs';
import { AuthService, Logout, Permission } from './../../../core/auth';
import { BasePageComponent } from '../BasePage.component';
import { MatDialog, MatPaginator, MatSort } from '@angular/material';
import { Store } from '@ngrx/store';
import { AppState } from './../../../core/reducers';
import { Router } from '@angular/router';
import { LayoutUtilsService, MessageType, QueryResultsModel } from './../../../core/_base/crud';
import { FormBuilder, FormGroup } from '@angular/forms';
import { AppService, ParameterService } from './../../../core/auth/_services';
import { TranslateService } from '@ngx-translate/core';
import { SelectionModel } from '@angular/cdk/collections';

import { distinctUntilChanged, finalize, skip, takeUntil, tap } from 'rxjs/operators';
import { NewAdGroupComponent } from './new-ad-group/new-ad-group.component';
import { RemoveAdGroupComponent } from './remove-ad-group/remove-ad-group.component';
import { IdleService } from './../../../core/_base/layout/services/idle.service';
import { SignatureComponent } from '../../components/signature/signature.component';

@Component({
	selector: 'kt-ad-groups',
	templateUrl: './ad-groups.component.html',
	styleUrls: ['./ad-groups.component.scss']
})
export class AdGroupsComponent extends BasePageComponent implements OnInit {

	// Public properties
	hasUserAccess$: Observable<boolean>;
	hasUserAccessCriar$: Observable<boolean>;
	hasUserAccessEditar$: Observable<boolean>;
	hasUserAccessDesativar$: Observable<boolean>;
	currentUserPermission$: Observable<Permission[]>;

	// Table fields
	dataSource: AdGroupsDataSource;
	displayedColumns = ['name', 'action', 'administrator' ];
	@ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;
	@ViewChild('sort1', { static: true }) sort: MatSort;

	// Selection
	selection = new SelectionModel<AdGroup>(true, []);
	AdGroupsResult: AdGroup[] = [];
	permiteCriar: Boolean;
	permiteEditar: Boolean;
	permiteDesativar: Boolean;
	buscaForm: FormGroup;
	filter: AdGroup = new AdGroup();
	lastConditions: string[] = [];
	error: Boolean = false;
	errorMessage: string = '';

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
		public parameterService: ParameterService,
		public idleService: IdleService,
		public translate: TranslateService,
		private detector: ChangeDetectorRef,
	) {
		super(auth, store, translate, router, "MANAGE_GROUPS", [], "MANAGE_GROUPS", parameterService, idleService)
		this.unsubscribe = new Subject();
	}

	ngOnInit() {
		this.title = this.translate.instant("MENU.AREAS");
		this.initAdGroups();
	}

	initAdGroups() {
		this.loading = true;
		this.paginator.page.pipe(
			tap(() => {
				this.loading = true;
				this.loadAdGroups();
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
		this.dataSource = new AdGroupsDataSource(this.app, this.store);

		const entitiesSubscription = this.dataSource.entitySubject.pipe(
			skip(1),
			distinctUntilChanged()
		).subscribe(res => {
			this.AdGroupsResult = res;
			this.loading = false;
		}, error => {
			this.errorMessage = '';
			this.error = true;
			this.errorMessage = error.message;
			this.loading = false;
		});
		this.subscriptions.push(entitiesSubscription);
	}

	loadAdGroups(): void {
		this.loading = true;

		this.app.getAllAdGroups(this.paginator.pageIndex + 1, this.paginator.pageSize, this.filter).subscribe((response: QueryResultsModel) => {
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
		this.loadAdGroups();
	}

	limparFiltros() {
		this.filter = new AdGroup();
		this.loadAdGroups();
	}

	newAdGroup(){
		const dialogRef = this.dialog.open(NewAdGroupComponent, {width:'60%', data: { adGroup: null } });
		dialogRef.afterClosed().subscribe(res => {
			this.applyFilters();
		});
	}

	editAdGroup(adGroup: AdGroup){
		const dialogRef = this.dialog.open(NewAdGroupComponent, {width:'60%', data: { adGroupId: adGroup.adGroupId } });
		dialogRef.afterClosed().subscribe(res => {
			this.applyFilters();
		});
	}

	removeAdGroup(adGroup: AdGroup){
		const dialogRef = this.dialog.open(RemoveAdGroupComponent, { data: { adGroup: adGroup } });
		dialogRef.afterClosed().subscribe(res => {
			if (!res) {
				this.applyFilters();
				return;
			} else {
				this.applyFilters();
			}
		});
	}
}

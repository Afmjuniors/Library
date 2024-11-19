// Angular
import { Component, ChangeDetectionStrategy, OnInit, OnDestroy, ViewChild, ElementRef, ChangeDetectorRef } from '@angular/core';
import { Router } from '@angular/router';
// RxJS
import { Observable, Subscription, merge, fromEvent, of } from 'rxjs';
// NGRX
import { Store, select } from '@ngrx/store';
// AppState
import { AppState } from '../../../../core/reducers';
// Auth
import { Permission, currentUserPermissions, checkHasUserPermission, User, AuthService } from '../../../../core/auth';
import { MatPaginator, MatSort, MatDialog, MatTableDataSource } from '@angular/material';
import { SelectionModel } from '@angular/cdk/collections';
import { debounceTime, distinctUntilChanged, tap, take, delay, skip } from 'rxjs/operators';
import { QueryParamsModel, LayoutUtilsService, MessageType } from '../../../../core/_base/crud';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ColaboradorFiltro } from '../../../../core/auth/_models/ColaboradorFiltro.model';
import { BasePageComponent } from '../../BasePage.component';
import { TranslateService } from '@ngx-translate/core';

import { MAT_MOMENT_DATE_FORMATS, MomentDateAdapter, MAT_MOMENT_DATE_ADAPTER_OPTIONS } from '@angular/material-moment-adapter';
import { DateAdapter, MAT_DATE_FORMATS, MAT_DATE_LOCALE } from '@angular/material/core';
import { GroupRegister } from '../../../../core/auth/_models/_config/group-register.model';
import { GroupRegisterDataSource } from '../../../../core/auth/_data-sources/_config/group-register.datasource';
import { ConfigService } from '../../../../core/auth/_services/config.service';
import { GroupRegisterFilter } from '../../../../core/auth/_models/_config/group-register-filter.model';
import { RegisterDataSource } from '../../../../core/auth/_data-sources/_config/register.datasource';
import { RegisterFilter } from '../../../../core/auth/_models/_config/register-filter.model';
import { Process } from '../../../../core/auth/_models/process.model';
import { Area } from '../../../../core/auth/_models/area.model';
import { AppService, ParameterService } from '../../../../core/auth/_services';
import { CreateTypeGroupComponent } from '../create-type-group/create-type-group.component';
import { Register } from '../../../../core/auth/_models/_config/register.model';
import { EditTypeGroupComponent } from '../edit-type-group/edit-type-group.component';
import { RemoveTypeGroupComponent } from '../remove-type-group/remove-type-group.component';
import { TypeOccurrenceRecordConfigurationComponent } from '../type-occurrence-record-configuration/type-occurrence-record-configuration.component';
import { IdleService } from '../../../../core/_base/layout/services/idle.service';



@Component({
	selector: 'kt-events',
	templateUrl: './group-register.component.html',
	styleUrls: ['./group-register.component.scss'],
	providers: [
		// The locale would typically be provided on the root module of your application. We do it at
		// the component level here, due to limitations of our example generation script.
		{ provide: MAT_DATE_LOCALE, useValue: 'pt-br' },

		// `MomentDateAdapter` and `MAT_MOMENT_DATE_FORMATS` can be automatically provided by importing
		// `MatMomentDateModule` in your applications root module. We provide it at the component level
		// here, due to limitations of our example generation script.
		{
			provide: DateAdapter,
			useClass: MomentDateAdapter,
			deps: [MAT_DATE_LOCALE, MAT_MOMENT_DATE_ADAPTER_OPTIONS]
		},
		{ provide: MAT_DATE_FORMATS, useValue: MAT_MOMENT_DATE_FORMATS },
	],
})
export class GroupRegisterComponent extends BasePageComponent implements OnInit {
	title: string;
	loading: boolean = false;
	error: string;
	currentGroups: GroupRegisterDataSource;
	currentRegisters: RegisterDataSource;
	registerFilters: RegisterFilter = new RegisterFilter();

	processes: Process[] = [];
	areas: Area[] = [];

	columnGroups = ['id', 'name', 'edit', 'delete'];
	columnRegisters = ['selected', 'tag', 'process', 'area', 'type', 'group'];

	selectedIds: Register[] = [];

	constructor(
		public store: Store<AppState>,
		public router: Router,
		public changeDetectorRef: ChangeDetectorRef,
		private layoutUtilsService: LayoutUtilsService,
		public dialog: MatDialog,
		private fb: FormBuilder,
		auth: AuthService,
		public translate: TranslateService,
		public configService: ConfigService,
		public app: AppService,
		public parameterService: ParameterService,
		public idleService: IdleService,
	)	{
		super(auth, store, translate, router, "CONFIG_GROUPS_REGISTERS" ,[], "CONFIG_GROUPS_REGISTERS", parameterService, idleService)
		this.currentGroups = new GroupRegisterDataSource(this.configService, this.store, new GroupRegisterFilter());
		this.currentRegisters = new RegisterDataSource(this.configService, this.store, this.registerFilters);
	}

	ngOnInit() {
		this.loadListProcess();
	}
	LoadGroups(){
		this.currentGroups = new GroupRegisterDataSource(this.configService, this.store, new GroupRegisterFilter());
	}
	LoadByFilters()
	{
		this.currentRegisters = new RegisterDataSource(this.configService, this.store, this.registerFilters);
	}
	ClearFilters(){
		this.registerFilters = new RegisterFilter();
	}

	loadListProcess(){
		this.app.listProcesses().subscribe(x => {
			this.processes = x;
		},
		error =>
		{
			this.layoutUtilsService.showActionNotification(error.error, MessageType.Create);
		});
	}

	loadListAreas(processId: number){
		this.app.listAreasByProcess(processId).subscribe(x => {
			this.areas = x;
		},
		error =>
		{
			this.layoutUtilsService.showErrorNotification(error, MessageType.Create);
		});
	}

	//Adiciona o id do check box na lista que vai ser enviada para o backend
    onCheckboxChange(data:any){
		var newList: Register[] = [];
		var found = false;
		this.selectedIds.map(x => {
			if(x != data.occurrenceRecordId) {
				newList.push(x);
			}
			else {
				found =  true;
			}
		});
		if(found == false)
			newList.push(data);

		this.selectedIds = newList;
    }
	editGroup(groupId){
		const dialogRef = this.dialog.open(CreateTypeGroupComponent, {width:'800px', minHeight:'400px', data: { groupId: groupId } });
		dialogRef.afterClosed().subscribe(res => {
			this.LoadGroups();
			this.LoadByFilters();
			this.selectedIds = [];
			this.changeDetectorRef.detectChanges();
		});
	}

	createGroup(){
		const dialogRef = this.dialog.open(CreateTypeGroupComponent, {width:'800px', minHeight:'400px', data: { selectedItems: this.selectedIds } });
		dialogRef.afterClosed().subscribe(res => {
			this.LoadGroups();
			this.LoadByFilters();
			this.selectedIds = [];
			this.changeDetectorRef.detectChanges();
		});
	}
	deleteGroup(group){
		const dialogRef = this.dialog.open(RemoveTypeGroupComponent, { data: { type: group } });
		dialogRef.afterClosed().subscribe(res => {
			this.LoadGroups();
			this.LoadByFilters();
			this.selectedIds = [];
			this.changeDetectorRef.detectChanges();
		});

	}

	editRegisterConfiguration(){
		const dialogRef = this.dialog.open(TypeOccurrenceRecordConfigurationComponent, {width:'70%', minHeight:'70%', data: { selectedItems: this.selectedIds } });
		dialogRef.afterClosed().subscribe(res => {
			this.LoadGroups();
			this.LoadByFilters();
			this.selectedIds = [];
			this.changeDetectorRef.detectChanges();
		});
	}
}

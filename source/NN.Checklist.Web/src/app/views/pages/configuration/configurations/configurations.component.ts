import { TypeOccurenceRecord } from './../../../../core/auth/_models/typeOccurrenceRecord.model';
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
import { NewTypeOccurrenceRecordComponent } from '../new-type-occurrence-record/new-type-occurrence-record.component';
import { IdleService } from '../../../../core/_base/layout/services/idle.service';
import { EmailComponent } from '../../../components/email/email.component';

@Component({
  selector: 'kt-configurations',
  templateUrl: './configurations.component.html',
  styleUrls: ['./configurations.component.scss']
})
export class ConfigurationsComponent extends BasePageComponent implements OnInit {
	title: string;
	loading: boolean = false;
	error: string;
	currentRegisters: RegisterDataSource;
	registerFilters: RegisterFilter = new RegisterFilter();

	selectedList = [];

	processes: Process[] = [];
	areas: Area[] = [];

	pdfFile;
	csvFile;

	columnGroups = ['id', 'name', 'edit', 'delete'];
	columnRegisters = ['selected', 'tag', 'process', 'area', 'type'];

	@ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;
	@ViewChild('sort1', { static: true }) sort: MatSort;
	private unsubscribe: Subject<any>;

	OccurrenceResult: TypeOccurenceRecord[] = [];

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
		this.unsubscribe = new Subject();
	}

	ngOnInit() {
		this.loading = true;
		this.initRegisters();
		this.loadListProcess();
	}

	initRegisters(){
		this.loading = true;
		this.paginator.page.pipe(
			tap(() => {
				this.loading = true;
				this.loadRegisters();
			}, error => {
				this.layoutUtilsService.showErrorNotification(error, MessageType.Create);
			}),
			takeUntil(this.unsubscribe),
			finalize(() => {
				this.loading = false;
				this.changeDetectorRef.detectChanges();
			})
		).subscribe();

		//Init DataSource
		this.currentRegisters = new RegisterDataSource(this.configService, this.store, this.registerFilters);


		const entitiesSubscription = this.currentRegisters.entitySubject.pipe(
			skip(1),
			distinctUntilChanged()
		).subscribe(res => {
			this.OccurrenceResult = res;
			this.loading = false;
			this.changeDetectorRef.detectChanges();
		},error =>{
			this.loading = false;
			this.changeDetectorRef.detectChanges();
		});
		this.subscriptions.push(entitiesSubscription);

	}

	loadRegisters(): void {
		this.loading = true;
		this.configService.GetAllRegisters(this.paginator.pageIndex + 1, this.paginator.pageSize, this.registerFilters).subscribe((response: QueryResultsModel) => {
			this.currentRegisters.paginatorTotalSubject.next(response.rowsCount);
			this.currentRegisters.entitySubject.next(response.entities);
			this.currentRegisters.loadingSubject.next(false);
			this.loading = false;
		}, error => {
			this.currentRegisters.paginatorTotalSubject.next(0);
			this.currentRegisters.entitySubject.next(null);
			this.currentRegisters.loadingSubject.next(false);
			this.loading = false;
		});
	}

	applyFilters() {
		this.paginator.pageIndex = 0
		this.loadRegisters();
	}

	LoadByFilters()
	{
		if(this.registerFilters.processId == 0){
			this.registerFilters.processId = null;
		}

		if(this.registerFilters.areaId == 0){
			this.registerFilters.areaId = null;
		}

		if(this.registerFilters.typeId == 0){
			this.registerFilters.typeId = null;
		}

		this.paginator.pageIndex = 0;
		this.loading = true;
		this.loadRegisters();
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
			this.layoutUtilsService.showErrorNotification(error.error);
		});
	}

	loadListAreas(processId){
		if(processId != '0'){

			this.app.listAreasByProcess(processId).subscribe(x => {
				this.areas = x;
			},
			error =>
			{
				this.layoutUtilsService.showErrorNotification(error);
			});
		} else {
			this.areas = [];
			this.registerFilters.areaId = null;
			this.changeDetectorRef.detectChanges();
		}
	}

	editRegisterConfiguration(){
		var ids = [];

		for (let index = 0; index < this.selectedList.length; index++) {
			ids.push(this.selectedList[index].typeOccurrenceRecordId)
		}

		const dialogRef = this.dialog.open(TypeOccurrenceRecordConfigurationComponent, {width:'70%', minHeight:'70%', data: { selectedItems: ids } });
		dialogRef.afterClosed().subscribe(res => {
			this.LoadByFilters();
			this.selectedList = [];
			this.changeDetectorRef.detectChanges();
		});
	}

	newRegister(){
		const dialogRef = this.dialog.open(NewTypeOccurrenceRecordComponent, {width:'70%', minHeight:'70%', data: { new: true } });
		dialogRef.afterClosed().subscribe(res => {
			this.LoadByFilters();
			this.selectedList = [];
			this.changeDetectorRef.detectChanges();
		});
	}

	addRegister(){

		for (let i = 0; i < this.currentRegisters.entitySubject.value.length; i++) {

			let index = this.selectedList.findIndex(x => x.typeOccurrenceRecordId == this.currentRegisters.entitySubject.value[i].typeOccurrenceRecordId);

			if(index == -1){
				if (this.currentRegisters.entitySubject.value[i].selected == true) {
					this.currentRegisters.entitySubject.value[i].selected = false;

					let register = new Register();
					register.area = this.currentRegisters.entitySubject.value[i].area;
					register.areaId = this.currentRegisters.entitySubject.value[i].areaId;
					register.process = this.currentRegisters.entitySubject.value[i].process;
					register.processId = this.currentRegisters.entitySubject.value[i].processId;
					register.tag = this.currentRegisters.entitySubject.value[i].tag;
					register.type = this.currentRegisters.entitySubject.value[i].type;
					register.typeId = this.currentRegisters.entitySubject.value[i].typeId;
					register.typeOccurrenceRecordGroupId = this.currentRegisters.entitySubject.value[i].typeOccurrenceRecordGroupId;
					register.typeOccurrenceRecordId = this.currentRegisters.entitySubject.value[i].typeOccurrenceRecordId;

					this.selectedList.push(register);
				}
			} else {
				this.currentRegisters.entitySubject.value[i].selected = false;
			}
		}
	}

	addRegisterAll(){

		for (let i = 0; i < this.currentRegisters.entitySubject.value.length; i++) {
			let register = new Register();
			register.area = this.currentRegisters.entitySubject.value[i].area;
			register.areaId = this.currentRegisters.entitySubject.value[i].areaId;
			register.process = this.currentRegisters.entitySubject.value[i].process;
			register.processId = this.currentRegisters.entitySubject.value[i].processId;
			register.tag = this.currentRegisters.entitySubject.value[i].tag;
			register.type = this.currentRegisters.entitySubject.value[i].type;
			register.typeId = this.currentRegisters.entitySubject.value[i].typeId;
			register.typeOccurrenceRecordGroupId = this.currentRegisters.entitySubject.value[i].typeOccurrenceRecordGroupId;
			register.typeOccurrenceRecordId = this.currentRegisters.entitySubject.value[i].typeOccurrenceRecordId;

			this.selectedList.push(register);
		}
	}

	removeRegister(){
		for (var i = this.selectedList.length -1; i >= 0; i--){
			if (this.selectedList[i].selected) {
				this.selectedList.splice(i,1);
			}
		}
	}

	removeRegisterAll(){
		for (var i = this.selectedList.length -1; i >= 0; i--){
			this.selectedList.splice(i,1);
		}
	}

	sendEmailPdf(){
		const dialogRef = this.dialog.open(EmailComponent, {width:'350px', data: {  } });
		dialogRef.afterClosed().subscribe(res => {
			this.registerFilters.sendMail = res;
			if(res != "" && res != undefined && res != null)
			{
				this.loading = true;
				this.app.exportTypesOccurrencesRecordsPdf(this.registerFilters).subscribe(x => {
					this.loading = false;
					this.changeDetectorRef.detectChanges();
					this.pdfFile = x;
					this.layoutUtilsService.showActionNotification(this.translate.instant("WAITGENERATION"), MessageType.Create);
				},
				error =>
				{
					this.loading = false;
					this.changeDetectorRef.detectChanges();
					this.layoutUtilsService.showErrorNotification(error, MessageType.Create);
				});
			}
		});
	}

	sendEmailCSV(){
		const dialogRef = this.dialog.open(EmailComponent, {width:'350px', data: {  } });
		dialogRef.afterClosed().subscribe(res => {
			this.registerFilters.sendMail = res;
			if(res != "" && res != undefined && res != null)
			{
				this.loading = true;
				this.app.exportTypesOccurrencesRecordsCSV(this.registerFilters).subscribe(x => {
					this.loading = false;
					this.changeDetectorRef.detectChanges();
					this.layoutUtilsService.showActionNotification(this.translate.instant("WAITGENERATION"), MessageType.Create);
					this.csvFile = x;
				},
				error =>
				{
					this.loading = false;
					this.changeDetectorRef.detectChanges();
					this.layoutUtilsService.showErrorNotification(error, MessageType.Create);
				});
			}
		});
	}
}

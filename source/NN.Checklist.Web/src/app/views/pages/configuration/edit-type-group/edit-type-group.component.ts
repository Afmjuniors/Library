import { Component, OnInit, ViewChild, ChangeDetectorRef, Inject } from '@angular/core';
import { Observable, merge } from 'rxjs';
import { Permission, AuthService, User, currentUser } from '../../../../core/auth';
import { MatPaginator, MatSort, MatDialog } from '@angular/material';
import { LayoutUtilsService, MessageType, QueryParamsModel } from '../../../../core/_base/crud';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { select, Store } from '@ngrx/store';
import { AppState } from '../../../../core/reducers';
import { Router } from '@angular/router';

import { TranslateService } from '@ngx-translate/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { SignatureService } from '../../../../core/auth/_services/signature.service';
import { QAService } from '../../../../core/auth/_services/qa.service';
import { QADetailFilter } from '../../../../core/auth/_models/QADetailFilter.model';
import { Register } from '../../../../core/auth/_models/_config/register.model';
import { ConfigService } from '../../../../core/auth/_services/config.service';
import { EditRegisterGroupDataSource } from '../../../../core/auth/_data-sources/_config/edit-register-group.datasource';
import { EditRegisterGroupFilter } from '../../../../core/auth/_models/_config/edit-register-group-filter.model';

@Component({
	selector: 'apta-edit-type-group',
	templateUrl: './edit-type-group.component.html',
	styleUrls: ['./edit-type-group.component.scss'],
	providers: [SignatureService]
})
export class EditTypeGroupComponent implements OnInit{
	loading: boolean = false;
	user: User = null;
	errors: string[] = [];
	dataSource: EditRegisterGroupDataSource;
	columnRegisters = ['tag', 'process', 'area', 'type', 'remove'];
	description: string;
	id:number;
	selectedItems: any;

	/**
	 * Component constructor
	 *
	 * @param store: Store<AppState>
	 * @param router: Router
	 */
	constructor(
        public store: Store<AppState>,
		public dialog: MatDialog,
		public translateService: TranslateService,
		public changeDetectorRef: ChangeDetectorRef,
		public dialogRef: MatDialogRef<string>,
		public configService: ConfigService,
		private layoutUtilsService: LayoutUtilsService,
		public translate: TranslateService,
        @Inject(MAT_DIALOG_DATA)
		public data: any,
	) {
		this.LoadByFilters();
	}

	/**
	 * On init
	 */
	ngOnInit()
    {
        this.store.pipe(select(currentUser)).subscribe((x:User) => {
			this.user = x;
		},
		error =>
		{
			this.layoutUtilsService.showErrorNotification(error);
		});
	}

	/**
	 * On Destroy
	 */
	ngOnDestroy()
	{
		//this.subscriptions.forEach(el => el.unsubscribe());
	}

	LoadByFilters(){
		let edt = new EditRegisterGroupFilter();
		edt.groupId = this.data.groupId;
		this.dataSource = new EditRegisterGroupDataSource(this.configService, this.store, edt);
	}
	closeModal()
	{
		this.dialog.closeAll();
	}
	SaveGroup(){
		if(this.description != undefined && this.description != null && this.description != "")
		{
			var ids: number[] = [];
			this.data.selectedItems.map(x=> {
				ids.push(x.typeOccurrenceRecordId);
			});
			this.configService.InsertRegisterGroup({
				groupId: this.id,
				typeOccurrencesRecordsIds: ids,
				groupDescription: this.description
			}).subscribe(x =>
			{
				this.layoutUtilsService.showActionNotification(x.message, MessageType.Create);
				this.dialogRef.close("success");
			},
			error =>
			{
				this.layoutUtilsService.showErrorNotification(error);
			});
		}
		else {
			this.layoutUtilsService.showActionNotification(this.translate.instant("AUTH.CONFIG.DESCRIPTION_EMPTY"), MessageType.Create);
			this.dialogRef.close("false");

		}
	}
	deleteGroup(group){
		this.configService.DeleteRegisterFromGroup({typeOccurrenceRecordId: group.typeOccurrenceRecordId}).subscribe(x => {
			this.LoadByFilters();
			this.changeDetectorRef.detectChanges();
		},
		error =>
		{
			this.layoutUtilsService.showErrorNotification(error);
		});
	}
}

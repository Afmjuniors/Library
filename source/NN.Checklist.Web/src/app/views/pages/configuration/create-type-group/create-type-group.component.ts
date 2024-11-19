import { Component, OnInit, ViewChild, ChangeDetectorRef, Inject } from '@angular/core';
import { Observable, merge } from 'rxjs';
import { Permission, AuthService, User, currentUser } from '../../../../core/auth';
import { MatPaginator, MatSort, MatDialog } from '@angular/material';
import { LayoutUtilsService, MessageType, QueryParamsModel, QueryResultsModel } from '../../../../core/_base/crud';
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
import { EditRegisterGroupFilter } from '../../../../core/auth/_models/_config/edit-register-group-filter.model';

@Component({
	selector: 'apta-create-type-group',
	templateUrl: './create-type-group.component.html',
	styleUrls: ['./create-type-group.component.scss'],
	providers: [SignatureService]
})
export class CreateTypeGroupComponent implements OnInit{
	loading: boolean = false;
	user: User = null;
	errors: string[] = [];
	selectedIds: Register[] = [];
	filter: EditRegisterGroupFilter = new EditRegisterGroupFilter();

	columnRegisters = [ 'remove', 'tag', 'process', 'area', 'type'];
	description: string;
	id:number;
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
		public dialogRef: MatDialogRef<string>,
		public configService: ConfigService,
		private layoutUtilsService: LayoutUtilsService,
		public translate: TranslateService,
        @Inject(MAT_DIALOG_DATA)
		public data: any,
		public detector: ChangeDetectorRef
	) {	}

	/**
	 * On init
	 */
	ngOnInit()
    {
		if (this.data.groupId > 0) {
			this.configService.getGroupById(this.data.groupId).subscribe(res =>{
				this.id = res.typeOccurrenceRecordGroupId;
				this.description = res.description;
				this.filter.groupId = this.data.groupId;
				this.configService.GetRegistersByGroupId(1, 100, this.filter).subscribe((response: QueryResultsModel) => {
					this.selectedIds = response.entities;
				});
			},
			error =>
			{
				this.layoutUtilsService.showErrorNotification(error);
			})
		} else {
			this.selectedIds = this.data.selectedItems;
		}
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

	deleteRegister(register){
		var index = this.selectedIds.findIndex(x => x.typeOccurrenceRecordId == register.typeOccurrenceRecordId);
		if (index >= 0)
		{
			this.selectedIds.splice(index, 1);
			this.detector.detectChanges();
		}
	}
}

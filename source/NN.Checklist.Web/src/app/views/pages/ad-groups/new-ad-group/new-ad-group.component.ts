import { Permission } from './../../../../core/auth/_models/permission.model';
import { AdGroup } from './../../../../core/auth/_models/adGroup.model';
import { Component, Inject, OnInit } from '@angular/core';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { AppService, AuthService } from './../../../../core/auth/_services';
import { LayoutUtilsService, MessageType } from './../../../../core/_base/crud';
import { SignatureComponent } from '../../../../views/components/signature/signature.component';
import { TranslateService } from '@ngx-translate/core';
import { duration } from 'moment';

@Component({
	selector: 'kt-new-ad-group',
	templateUrl: './new-ad-group.component.html',
	styleUrls: ['./new-ad-group.component.scss']
})
export class NewAdGroupComponent implements OnInit {
	loading: boolean = false;
	adGroup: AdGroup = new AdGroup();
	adGroupId: number = 0;
	permissions: Permission[] = [];

	permission: Permission = new Permission();

	visible = true;
	selectable = true;
	removable = true;
	edit = false;

	remainingText: number = 8000;
	comments: string = "";

	constructor(
		public dialogRef: MatDialogRef<NewAdGroupComponent>,
		@Inject(MAT_DIALOG_DATA)
		public data: any,
		private app: AppService,
		public auth: AuthService,
		public dialog: MatDialog,
		private layoutUtilsService: LayoutUtilsService,
		public translateService: TranslateService,
	) { }

	ngOnInit() {
		this.loadListPermissions();
		if (this.data != null) {
			this.adGroupId = this.data.adGroupId;
			this.loadAdGroup();
		}
	}

	loadAdGroup(){
		if (this.adGroupId > 0) {
			this.app.getAdGroup(this.adGroupId).subscribe(x => {
				this.adGroup = x;
			},error =>{
				this.loading = false;
			});
		}
	}

	loadListPermissions(){
		this.auth.getAllPermissions().subscribe(x => {
			this.permissions = x;
		},
		error =>
		{
			this.layoutUtilsService.showErrorNotification(error, MessageType.Create);
		});
	}

	prepareAdGroup(): AdGroup {
		const _AdGroup = new AdGroup();

		_AdGroup.adGroupId = this.adGroup.adGroupId;
		_AdGroup.administrator = this.adGroup.administrator;

		return _AdGroup;
	}

	addPermission() {

		let index;

		if(this.adGroup.permissions){
			index = this.adGroup.permissions.findIndex(x => x.permissionId == this.permission.permissionId);
		} else{
			index = -1;
		}

		if(index == -1){

			var _Permission: Permission = new Permission()

			_Permission.permissionId = this.permission.permissionId;
			_Permission.tag = this.permission.tag;
			_Permission.description = this.permission.description;

			if(!this.adGroup.permissions){
				this.adGroup.permissions = [];
			}
			this.adGroup.permissions.push(_Permission);
			this.permission = new Permission();
		}
		this.permission.clear();
	}


	save(){
		
		if(this.adGroup.adGroupId > 0){
			this.update();
		} else{
			this.insert();
		}
	}

	insert(){
		this.loading = true;
		this.app.insertAdGroup(this.adGroup, this.comments).subscribe(x =>{
			this.loading = false;
			this.dialogRef.close("success");
		},
		error => {
			this.loading = false;
			this.layoutUtilsService.showErrorNotification(error);
		});
	}

	update(){
		this.loading = true;
		this.app.updateAdGroup(this.adGroup, this.comments).subscribe(x =>{
			this.loading = false;
			this.dialogRef.close("success");
		},
		error => {
			this.loading = false;
			this.layoutUtilsService.showErrorNotification(error);
		});
	}

	remove(permission: Permission): void {
		this.edit = false;
		this.permission.clear();
		this.adGroup.permissions.forEach(_permission => {
			if(_permission.permissionId === permission.permissionId && _permission.description === permission.description){
				const index = this.adGroup.permissions.indexOf(_permission);

				if (index >= 0) {
					this.adGroup.permissions.splice(index, 1);
				}

			}
		});

	}
	
	sign()
	{
		if(this.comments == null || this.comments.trim().length == 0){
			this.layoutUtilsService.showActionNotification(this.translateService.instant("COMMENTS_NOT_PROVIDED"), MessageType.Update,10000);
			return;
		}

		this.dialog.open(SignatureComponent, {
			minHeight: '300px',
			width: '400px',
			data: true
		}).afterClosed()
		.subscribe(x => {
			if(x != '' && x != undefined){
				this.save();
			}
		});
	}

	valueChange(value) {
		this.remainingText = 8000 - value;
	}

	closeModal()
	{
		this.dialog.closeAll();
	}
}

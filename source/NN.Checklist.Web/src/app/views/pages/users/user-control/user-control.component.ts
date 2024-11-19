import { ChangeDetectorRef, Component, Inject, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { SignatureComponent } from '../../../components/signature/signature.component';
import { User } from '../../../../core/auth';
import { AppService } from '../../../../core/auth/_services';
import { LayoutUtilsService, MessageType } from '../../../../core/_base/crud';

@Component({
	selector: 'kt-user-control',
	templateUrl: './user-control.component.html',
	styleUrls: ['./user-control.component.scss']
})
export class UserControlComponent implements OnInit {
	user: User = new User();
	result: boolean = false;
	loading: boolean = false;

	constructor(
		public dialogRef: MatDialogRef<UserControlComponent>,
		@Inject(MAT_DIALOG_DATA)
		public data: any,
		private app: AppService,
		public fb: FormBuilder,
		private layoutUtilsService: LayoutUtilsService,
		public dialog: MatDialog,
		private ref: ChangeDetectorRef
	) { }

	ngOnInit() {
		this.user = this.data.user;
	}

	closeDialog() {
		this.dialogRef.close();
	}

	activate(newStatus){
		this.loading = true;
		let user: User = new User();
		user.userId = this.user.userId;
		user.active = newStatus;

		this.app.activateUser(user).subscribe(res =>{
			this.loading = false;
			this.loading = false;
			this.closeDialog();
		},
		error =>
		{
			this.loading = false;
			this.layoutUtilsService.showErrorNotification(error, MessageType.Create);
		});
	}

	sign(newStatus)
	{
		this.dialog.open(SignatureComponent, {
			minHeight: '300px',
			width: '400px',
			data: this.result
		}).afterClosed()
		.subscribe(x => {
			if(x != '' && x != undefined){
				this.activate(newStatus);
			}
		});
	}
}

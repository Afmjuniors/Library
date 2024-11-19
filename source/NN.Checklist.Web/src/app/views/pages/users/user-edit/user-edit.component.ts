import { ChangeDetectorRef, Component, Inject, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { User } from '../../../../core/auth';
import { AppService } from '../../../../core/auth/_services';
import { LayoutUtilsService } from '../../../../core/_base/crud';

@Component({
	selector: 'kt-user-edit',
	templateUrl: './user-edit.component.html',
	styleUrls: ['./user-edit.component.scss']
})
export class UserEditComponent implements OnInit {
	loading: boolean = false;
	user: User = new User();

	constructor(
		public dialogRef: MatDialogRef<UserEditComponent>,
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

	showLoading(loading) {
		this.loading = loading;
	}
}

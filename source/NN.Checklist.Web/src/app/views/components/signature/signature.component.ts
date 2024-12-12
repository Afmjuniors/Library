import { Component, OnInit,   Inject } from '@angular/core';
import {   currentUser } from '../../../core/auth';
import {   MatDialog } from '@angular/material';
import { LayoutUtilsService } from '../../../core/_base/crud';
import { select, Store } from '@ngrx/store';
import { AppState } from '../../../core/reducers';

import { TranslateService } from '@ngx-translate/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { SignatureService } from '../../../core/auth/_services/signature.service';
import { Signature } from '../../../core/auth/_models/signature.model';
import { User } from '../../../core/auth/_models/user.model';

@Component({
	selector: 'td-signature',
	templateUrl: './signature.component.html',
	styleUrls: ['./signature.component.scss'],
	providers: [SignatureService]
})
export class SignatureComponent implements OnInit{

	loading: boolean = false;
	permissionTag: string = '';
	username: string = '';
	password: string = '';
	errors: string[] = [];
	user: User = null;
	showPassword: boolean = false;

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
		public signatureService: SignatureService,
		private layoutUtilsService: LayoutUtilsService,
        @Inject(MAT_DIALOG_DATA)
		public data: any
	) {

	}

	/**
	 * On init
	 */
	ngOnInit()
	{
		this.store.pipe(select(currentUser)).subscribe((x:User) => {
			this.user = x;
			this.username = this.user.userAD;
		},
		error =>{			
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

	validateSignature()
	{
		this.errors = [];
		if(this.username.length == 0)
		{
			this.errors.push(this.translateService.instant("AUTH.SIGNATURE.USER_ERROR"))
		}
		if(this.password.length == 0)
		{
			this.errors.push(this.translateService.instant("AUTH.SIGNATURE.PASS_ERROR"));
		}
		if(this.errors.length == 0)
		{
			var sig : Signature = new Signature();
			sig.username = this.user.userAD;
			sig.password = this.password;
			sig.validationDate = new Date();
			sig.cryptData = '';
			sig.result = this.data;

			var request = this.signatureService.ValidateSignature(sig).subscribe((x:Signature) => {
				this.dialogRef.close(x.cryptData);
			}, responseError => {
				this.errors.push((typeof responseError.error === 'string') ? responseError.error : responseError.message);
			});

		}
	}

	closeModal()
	{
		this.dialogRef.close();
	}
	
	togglePasswordVisibility(): void {
		this.showPassword = !this.showPassword;
	}
}

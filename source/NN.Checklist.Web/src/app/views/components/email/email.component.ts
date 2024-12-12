import { Component, OnInit} from '@angular/core';

import {  MatDialog } from '@angular/material';


import { TranslateService } from '@ngx-translate/core';
import { MatDialogRef } from '@angular/material/dialog';

@Component({
	selector: 'td-email',
	templateUrl: './email.component.html',
	styleUrls: ['./email.component.scss'],
})
export class EmailComponent implements OnInit{

	loading: boolean = false;
    email: string = '';
	errors: string[] = [];
	placeholder: string;

	/**
	 * Component constructor
	 *
	 * @param store: Store<AppState>
	 * @param router: Router
	 */
	constructor(
		public dialog: MatDialog,
		public translateService: TranslateService,
		public dialogRef: MatDialogRef<string>,
	) {
		this.placeholder = translateService.instant("PLACEHOLDER_SEND_MAIL");
	}

	/**
	 * On init
	 */
	ngOnInit()
	{

	}

	/**
	 * On Destroy
	 */
	ngOnDestroy()
	{
		//this.subscriptions.forEach(el => el.unsubscribe());
	}
    validateEmail(email)
    {
        var re = /\S+@\S+\.\S+/;
        return re.test(email);
    }
	SendMail()
	{
        this.errors = [];
		var email = this.email + "@novonordisk.com";
        if(!this.validateEmail(email))
        {
			this.errors.push(this.translateService.instant("AUTH.EMAIL.EMAIL_INVALID"))
        }
        if(this.errors.length == 0)
            this.dialogRef.close(email);
	}

	closeModal()
	{
		this.dialog.closeAll();
	}
}

import { Component, OnInit, ViewChild, ChangeDetectorRef, Inject } from '@angular/core';
import { Observable, merge } from 'rxjs';
import { Permission, AuthService } from '../../../core/auth';
import { MatPaginator, MatSort, MatDialog } from '@angular/material';
import { LayoutUtilsService, MessageType, QueryParamsModel } from '../../../core/_base/crud';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { Store } from '@ngrx/store';
import { AppState } from '../../../core/reducers';
import { Router } from '@angular/router';

import { TranslateService } from '@ngx-translate/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
	selector: 'apta-email',
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

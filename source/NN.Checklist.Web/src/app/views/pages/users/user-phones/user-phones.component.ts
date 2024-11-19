import { ChangeDetectorRef, Component, Inject, Input, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { UserPhone } from '../../../../core/auth/_models/userPhone.model';
import { User } from '../../../../core/auth';
import { AppService } from '../../../../core/auth/_services';
import { LayoutUtilsService } from '../../../../core/_base/crud';
import { Country } from '../../../../core/auth/_models/country.model';

const CELLPHONE = '(00) 0 0000-0000';
const LANDLINE = '(00) 0000-0000';
@Component({
	selector: 'kt-user-phones',
	templateUrl: './user-phones.component.html',
	styleUrls: ['./user-phones.component.scss']
})
export class UserPhonesComponent implements OnInit {
	loading: boolean = false;
	user: User = new User();
	phones: UserPhone[] = [];
	phone: UserPhone = new UserPhone();
	countries: Country[] = [];
	
	mask = LANDLINE;
	previousLength = 0;

	constructor(
		public dialogRef: MatDialogRef<UserPhonesComponent>,
		@Inject(MAT_DIALOG_DATA)
		public data: any,
		private app: AppService,
		public fb: FormBuilder,
		private layoutUtilsService: LayoutUtilsService,
		public dialog: MatDialog,
		private ref: ChangeDetectorRef
	) { }


	ngOnInit() {
		this.listCountries();
		this.user = this.data.user;
		this.listUserPhones();
	}

	listUserPhones(){
		this.loading = true;
		if(this.user.userId > 0){
			this.app.listPhonesNumbersByUser(this.user.userId).subscribe(res =>{
				this.phones = res;
				this.loading = false;
			},
			error =>
			{
				this.loading = false;
				this.layoutUtilsService.showErrorNotification(error);
			})
		}
	}

	listCountries(){
		this.app.listCountries().subscribe(res =>{
			this.countries = res;
		},
		error =>
		{
			this.layoutUtilsService.showErrorNotification(error);
		})
	}

	addPhone(){
		this.loading = true;
		this.phone.userId = this.user.userId;
		this.app.insertUserPhone(this.phone).subscribe(res =>{
			this.phone.clear();
			this.listUserPhones();
		},
		error =>
		{
			this.loading = false;
			this.layoutUtilsService.showErrorNotification(error);
		})
	}

	removePhone(item){
		this.loading = true;
		this.app.removeUserPhone(item).subscribe(res =>{
			this.phone.clear();
			this.listUserPhones();
		},
		error =>
		{
			this.loading = false;
			this.layoutUtilsService.showErrorNotification(error);
		})
	}
	
	onPhoneChanged(phone) {
		if (phone.inputType == 'insertText' && this.previousLength < 11 )
		{
			this.previousLength ++;
		}
		else if ((phone.inputType == 'deleteContentBackward' || phone.inputType == 'deleteContentForward') && this.previousLength > 0 )
		{
			this.previousLength --;
		}

		this.subscribeOnChangeFoneMask();
	}

	subscribeOnChangeFoneMask() {
		var self = this;
		const onChangeFoneMask = (value: string) => Promise.resolve().then(() => {
			if (typeof value === "string" && self.previousLength > 10) {
				this.mask = CELLPHONE
			 } else {
			 	this.mask = LANDLINE;
			 }
		});
		onChangeFoneMask(this.phone.number);
	}
}

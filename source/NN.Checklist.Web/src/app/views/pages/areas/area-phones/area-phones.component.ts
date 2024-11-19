import { AreaPhone } from './../../../../core/auth/_models/areaPhone.model';
import { ChangeDetectorRef, Component, Inject, OnInit } from '@angular/core';
import { Area } from './../../../../core/auth/_models/area.model';
import { Country } from './../../../../core/auth/_models/country.model';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { AppService } from './../../../../core/auth/_services';
import { FormBuilder } from '@angular/forms';
import { LayoutUtilsService } from './../../../../core/_base/crud';

const CELLPHONE = '(00) 0 0000-0000';
const LANDLINE = '(00) 0000-0000';
@Component({
	selector: 'apta-area-phones',
	templateUrl: './area-phones.component.html',
	styleUrls: ['./area-phones.component.scss']
})
export class AreaPhonesComponent implements OnInit {
	loading: boolean = false;
	area: Area = new Area();
	phones: AreaPhone[] = [];
	phone: AreaPhone = new AreaPhone();
	countries: Country[] = [];

	mask = LANDLINE;
	previousLength = 0;

	constructor(
		public dialogRef: MatDialogRef<AreaPhonesComponent>,
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
		this.area = this.data.area;
		this.listAreaPhones();
	}

	listAreaPhones(){
		if(this.area.areaId > 0){
			this.loading = true;
			this.app.listPhonesNumbersByArea(this.area.areaId).subscribe(res =>{
				this.phones = res;
				this.loading = false;
			},
			error =>{
				this.loading = false;
				this.layoutUtilsService.showErrorNotification(error);
			});
		}
	}

	listCountries(){
		this.app.listCountries().subscribe(res =>{
			this.countries = res;
		},
		error =>{
			this.layoutUtilsService.showErrorNotification(error);
		})
	}

	addPhone(){
		this.loading = true;
		this.phone.areaId = this.area.areaId;
		this.app.insertAreaPhone(this.phone).subscribe(res =>{
			this.phone.clear();
			this.listAreaPhones();
		},
		error =>{
			this.loading = false;
			this.layoutUtilsService.showErrorNotification(error);
		})
	}

	removePhone(item){
		this.loading = true;
		this.app.removeAreaPhone(item).subscribe(res =>{
			this.phone.clear();
			this.listAreaPhones();
		},
		error =>{
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

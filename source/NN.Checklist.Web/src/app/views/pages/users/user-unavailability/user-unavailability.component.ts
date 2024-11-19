import { NGX_MAT_DATE_FORMATS } from '@angular-material-components/datetime-picker';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { LayoutUtilsService } from '../../../../core/_base/crud';
import { UserUnavailability } from '../../../../core/auth/_models/userUnavailability.model';
import { AppService } from '../../../../core/auth/_services';

const DATE_TIME_FORMAT = {
	parse: {
	  dateInput: 'YYYY-MM-DD HH:mm:ss',
	},
	display: {
	  dateInput: 'YYYY-MM-DD HH:mm:ss',
	  monthYearLabel: 'YYYY MMM',
	  dateA11yLabel: 'DD',
	  monthYearA11yLabel: 'YYYY MMM',
	  enableMeridian: false,
	  useUtc: true,
	}
};

@Component({
	selector: 'apta-user-unavailability',
	templateUrl: './user-unavailability.component.html',
	styleUrls: ['./user-unavailability.component.scss'],
	providers: [
		{provide: NGX_MAT_DATE_FORMATS, useValue: DATE_TIME_FORMAT},
	],
})
export class UserUnavailabilityComponent implements OnInit {
	@Input() public userId: number;
	unavailabilities: UserUnavailability[] = [];
	unavailability: UserUnavailability = new UserUnavailability();
	@Output() loading = new EventEmitter<any>();

	//Datetime
	public date: moment.Moment;
	public showSpinners = true;
	public showSeconds = true;
	public touchUi = false;
	public enableMeridian = false;
	public minDate: moment.Moment;
	public maxDate: moment.Moment;
	public stepHour = 1;
	public stepMinute = 1;
	public stepSecond = 1;
	public date2: moment.Moment;

	constructor(
		public app: AppService,
		private layoutUtilsService: LayoutUtilsService,
	) { }

	ngOnInit() {
		this.listUserUnavailabilities();
	}

	listUserUnavailabilities(){
		this.loading.emit(true);
		if(this.userId > 0){
			this.app.listUnavailabilitiesByUser(this.userId).subscribe(res =>{
				this.unavailabilities = res;
				this.loading.emit(false);
			},
			error =>
			{
				this.loading.emit(false);
				this.layoutUtilsService.showErrorNotification(error);
			})
		}
	}

	removeUnavailability(item){
		this.loading.emit(true);
		this.app.removeUserUnavailability(item).subscribe(res =>{
			this.unavailability.clear();
			this.listUserUnavailabilities();
		},
		error =>
		{
			this.loading.emit(false);
			this.layoutUtilsService.showErrorNotification(error);
		})
	}

	addUnavailability(){
		this.loading.emit(true);
		this.unavailability.userId = this.userId;
		this.app.insertUserUnavalability(this.unavailability).subscribe(res =>{
			this.unavailability.clear();
			this.listUserUnavailabilities();
		},
		error =>
		{
			this.loading.emit(false);
			this.layoutUtilsService.showErrorNotification(error);
		})
	}
}

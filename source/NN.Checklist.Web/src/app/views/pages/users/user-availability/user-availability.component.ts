import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { MatDialog } from '@angular/material';
import { LayoutUtilsService } from '../../../../core/_base/crud';
import { UserAvailabilities } from '../../../../core/auth/_models/userAvailabilities.model';
import { AppService } from '../../../../core/auth/_services';

@Component({
	selector: 'apta-user-availability',
	templateUrl: './user-availability.component.html',
	styleUrls: ['./user-availability.component.scss'],
})
export class UserAvailabilityComponent implements OnInit {
	@Input() public userId: number;
	availabilities: UserAvailabilities[] = [];
	availability: UserAvailabilities = new UserAvailabilities();
	@Output() loading = new EventEmitter<any>();

	daysOfWeek:[
		{id: 1, description:"Domingo"},
		{id: 2, description:"Segunda-feira"},
		{id: 3, description:"Terça-feira"},
		{id: 4, description:"Quarta-feira"},
		{id: 5, description:"Quinta-feira"},
		{id: 6, description:"Sexta-feira"},
		{id: 7, description:"Sábado"}
	]

	constructor(
		public app: AppService,
		private layoutUtilsService: LayoutUtilsService,
	) { }

	ngOnInit() {
		this.listUserAvailabilities();
	}

	listUserAvailabilities(){
		this.loading.emit(true);
		if(this.userId > 0){
			this.app.listAvailabilitiesByUser(this.userId).subscribe(res =>{
				this.availabilities = res;
				this.loading.emit(false);
			},
			error => {
				this.loading.emit(false);
				this.layoutUtilsService.showErrorNotification(error);
			});
		}
	}

	addAvailability(){
		this.loading.emit(true);
		this.availability.userId = this.userId;
		this.app.insertUserAvalability(this.availability).subscribe(res =>{
			this.availability.clear();
			this.listUserAvailabilities();
		},
		error => {
			this.loading.emit(false);
			this.layoutUtilsService.showErrorNotification(error);
		});
	}

	removeAvailability(item){
		this.loading.emit(true);
		this.app.removeUserAvalability(item).subscribe(res =>{
			this.availability.clear();
			this.listUserAvailabilities();
		},
		error => {
			this.loading.emit(false);
			this.layoutUtilsService.showErrorNotification(error);
		});
	}

}

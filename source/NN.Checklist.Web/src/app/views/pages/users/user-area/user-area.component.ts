import { ChangeDetectorRef, Component, Inject, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { Area } from '../../../../core/auth/_models/area.model';
import { Process } from '../../../../core/auth/_models/process.model';
import { User } from '../../../../core/auth';
import { AppService } from '../../../../core/auth/_services';
import { LayoutUtilsService, MessageType } from '../../../../core/_base/crud';
import { AdGroupUser } from '../../../../core/auth/_models/adGroupUser.model';
import { AdGroupUserArea } from '../../../../core/auth/_models/adGroupUserArea.model';

@Component({
	selector: 'td-user-area',
	templateUrl: './user-area.component.html',
	styleUrls: ['./user-area.component.scss']
})
export class UserAreaComponent implements OnInit {
	loading: boolean = false;
	user: User = new User();
	adGroupsUser: AdGroupUser[] = [];
	process: Process = new Process();
	adGroupUserArea: AdGroupUserArea = new AdGroupUserArea();

	processes: Process[] = [];
	areas: Area[] = [];

	areasAdGroup: Area[] = [];
	adGroupUser: AdGroupUser = new AdGroupUser();

	constructor(
		public dialogRef: MatDialogRef<UserAreaComponent>,
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
		this.loadListProcess();
		this.loadListAdGroups();
	}

	loadListProcess(){
		this.app.listProcesses().subscribe(x => {
			this.processes = x;
		},
		error =>
		{
			this.layoutUtilsService.showErrorNotification(error);
		});
	}

	loadListAreas(processId: number){
		this.loading = true;
		this.app.listAreasByProcess(processId).subscribe(x => {
			this.loading = false;
			this.areas = x;
		},
		error =>{
			this.loading = false;
			this.layoutUtilsService.showErrorNotification(error);
		});
	}

	loadListAdGroups(){
		this.loading = true;
		this.app.listAdGroupsByUser(this.user.userId).subscribe(x => {
			this.adGroupsUser = x;
			this.loading = false;
		},
		error =>
		{
			this.loading = false;
			this.layoutUtilsService.showErrorNotification(error, MessageType.Create);
		});
	}

	addAdGroupUserArea(){
		this.loading = true;
		this.adGroupUserArea.adGroupUserId = this.adGroupUser.adGroupUserId;
		this.app.insertAdGroupUserArea(this.adGroupUserArea).subscribe(res =>{
			this.loading = false;
			this.listAreasByAdGroup();
			this.adGroupUserArea.clear();
		},
		error =>
		{
			this.loading = false;
			this.layoutUtilsService.showErrorNotification(error);
		})
	}

	removeAdGroupUserArea(item: Area){
		this.loading = true;
		let data = new AdGroupUserArea();

		data.areaId = item.areaId;
		data.adGroupUserId = this.adGroupUser.adGroupUserId;

		this.app.removeAdGroupUserArea(data).subscribe(res =>{
			this.loading = false;
			this.adGroupUserArea.clear();
			this.listAreasByAdGroup();
		},
		error =>
		{
			this.loading = false;
			this.layoutUtilsService.showErrorNotification(error);
		})
	}

	selectAdGroup(ad: AdGroupUser){
		this.adGroupUser = ad;
		this.listAreasByAdGroup();
	}

	listAreasByAdGroup(){
		this.loading = true;
		this.app.listAreasByAdGroupUser(this.adGroupUser.adGroupUserId).subscribe(res =>{
			this.areasAdGroup = res;
			this.loading = false;
		},
		error =>
		{
			this.loading = false;
			this.layoutUtilsService.showErrorNotification(error);
		})
	}
}

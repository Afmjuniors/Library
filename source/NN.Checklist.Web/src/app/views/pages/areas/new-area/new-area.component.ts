import { ChangeDetectorRef, Component, Inject, OnInit } from '@angular/core';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { Store } from '@ngrx/store';
import { TranslateService } from '@ngx-translate/core';
import { AreaPhone } from './../../../../core/auth/_models/areaPhone.model';
import { Area } from './../../../../core/auth/_models/area.model';
import { Process } from './../../../../core/auth/_models/process.model';
import { AppService } from './../../../../core/auth/_services';
import { AppState } from './../../../../core/reducers';
import { LayoutUtilsService, MessageType } from './../../../../core/_base/crud';
import { Country } from './../../../../core/auth/_models/country.model';
import { SignatureComponent } from '../../../../views/components/signature/signature.component';

@Component({
	selector: 'kt-new-area',
	templateUrl: './new-area.component.html',
	styleUrls: ['./new-area.component.scss']
})
export class NewAreaComponent implements OnInit {
	loading: boolean = false;
	processes: Process[] = [];
	area: Area = new Area();
	phone: AreaPhone = new AreaPhone();
	countries: Country[] = [];

	remainingText: number = 8000;
	comments: string = "";

	constructor(
		public store: Store<AppState>,
		public dialog: MatDialog,
		public translateService: TranslateService,
		public dialogRef: MatDialogRef<string>,
		public app: AppService,
		private layoutUtilsService: LayoutUtilsService,
		public translate: TranslateService,
		public detector: ChangeDetectorRef,
		@Inject(MAT_DIALOG_DATA)
		public data: any,
	) { }

	ngOnInit() {
		this.loadListProcess();
		if(this.data.area != null){
			this.area = this.data.area;
		}
	}

	loadListProcess(){
		this.app.listProcesses().subscribe(x => {
			this.processes = x;
		},
		error =>
		{
			this.layoutUtilsService.showErrorNotification(error, MessageType.Create);
		});
	}

	save(){
		this.loading = true;
		if(this.area.areaId > 0){
			this.update();
		} else{
			this.insert();
		}
	}

	insert(){
		this.app.insertArea(this.area, this.comments).subscribe(x =>{
			this.loading = false;
			this.dialogRef.close("success");
		},
		error =>{
			this.loading = false;
			this.layoutUtilsService.showErrorNotification(error);
		})
	}

	update(){
		this.app.updateArea(this.area, this.comments).subscribe(x =>{
			this.loading = false;
			this.dialogRef.close("success");
		},
		error =>{
			this.loading = false;
			this.layoutUtilsService.showErrorNotification(error);
		})
	}

	closeDialog()
	{
		this.dialog.closeAll();
	}

	sign()
	{
		if(this.comments == null || this.comments.trim().length == 0){
			this.layoutUtilsService.showActionNotification(this.translateService.instant("COMMENTS_NOT_PROVIDED"), 10000);
			return;
		}

		this.dialog.open(SignatureComponent, {
			minHeight: '300px',
			width: '400px',
			data: true
		}).afterClosed()
		.subscribe(x => {
			if(x != '' && x != undefined){
				this.save();
			}
		});
	}

	valueChange(value) {
		this.remainingText = 8000 - value;
	}
}

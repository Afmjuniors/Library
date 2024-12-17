import { State } from '../../../core/auth/_models/state.model';
import { TypeSeverity } from '../../../core/auth/_models/typeSeverity.model';
import { SystemNode } from '../../../core/auth/_models/systemNode.model';
import { ChangeDetectorRef, Component, Inject, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA, ProgressSpinnerMode, ThemePalette } from '@angular/material';
import { AppService } from '../../../core/auth/_services';
import { LayoutUtilsService, MessageType } from '../../../core/_base/crud';
import { TypeEventCategory } from '../../../core/auth/_models/typeEventCategory.model';
import { TranslateService } from '@ngx-translate/core';
import { NGX_MAT_DATE_FORMATS } from '@angular-material-components/datetime-picker';
import { SignatureComponent } from '../signature/signature.component';
import { Checklist } from '../../../core/auth/_models/checklist.model';
import { TypeComponent } from '../../../core/auth/_models/typeComponent.model';
import { ChecklistFilter } from '../../../core/auth/_models/checklistFilter.model';

const DATE_TIME_FORMAT = {
	parse: {
		dateInput: 'YYYY-MM-DD HH:mm:ss',
	},
	display: {
		dateInput: 'YYYY-MM-DD HH:mm:ss',
		monthYearLabel: 'YYYY MMM',
		dateA11yLabel: 'DD',
		monthYearA11yLabel: 'YYYY MMM',
		enableMeridian: true,
		useUtc: true,
	}
};

@Component({
	selector: 'kt-new-checklist-form',
	templateUrl: './newChecklistForm.component.html',
	styleUrls: ['./newChecklistForm.component.scss'],
	providers: [
		{ provide: NGX_MAT_DATE_FORMATS, useValue: DATE_TIME_FORMAT },
	],
})
export class NewChecklistForm implements OnInit {
	checklist: Checklist = new Checklist();
	typeComponent: TypeComponent[] = [];

	typeId: number = 0;

	systemNodes: SystemNode[] = [];
	systemStates: State[] = [];
	filter: ChecklistFilter = new ChecklistFilter();
	loadingTags: boolean = false;

	comments: string = "";
	remainingText: number = 8000;

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

	public title:string;
	public checklistDropDown:string;
	public versions:string;

	constructor(
		public dialogRef: MatDialogRef<NewChecklistForm>,
		@Inject(MAT_DIALOG_DATA)
		public data: any,
		private app: AppService,
		public fb: FormBuilder,
		private layoutUtilsService: LayoutUtilsService,
		public dialog: MatDialog,
		private ref: ChangeDetectorRef,
		public translate: TranslateService,
	) { }

	ngOnInit() {
		if (this.data != null) {
			this.typeId = this.data.type;
		}
		this.title  = this.translate.instant("MENU.CHECKLIST");
		this.checklistDropDown  = this.translate.instant("FILTERS.CHECKLIST");
		this.versions  = this.translate.instant("FILTERS.VERSIONS");
		this.loadListSystemNodes();
		this.loadListStates();
	}

	loadListStates() {
		this.app.listStates().subscribe(x => {
			this.systemStates = x;
		},
			error => {
				this.layoutUtilsService.showErrorNotification(error, MessageType.Create);
			});
	}


	loadListSystemNodes() {
		this.app.listSystemNodes().subscribe(x => {
			this.systemNodes = x;
		},
			error => {
				this.layoutUtilsService.showErrorNotification(error, MessageType.Create);
			});
	}

	validate(): boolean {

		//TODO
		return true;
	}

	valueChange(value) {
		this.remainingText = 8000 - value;
	}

	sign()
	{
		if (!this.validate()) {
			return;
		}

		if (this.checklist.checklistId <= 0) {
			this.layoutUtilsService.showErrorNotification(this.translate.instant("MISSING_TYPE_checklist_RECORD"), MessageType.Create);
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

	save() {

		if (!this.validate()) {
			return;
		}

		if (this.checklist.checklistId <= 0) {
			this.layoutUtilsService.showErrorNotification(this.translate.instant("MISSING_TYPE_checklist_RECORD"), MessageType.Create);
			return;
		}

		this.checklist.checklistId = null;

		// this.app.insertChecklistRecord(this.checklist, this.comments)
		// 	.subscribe(res => {
		// 		if (!res) {
		// 			return;
		// 		}
		// 		this.dialogRef.close();
		// 	}, error => {
		// 		this.layoutUtilsService.showErrorNotification(error, MessageType.Create);
		// 	});
	}

	closeModal() {
		this.dialog.closeAll();
	}

	
}

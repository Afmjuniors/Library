import { State } from '../../../core/auth/_models/state.model';
import { TypeSeverity } from '../../../core/auth/_models/typeSeverity.model';
import { SystemNode } from '../../../core/auth/_models/systemNode.model';
import { ChangeDetectorRef, Component, Inject, Injector, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA, ProgressSpinnerMode, ThemePalette } from '@angular/material';
import { AppService } from '../../../core/auth/_services';
import { LayoutUtilsService, MessageType } from '../../../core/_base/crud';
import { TypeEventCategory } from '../../../core/auth/_models/typeEventCategory.model';
import { TranslateService } from '@ngx-translate/core';
import { NGX_MAT_DATE_FORMATS } from '@angular-material-components/datetime-picker';
import { SignatureComponent } from '../signature/signature.component';
import { VersionChecklistTemplate } from '../../../core/auth/_models/versionChecklistTemplate.model';
import { TypeComponent } from '../../../core/auth/_models/typeComponent.model';
import { ChecklistFilter } from '../../../core/auth/_models/checklistFilter.model';
import { ComboboxInputComponent } from '../input-components/combobox/combobox-input.component';
import { DatePickerInputComponent } from '../input-components/date/datepicker-input.component';
import { NumberInputComponent } from '../input-components/number/number-input.component';
import { TextInputComponent } from '../input-components/text/text-input.component';
import { ChecklistTemplate } from '../../../core/auth/_models/checklistTemplate.model';
import { ChecklistModel } from '../../../core/auth/_models/checklist.model';
import { FieldChecklist } from '../../../core/auth/_models/fieldChecklist.model';

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
	typeComponent: TypeComponent[] = [];

	typeId: number = 0;

	checklist : ChecklistModel = new ChecklistModel();

	checklistVersion: VersionChecklistTemplate = new VersionChecklistTemplate() ;
	checklists: ChecklistTemplate[] = [];
	filter: ChecklistFilter = new ChecklistFilter();
	loadingTags: boolean = false;

	comments: string = "";
	remainingText: number = 8000;

		displayedColumns = ['title', 'signature'];
		displayedColumnsCell = ['all'];
    checklistId: string | null = null;


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

	public title: string;
	public checklistDropDown: string;
	public versions: string;
	myInjector: Injector;

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
		this.title = this.translate.instant("MENU.CHECKLIST");
		this.checklistDropDown = this.translate.instant("FILTERS.CHECKLIST");
		this.versions = this.translate.instant("FILTERS.VERSIONS");
		this.loadListChecklist();

	}




	public checkDisable(block: any, item: any): boolean {

		
		return false;
	}

	saveInformation(){
		console.log(this.checklistVersion);

	}


	onChecklistChange(event: any): void {
			const selectedId = event.value;
			this.app.getChecklistVersions(selectedId).subscribe(x => {
				this.checklistVersion = x.result;
			},
				error => {
					this.layoutUtilsService.showErrorNotification(error, MessageType.Create);
				});
	}

	
	loadListChecklist() {
		this.app.listChecklist().subscribe(x => {
			this.checklists = x.result;
		},
			error => {
				this.layoutUtilsService.showErrorNotification(error, MessageType.Create);
			});
	}


	loadGetChecklistVersionTemplate() {
		
	}

	validate(): boolean {

		//TODO
		return true;
	}

	valueChange(value) {
		this.remainingText = 8000 - value;
	}

	sign() {
		if (!this.validate()) {
			return;
		}

		if (this.checklistVersion.checklistTemplateId <= 0) {
			this.layoutUtilsService.showErrorNotification(this.translate.instant("MISSING_TYPE_checklist_RECORD"), MessageType.Create);
			return;
		}

		this.dialog.open(SignatureComponent, {
			minHeight: '300px',
			width: '400px',
			data: true
		}).afterClosed()
			.subscribe(x => {
				if (x != '' && x != undefined) {
					this.save();
				}
			});
	}
	signHeader() {
		if (!this.validate()) {
			return;
		}

		if (this.checklistVersion.checklistTemplateId <= 0) {
			this.layoutUtilsService.showErrorNotification(this.translate.instant("MISSING_TYPE_checklist_RECORD"), MessageType.Create);
			return;
		}

		this.dialog.open(SignatureComponent, {
			minHeight: '300px',
			width: '400px',
			data: true
		}).afterClosed()
			.subscribe(x => {
				if (x != '' && x != undefined) {
					this.save();
				}
			});
	}


	save() {

		if (!this.validate()) {
			return;
		}

		if (this.checklistVersion.checklistTemplateId <= 0) {
			this.layoutUtilsService.showErrorNotification(this.translate.instant("MISSING_TYPE_checklist_RECORD"), MessageType.Create);
			return;
		}


	this.checklist.versionChecklistTemplateId = this.checklistVersion.versionChecklistTemplateId ;


	this.saveFieldChecklist()


	this.app.insertUpdateChecklist(this.checklist)
		 .subscribe(res => {
			 if (!res) {
				 return;
			 }
			 this.checklist = res;
		 }, error => {
			 this.layoutUtilsService.showErrorNotification(error, MessageType.Create);
		 });

	
		
	}

	saveFieldChecklist(){
		for (let index = 0; index < this.checklistVersion.fieldsVersionChecklistsTemplate.length; index++) {
			const element = this.checklistVersion.fieldsVersionChecklistsTemplate[index];
			this.checklist.fields = this.checklist.fields || [];
			if(element.value!=null){
				let field = new FieldChecklist();
				var fieldFoundIndex = this.checklist.fields.findIndex(x=>x.fieldVersionChecklistTemplateId ==  element.fieldVersionChecklistTemplateId)
				
				if (fieldFoundIndex >= 0) {
					if(this.checklist.fields[fieldFoundIndex].value == element.value){
						continue;
					}
					this.checklist.fields.splice(fieldFoundIndex, 1); // Remove o item do array
				}
				field.checklistId = this.checklist.checklistId;
				field.versionChecklistTemplateId =this.checklistVersion.versionChecklistTemplateId;
				field.fieldChecklistId = null ; //TODO
				field.fieldVersionChecklistTemplateId = element.fieldVersionChecklistTemplateId;
				field.optionFieldVersionChecklistTemplateId = element.optionFieldVersionChecklistTemplate!=null ?element.optionFieldVersionChecklistTemplate.find(x=>x.checked).optionFieldVersionChecklistTemplateId:null;
				field.value = element.value;
				

				this.checklist.fields.push(field)
			}
		}
	}


	closeModal() {
		this.dialog.closeAll();
	}


}

import { State } from './../../../core/auth/_models/state.model';
import { TypeSeverity } from './../../../core/auth/_models/typeSeverity.model';
import { SystemNode } from './../../../core/auth/_models/systemNode.model';
import { OccurrenceRecord } from './../../../core/auth/_models/occurenceRecord.model';
import { ChangeDetectorRef, Component, Inject, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA, ProgressSpinnerMode, ThemePalette } from '@angular/material';
import { AppService } from './../../../core/auth/_services';
import { LayoutUtilsService, MessageType } from './../../../core/_base/crud';
import { TypeOccurenceRecord } from './../../../core/auth/_models/typeOccurrenceRecord.model';
import { Process } from './../../../core/auth/_models/process.model';
import { Area } from './../../../core/auth/_models/area.model';
import { OccurrenceRecordFilter } from './../../../core/auth/_models/occurrenceRecordFilter.model';
import { TypeEventCategory } from './../../../core/auth/_models/typeEventCategory.model';
import { TranslateService } from '@ngx-translate/core';
import { NGX_MAT_DATE_FORMATS } from '@angular-material-components/datetime-picker';
import { SignatureComponent } from '../signature/signature.component';

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
	selector: 'kt-new-occurrence-record',
	templateUrl: './new-occurrence-record.component.html',
	styleUrls: ['./new-occurrence-record.component.scss'],
	providers: [
		{ provide: NGX_MAT_DATE_FORMATS, useValue: DATE_TIME_FORMAT },
	],
})
export class NewOccurrenceRecordComponent implements OnInit {
	occurrence: OccurrenceRecord = new OccurrenceRecord();
	typesOccurrences: TypeOccurenceRecord[] = [];

	typeId: number = 0;

	processes: Process[] = [];
	systemNodes: SystemNode[] = [];
	systemStates: State[] = [];
	typeEventCategory: TypeEventCategory[] = [];
	typesSeverities: TypeSeverity[] = [];
	areas: Area[] = [];
	filter: OccurrenceRecordFilter = new OccurrenceRecordFilter();
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

	constructor(
		public dialogRef: MatDialogRef<NewOccurrenceRecordComponent>,
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
		this.loadListProcess();
		this.loadListSystemNodes();
		this.loadListTypesSeverities();
		this.loadListStates();
		this.loadListEvent();
	}

	loadListStates() {
		this.app.listStates().subscribe(x => {
			this.systemStates = x;
		},
			error => {
				this.layoutUtilsService.showErrorNotification(error, MessageType.Create);
			});
	}

	loadListEvent() {
		this.app.listEventsCategories().subscribe(x => {
			this.typeEventCategory = x;
		},
			error => {
				this.layoutUtilsService.showErrorNotification(error, MessageType.Create);
			});
	}

	loadListTypes() {
		this.loadingTags = true;
		this.app.listTypesOccurrencesRecordsByTypeArea(this.typeId, this.filter.area).subscribe(x => {
			this.typesOccurrences = x;
			this.loadingTags = false;
			this.ref.detectChanges();
		},
		error => {
			this.layoutUtilsService.showErrorNotification(error, MessageType.Create);
			this.loadingTags = false;
		});
	}

	loadListProcess() {
		this.app.listProcesses().subscribe(x => {
			this.processes = x;
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

	loadListTypesSeverities() {
		this.app.listTypesSeverities().subscribe(res => {
			this.typesSeverities = res;
		},
			error => {
				this.layoutUtilsService.showErrorNotification(error, MessageType.Create);
			});
	}

	loadListAreas(processId: number) {
		this.app.listAreasByProcess(processId).subscribe(x => {
			this.areas = x;
			this.ref.detectChanges();
		},
			error => {
				this.layoutUtilsService.showErrorNotification(error, MessageType.Create);
			});
	}

	validate(): boolean {

		if (this.filter.process == null || this.filter.process == 0) {
			this.layoutUtilsService.showActionNotification(this.translate.instant("PROCESS_NOT_PROVIDED"));
			return false;
		}

		if (this.filter.area == null || this.filter.area == 0) {
			this.layoutUtilsService.showActionNotification(this.translate.instant("AREA_NOT_PROVIDED"));
			return false;
		}

		if (this.occurrence.typeOccurrenceRecordId == null || this.occurrence.typeOccurrenceRecordId == 0) {
			this.layoutUtilsService.showActionNotification(this.translate.instant("TAG_NOT_PROVIDED"));
			return false;
		}

		if (this.typeId == 1) {
			if (this.occurrence.message == null || this.occurrence.message.trim().length == 0) {
				this.layoutUtilsService.showActionNotification(this.translate.instant("DESCRIPTION_NOT_PROVIDED"));
				return false;
			}

			if (!this.occurrence.dateTimeEnd) {
				this.layoutUtilsService.showActionNotification(this.translate.instant("DATETIMEEND_NOT_PROVIDED"));
				return false;
			}
		}
		if (this.typeId == 2) {
			if (this.occurrence.responsibleEvent == null || this.occurrence.responsibleEvent.trim().length == 0) {
				this.layoutUtilsService.showActionNotification(this.translate.instant("RESPONSIBLE_NOT_PROVIDED"));
				return false;
			}
			if (this.occurrence.beforeValue == null || this.occurrence.beforeValue.trim().length == 0) {
				this.layoutUtilsService.showActionNotification(this.translate.instant("BEFORE_VALUE_NOT_PROVIDED"));
				return false;
			}
			if (this.occurrence.newValue == null || this.occurrence.newValue.trim().length == 0) {
				this.layoutUtilsService.showActionNotification(this.translate.instant("NEW_VALUE_NOT_PROVIDED"));
				return false;
			}
			if (this.occurrence.comment == null || this.occurrence.comment.trim().length == 0) {
				this.layoutUtilsService.showActionNotification(this.translate.instant("COMMENT_NOT_PROVIDED"));
				return false;
			}
		}

		if (this.comments == null || this.comments.trim().length == 0) {
			this.layoutUtilsService.showActionNotification(this.translate.instant("COMMENTS_NOT_PROVIDED"));
			return false;
		}

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

		if (this.occurrence.typeOccurrenceRecordId <= 0) {
			this.layoutUtilsService.showErrorNotification(this.translate.instant("MISSING_TYPE_OCCURRENCE_RECORD"), MessageType.Create);
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

		if (this.occurrence.typeOccurrenceRecordId <= 0) {
			this.layoutUtilsService.showErrorNotification(this.translate.instant("MISSING_TYPE_OCCURRENCE_RECORD"), MessageType.Create);
			return;
		}

		this.occurrence.typeOccurrenceRecord = null;

		this.app.insertOccurrenceRecord(this.occurrence, this.comments)
			.subscribe(res => {
				if (!res) {
					return;
				}
				this.dialogRef.close();
			}, error => {
				this.layoutUtilsService.showErrorNotification(error, MessageType.Create);
			});
	}

	closeModal() {
		this.dialog.closeAll();
	}

	setMessage(message){
		if(this.typeId == 1){
			let type = this.typesOccurrences.find(x => x.typeOccurrenceRecordId == message.value);
			this.occurrence.message = type.description;
		}
	}
}

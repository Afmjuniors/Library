import { ChangeDetectorRef, Component, Inject, OnInit } from '@angular/core';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { Store } from '@ngrx/store';
import { TranslateService } from '@ngx-translate/core';
import { AppService } from '../../../../core/auth/_services';
import { ConfigService } from '../../../../core/auth/_services/config.service';
import { AppState } from '../../../../core/reducers';
import { LayoutUtilsService, MessageType } from '../../../../core/_base/crud';
import { Area } from '../../../../core/auth/_models/area.model';
import { Process } from '../../../../core/auth/_models/process.model';
import { TypeOccurenceRecord } from '../../../../core/auth/_models/typeOccurrenceRecord.model';
import { ImpactedArea } from '../../../../core/auth/_models/impactedArea.model';
import { Unit } from '../../../../core/auth/_models/unit.model';
import { SignatureComponent } from '../../../../../app/views/components/signature/signature.component';

@Component({
	selector: 'kt-new-type-occurrence-record',
	templateUrl: './new-type-occurrence-record.component.html',
	styleUrls: ['./new-type-occurrence-record.component.scss']
})
export class NewTypeOccurrenceRecordComponent implements OnInit {
	loading: boolean = false;
	errors: string[] = [];

	typeOccurrenceRecord: TypeOccurenceRecord = new TypeOccurenceRecord();

	selectedVoice: boolean = false;
	selectedSms: boolean = false;
	selectedWhatsapp: boolean = false;
	selectedEmail: boolean = true;
	columnRegisters = ['remove', 'tag', 'process', 'area', 'type'];

	comments: string = "";
	remainingText: number = 8000;

	processes: Process[] = [];
	areas: Area[] = [];
	impactedAreas: Area[] = [];
	units: Unit[] = [];

	constructor(
		public store: Store<AppState>,
		public dialog: MatDialog,
		public translateService: TranslateService,
		public dialogRef: MatDialogRef<string>,
		public configService: ConfigService,
		public app: AppService,
		private layoutUtilsService: LayoutUtilsService,
		public translate: TranslateService,
		public detector: ChangeDetectorRef,
		@Inject(MAT_DIALOG_DATA)
		public data: any,
	) { }

	ngOnInit() {
		this.loadListProcess();
		this.loadListUnits();
	}

	loadListProcess() {
		this.app.listProcesses().subscribe(x => {
			this.processes = x;
		},
			error => {
				this.layoutUtilsService.showErrorNotification(error);
			});
	}

	loadListAreas(processId: number) {
		this.app.listAreasByProcess(processId).subscribe(x => {
			this.typeOccurrenceRecord.areas = x;
			this.impactedAreas = JSON.parse(JSON.stringify(x));
		},
			error => {
				this.layoutUtilsService.showErrorNotification(error);
			});
	}

	loadListUnits() {
		this.app.listUnits().subscribe(x => {
			this.units = x;
		},
			error => {
				this.layoutUtilsService.showErrorNotification(error);
			});
	}

	loadImpactedAreas() {
		this.app.ListImpactedAreasByTypeOccurrenceRecord(this.typeOccurrenceRecord.typeOccurrenceRecordId).subscribe(x => {
			this.typeOccurrenceRecord.impactedAreas = x;
		},
			error => {
				this.layoutUtilsService.showErrorNotification(error);
			});
	}

	addImpactedArea() {
		for (let i = 0; i < this.impactedAreas.length; i++) {
			if (this.impactedAreas[i].selected == true) {
				this.impactedAreas[i].selected = false;
				if (!this.typeOccurrenceRecord.impactedAreas) {
					this.typeOccurrenceRecord.impactedAreas = [];
				}

				let impacted: ImpactedArea = new ImpactedArea();
				impacted.areaId = this.impactedAreas[i].areaId;
				impacted.area = this.impactedAreas[i];

				this.typeOccurrenceRecord.impactedAreas.push(impacted);
				this.impactedAreas.splice(i, 1);
			}
		}
	}

	removeImpactedArea() {
		for (let i = 0; i < this.typeOccurrenceRecord.impactedAreas.length; i++) {
			if (this.typeOccurrenceRecord.impactedAreas[i].area.selected == true) {
				this.typeOccurrenceRecord.impactedAreas[i].area.selected = false;
				this.impactedAreas.push(this.typeOccurrenceRecord.impactedAreas[i].area);
				this.typeOccurrenceRecord.impactedAreas.splice(i, 1);
			}
		}
	}

	verifyNotificationChannels() {
		var selecteds: number[] = [];

		if (this.selectedVoice) {
			selecteds.push(1);
		}

		if (this.selectedSms) {
			selecteds.push(2);
		}

		if (this.selectedWhatsapp) {
			selecteds.push(3);
		}

		if (this.selectedEmail) {
			selecteds.push(4);
		}

		return selecteds;
	}

	validate(): boolean {

		if (this.typeOccurrenceRecord.tag == null || this.typeOccurrenceRecord.tag.trim().length == 0) {
			this.layoutUtilsService.showActionNotification(this.translate.instant("TAG_NOT_PROVIDED"));
			return false;
		}

		if (this.typeOccurrenceRecord.areaId == null || this.typeOccurrenceRecord.areaId == 0) {
			this.layoutUtilsService.showActionNotification(this.translate.instant("AREA_NOT_PROVIDED"));
			return false;
		}

		if (this.typeOccurrenceRecord.description == null || this.typeOccurrenceRecord.description.trim().length == 0) {
			this.layoutUtilsService.showActionNotification(this.translate.instant("DESCRIPTION_NOT_PROVIDED"));
			return false;
		}

		if (this.typeOccurrenceRecord.typeOccurrenceId == null || this.typeOccurrenceRecord.typeOccurrenceId == 0) {
			this.layoutUtilsService.showActionNotification(this.translate.instant("TYPE_OCCURRENCE_NOT_PROVIDED"));
			return false;
		}

		if (this.typeOccurrenceRecord.getExtremeValue == true) {
			if (this.typeOccurrenceRecord.tagDevice == null || this.typeOccurrenceRecord.tagDevice.trim().length == 0) {
				this.layoutUtilsService.showActionNotification(this.translate.instant("TAG_DEVICE_NOT_PROVIDED"));
				return false;
			}

			if (this.typeOccurrenceRecord.compensationValue == null) {
				if (this.typeOccurrenceRecord.tagDevice == null || this.typeOccurrenceRecord.tagDevice.trim().length == 0) {
					this.layoutUtilsService.showActionNotification(this.translate.instant("COMPENSATION_VALUE_NOT_PROVIDED"));
					return false;
				}
			}
		}

		if (this.comments == null || this.comments.trim().length == 0) {
			this.layoutUtilsService.showActionNotification(this.translate.instant("COMMENTS_NOT_PROVIDED"));
			return false;
		}

		return true;
	}

	save() {
		this.loading = true;
		if (!this.validate()) {
			this.loading = false;
			return;
		}

		this.typeOccurrenceRecord.listTypeNotificationChannels = this.verifyNotificationChannels();
		this.typeOccurrenceRecord.typeOccurrenceId = Number(this.typeOccurrenceRecord.typeOccurrenceId);

		this.app.createRegister(this.typeOccurrenceRecord).subscribe(x => {
			this.loading = false;
			this.layoutUtilsService.showActionNotification(this.translate.instant("DATA_SAVED"), MessageType.Create);
			this.dialogRef.close("success");
		},
		error => {
			this.loading = false;
			this.layoutUtilsService.showErrorNotification(error);
		});
	}

	changeValue() {
		if (this.typeOccurrenceRecord.assessmentNeeded == true) {
			this.typeOccurrenceRecord.notify = true;
		}
	}

	valueChange(value) {
		this.remainingText = 8000 - value;
	}

	sign()
	{
		if (!this.validate()) {
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
}

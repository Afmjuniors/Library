import { AppService } from './../../../../core/auth/_services/app.service';
import { ChangeDetectorRef, Component, Inject, OnInit } from '@angular/core';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { Store } from '@ngrx/store';
import { TranslateService } from '@ngx-translate/core';
import { TypeOccurenceRecord } from '../../../../core/auth/_models/typeOccurrenceRecord.model';
import { EditRegisterGroupFilter } from '../../../../core/auth/_models/_config/edit-register-group-filter.model';
import { ConfigService } from '../../../../core/auth/_services/config.service';
import { AppState } from '../../../../core/reducers';
import { LayoutUtilsService, MessageType } from '../../../../core/_base/crud';
import { AdGroup } from '../../../../core/auth/_models/adGroup.model';
import { Process } from '../../../../core/auth/_models/process.model';
import { Area } from '../../../../core/auth/_models/area.model';
import { ImpactedArea } from '../../../../core/auth/_models/impactedArea.model';
import { TypeOccurrenceRecordNotificationChannel } from '../../../../core/auth/_models/typeOccurrenceRecordNotificationChannel.model';
import { Unit } from '../../../../core/auth/_models/unit.model';
import { SignatureComponent } from '../../../../views/components/signature/signature.component';
import { NotifyConfigurationComponent } from './../notify-configuration/notify-configuration.component';
import { ReleaseArea } from '../../../../core/auth/_models/releaseArea.model';

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
	selector: 'kt-type-occurrence-record-configuration',
	templateUrl: './type-occurrence-record-configuration.component.html',
	styleUrls: ['./type-occurrence-record-configuration.component.scss']
})
export class TypeOccurrenceRecordConfigurationComponent implements OnInit {
	loading: boolean = false;
	errors: string[] = [];
	adGroups: AdGroup[] = [];
	selectedIds: TypeOccurenceRecord[] = [];
	filter: EditRegisterGroupFilter = new EditRegisterGroupFilter();
	typeOccurrenceRecord: TypeOccurenceRecord = new TypeOccurenceRecord();
	adGroupSelected: AdGroup = new AdGroup();
	selectedVoice: boolean = false;
	selectedSms: boolean = false;
	selectedWhatsapp: boolean = false;
	selectedEmail: boolean = true;
	columnRegisters = ['remove', 'tag', 'process', 'area', 'type'];
	description: string;
	id: number;
	comments: string = "";
	remainingText: number = 8000;

	showImpactedAreas: boolean;
	removeAllImpactedAreas: boolean = false;
	removeAllReleaseAreas: boolean = false;

	processes: Process[] = [];
	areas: Area[] = [];
	listAreasRelease: Area[] = [];
	units: Unit[] = [];
	impactedAreasGeneral: Area[] = [];
	releaseAreasGeneral: Area[] = [];
	releaseProcessesGeneral;

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
		if (this.data.selectedItems) {
			this.getList();
		}
	}

	verifyProcesses() {
		if (this.selectedIds.length > 1) {
			var idProcess;
			idProcess = this.selectedIds[0].area.processId
			for (let index = 1; index < this.selectedIds.length; index++) {
				if (this.selectedIds[index].area.processId != idProcess) {
					return false;
				}
			}
			return true;
		} else {
			return false;
		}
	}

	addImpactedArea(indexType) {
		if (indexType != null) {
			for (let i = 0; i < this.selectedIds[indexType].areas2.length; i++) {
				if (this.selectedIds[indexType].areas2[i].selected == true) {
					this.selectedIds[indexType].areas2[i].selected = false;
					if (!this.selectedIds[indexType].impactedAreas) {
						this.selectedIds[indexType].impactedAreas = [];
					}

					let impacted: ImpactedArea = new ImpactedArea();
					impacted.areaId = this.selectedIds[indexType].areas2[i].areaId;
					impacted.area = this.selectedIds[indexType].areas2[i];

					this.selectedIds[indexType].impactedAreas.push(impacted);
					this.selectedIds[indexType].areas2.splice(i, 1);
					this.detector.detectChanges();
				}
			}
		} else {
			for (let i = 0; i < this.impactedAreasGeneral.length; i++) {
				if (this.impactedAreasGeneral[i].selected == true) {
					this.impactedAreasGeneral[i].selected = false;
					if (!this.typeOccurrenceRecord.impactedAreas) {
						this.typeOccurrenceRecord.impactedAreas = [];
					}

					let impacted: ImpactedArea = new ImpactedArea();
					impacted.areaId = this.impactedAreasGeneral[i].areaId;
					impacted.area = this.impactedAreasGeneral[i];

					this.typeOccurrenceRecord.impactedAreas.push(impacted);
					this.typeOccurrenceRecord.areas.splice(i, 1);
				}
			}
		}
	}

	addReleaseArea(indexType) {
		let removedAreas = []
		if (indexType != null) {
			for (let i = 0; i < this.selectedIds[indexType].releaseAreas2.length; i++) {
				if (this.selectedIds[indexType].releaseAreas2[i].selected == true) {
					this.selectedIds[indexType].releaseAreas2[i].selected = false;
					if (!this.selectedIds[indexType].releaseAreas) {
						this.selectedIds[indexType].releaseAreas = [];
					}

					let release: ReleaseArea = new ReleaseArea();
					release.areaId = this.selectedIds[indexType].releaseAreas2[i].areaId;
					release.area = this.selectedIds[indexType].releaseAreas2[i];

					this.selectedIds[indexType].releaseAreas.push(release);
					removedAreas.push(release.areaId);
					this.detector.detectChanges();
				}
			}
			for (let i = 0; i < removedAreas.length; i++) {
				let index = this.selectedIds[indexType].releaseAreas2.findIndex(x => x.areaId = removedAreas[i])
				this.selectedIds[indexType].releaseAreas2.splice(index, 1);		
				this.detector.detectChanges();
			}
		} else {
			for (let i = 0; i < this.releaseAreasGeneral.length; i++) {
				if (this.releaseAreasGeneral[i].selected == true) {
					this.releaseAreasGeneral[i].selected = false;
					if (!this.typeOccurrenceRecord.releaseAreas) {
						this.typeOccurrenceRecord.releaseAreas = [];
					}

					let release: ReleaseArea = new ReleaseArea();
					release.areaId = this.releaseAreasGeneral[i].areaId;
					release.area = this.releaseAreasGeneral[i];

					this.typeOccurrenceRecord.releaseAreas.push(release);
					removedAreas.push(release.areaId);
					//this.releaseAreasGeneral.splice(i, 1);
				}
			}

			for (let i = 0; i < removedAreas.length; i++) {
				let index = this.releaseAreasGeneral.findIndex(x => x.areaId = removedAreas[i])
				this.releaseAreasGeneral.splice(index, 1);		
			}
		}
	}

	removeReleaseArea(indexType) {
		console.log("indexType", indexType)
		if (indexType != null) {
			for (let i = 0; i < this.selectedIds[indexType].releaseAreas.length; i++) {
				console.log(this.selectedIds[indexType]);
				if (this.selectedIds[indexType].releaseAreas[i].area.selected == true) {
					this.selectedIds[indexType].releaseAreas[i].area.selected = false;
					//this.selectedIds[indexType].listAreasRelease.push(this.selectedIds[indexType].releaseAreas[i].area);
					if (this.selectedIds[indexType].releaseAreas[i].area.processId == this.selectedIds[indexType].processRelease) {
						this.selectedIds[indexType].releaseAreas2.push(this.selectedIds[indexType].releaseAreas[i].area);
					}
					console.log("areas2", this.selectedIds[indexType].releaseAreas2);
					this.selectedIds[indexType].releaseAreas.splice(i, 1);
					this.detector.detectChanges();
				}
			}
		} else {
			for (let i = 0; i < this.typeOccurrenceRecord.releaseAreas.length; i++) {
				if (this.typeOccurrenceRecord.releaseAreas[i].area.selected == true) {
					this.typeOccurrenceRecord.releaseAreas[i].area.selected = false;
					if (this.releaseProcessesGeneral == this.typeOccurrenceRecord.releaseAreas[i].area.processId) {
						let release: Area = new Area();
						release.areaId = this.typeOccurrenceRecord.releaseAreas[i].areaId;
						release = this.typeOccurrenceRecord.releaseAreas[i].area;
						this.releaseAreasGeneral.push(release);
					}
					this.typeOccurrenceRecord.releaseAreas.splice(i, 1);
					this.detector.detectChanges();
				}
			}
		}
	}

	removeImpactedArea(indexType) {

		if (indexType != null) {
			for (let i = 0; i < this.selectedIds[indexType].impactedAreas.length; i++) {
				if (this.selectedIds[indexType].impactedAreas[i].area.selected == true) {
					this.selectedIds[indexType].impactedAreas[i].area.selected = false;
					this.selectedIds[indexType].areas.push(this.selectedIds[indexType].impactedAreas[i].area);
					this.selectedIds[indexType].areas2.push(this.selectedIds[indexType].impactedAreas[i].area);
					this.selectedIds[indexType].impactedAreas.splice(i, 1);
					this.detector.detectChanges();
				}
			}
		} else {
			for (let i = 0; i < this.typeOccurrenceRecord.impactedAreas.length; i++) {
				if (this.typeOccurrenceRecord.impactedAreas[i].area.selected == true) {
					this.typeOccurrenceRecord.impactedAreas[i].area.selected = false;
					this.impactedAreasGeneral.push(this.typeOccurrenceRecord.impactedAreas[i].area);
					this.typeOccurrenceRecord.impactedAreas.splice(i, 1);
					this.detector.detectChanges();
				}
			}
		}
	}

	removeTypeOccurrence(index) {
		this.selectedIds.splice(index, 1);
	}

	getList() {
		this.loadListUnits();
		this.app.getListTypeOccurrenceRecord(this.data.selectedItems).subscribe(res => {
			this.selectedIds = res;

			if (this.selectedIds.length == 1) {
				this.loadData();
			}

			this.showImpactedAreas = this.verifyProcesses();

			this.getAreasList();

		},
			error => {
				this.layoutUtilsService.showErrorNotification(error);
			})
	}

	loadData() {
		this.typeOccurrenceRecord.notify = this.selectedIds[0].notify;
		this.typeOccurrenceRecord.notificationFrequency = this.selectedIds[0].notificationFrequency;
		this.typeOccurrenceRecord.assessmentNeeded = this.selectedIds[0].assessmentNeeded;
		this.typeOccurrenceRecord.deactivated = this.selectedIds[0].deactivated;
		this.loadNotificationChannels();
	}

	getAreasList() {
		for (let i = 0; i < this.selectedIds.length; i++) {
			this.loadListAreas(this.selectedIds[i].area.processId, i);
			this.selectedIds[i].processRelease = this.selectedIds[i].area.processId;
		}
	}
	
	loadReleaseAreas() {
		for (let i = 0; i < this.selectedIds.length; i++) {
			this.loadReleaseAreasTypeOccurrence(i)
		}
	}

	loadReleaseAreasTypeOccurrence(index: number){
		for (let k = 0; k < this.selectedIds[index].releaseAreas.length; k++) {
			if (this.selectedIds[index].releaseAreas2) {
				let i = this.selectedIds[index].releaseAreas2.findIndex(x => x.areaId == this.selectedIds[index].releaseAreas[k].areaId);
		
				if (i != -1) {
					this.selectedIds[index].releaseAreas2.splice(i, 1);
				}
			}
		}
		this.detector.detectChanges();
	}

	loadImpactedAreasTypeOccurrence() {
		for (let i = 0; i < this.selectedIds.length; i++) {
			this.loadImpactedAreas(this.selectedIds[i].typeOccurrenceRecordId, i)
		}
	}

	loadImpactedAreas(typeOccurrenceRecordId: number, index: number) {
		this.app.ListImpactedAreasByTypeOccurrenceRecord(typeOccurrenceRecordId).subscribe(x => {
			this.selectedIds[index].impactedAreas = x;

			if (x) {
				for (let k = 0; k < this.selectedIds[index].impactedAreas.length; k++) {
					if (this.selectedIds[index].areas2) {
						let i = this.selectedIds[index].areas2.findIndex(x => x.areaId == this.selectedIds[index].impactedAreas[k].areaId);

						if (i != -1) {
							this.selectedIds[index].areas2.splice(i, 1);
						}
					}
				}
			}
		},
			error => {
				this.layoutUtilsService.showErrorNotification(error);
			});
	}

	loadListProcess() {
		this.app.listProcesses().subscribe(x => {
			this.processes = x;
		},
			error => {
				this.layoutUtilsService.showActionNotification(error.error);
			});
	}

	loadListAreas(processId: number, index) {
		var self = this;
		this.app.listAreasByProcess(processId).subscribe(x => {
			if (index == 0) {
				self.typeOccurrenceRecord.areas = x;
			}
			self.areas = x;
			self.listAreasRelease = x;
			self.selectedIds[index].areas = JSON.parse(JSON.stringify(x));
			self.selectedIds[index].areas2 = JSON.parse(JSON.stringify(x));
			self.selectedIds[index].releaseAreas2 = JSON.parse(JSON.stringify(x));
			self.impactedAreasGeneral = JSON.parse(JSON.stringify(x));
			self.releaseAreasGeneral = JSON.parse(JSON.stringify(x));
			self.releaseProcessesGeneral = processId;
			self.detector.detectChanges();
			self.loadImpactedAreasTypeOccurrence();
			this.loadReleaseAreas();
		},
			error => {
				this.layoutUtilsService.showErrorNotification(error);
			});
	}

	loadListAreasRelease(processId: number, index) {
		var self = this;
		this.app.listAreasByProcess(processId).subscribe(x => {
			if(index == null){
				self.releaseAreasGeneral = x;
			} else{
				if (index == 0) {
					self.typeOccurrenceRecord.areas = x;
				}
				self.listAreasRelease = x;
				self.selectedIds[index].releaseAreas2 = x;
				self.selectedIds[index].processRelease = processId;
			}
			self.detector.detectChanges();
			this.loadReleaseAreas();
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

	loadNotificationChannels() {
		this.app.listTypeNotificationByTypeOccurrenceRecord(this.selectedIds[0].typeOccurrenceRecordId).subscribe(x => {
			var channels: TypeOccurrenceRecordNotificationChannel[] = x;

			for (let index = 0; index < channels.length; index++) {
				if (channels[index].typeNotificationChannel == 1) {
					this.selectedVoice = true;
				}
				else if (channels[index].typeNotificationChannel == 2) {
					this.selectedSms = true;
				}
				else if (channels[index].typeNotificationChannel == 3) {
					this.selectedWhatsapp = true;
				}
				else if (channels[index].typeNotificationChannel == 4) {
					this.selectedEmail = true;
				}
			}
		},
			error => {
				this.layoutUtilsService.showErrorNotification(error);
			});
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

	prepareTypesOccurrenceRecords() {
		var typesOccurrences: TypeOccurenceRecord[] = [];

		for (let i = 0; i < this.selectedIds.length; i++) {
			let type: TypeOccurenceRecord = new TypeOccurenceRecord();

			type.typeOccurrenceRecordId = this.selectedIds[i].typeOccurrenceRecordId;
			type.tag = this.selectedIds[i].tag;
			type.description = this.selectedIds[i].description;
			type.getExtremeValue = this.selectedIds[i].getExtremeValue;
			type.compensationValue = this.selectedIds[i].compensationValue;
			type.tagDevice = this.selectedIds[i].tagDevice;
			type.areaId = this.selectedIds[i].areaId;
			type.typeOccurrenceId = this.selectedIds[i].typeOccurrenceId;
			if (this.selectedIds[i].unitId == 0) {
				type.unitId = null;
			}
			else {
				type.unitId = this.selectedIds[i].unitId;
			}

			if (this.selectedIds[i].datatypeId == 0) {
				type.datatypeId = null;
			}
			else {
				type.datatypeId = this.selectedIds[i].datatypeId;
			}

			type.notify = this.typeOccurrenceRecord.notify;
			type.notificationFrequency = this.typeOccurrenceRecord.notificationFrequency;
			type.assessmentNeeded = this.typeOccurrenceRecord.assessmentNeeded;

			if (type.deactivated != null) {
				type.deactivated = this.typeOccurrenceRecord.deactivated;
				type.deactivateDate = this.typeOccurrenceRecord.deactivateDate;
			}
			else {
				type.deactivated = false;
			}

			type.listTypeNotificationChannels = this.verifyNotificationChannels();

			type.impactedAreas = this.selectedIds[i].impactedAreas;
			type.releaseAreas = this.selectedIds[i].releaseAreas;

			if (this.selectedIds && this.selectedIds.length > 0) {
				if (this.removeAllImpactedAreas) {
					type.impactedAreas = this.typeOccurrenceRecord.impactedAreas;
				} else {
					if (this.typeOccurrenceRecord.impactedAreas && this.typeOccurrenceRecord.impactedAreas.length > 0) {
						if (type.impactedAreas != null && type.impactedAreas.length > 0) {
							for (let k = 0; k < this.typeOccurrenceRecord.impactedAreas.length; k++) {
								let index = this.selectedIds[i].impactedAreas.findIndex(x => x.areaId == this.typeOccurrenceRecord.impactedAreas[k].areaId);

								if (index < 0) {
									let area: ImpactedArea = this.typeOccurrenceRecord.impactedAreas[k];
									type.impactedAreas.push(area);
								}
							}
						} else {
							type.impactedAreas = this.typeOccurrenceRecord.impactedAreas;
						}
					}
				}
				if (this.removeAllReleaseAreas) {
					type.releaseAreas = this.typeOccurrenceRecord.releaseAreas;
				} else {
					if (this.typeOccurrenceRecord.releaseAreas && this.typeOccurrenceRecord.releaseAreas.length > 0) {
						if (type.releaseAreas != null && type.releaseAreas.length > 0) {
							for (let k = 0; k < this.typeOccurrenceRecord.releaseAreas.length; k++) {
								let index = this.selectedIds[i].releaseAreas.findIndex(x => x.areaId == this.typeOccurrenceRecord.releaseAreas[k].areaId);

								if (index < 0) {
									let area: ReleaseArea = this.typeOccurrenceRecord.releaseAreas[k];
									type.releaseAreas.push(area);
								}
							}
						} else {
							type.releaseAreas = this.typeOccurrenceRecord.releaseAreas;
						}
					}
				}

			}

			if (!this.validate(type)) {
				return;
			}

			typesOccurrences.push(type);

		}
		this.save(typesOccurrences);
	}


	validate(typeOccurrenceRecord: TypeOccurenceRecord): boolean {
		if (this.comments == null || this.comments.trim().length == 0) {
			this.layoutUtilsService.showActionNotification(this.translate.instant("COMMENTS_NOT_PROVIDED"));
			return false;
		}

		if (typeOccurrenceRecord.tag == null || typeOccurrenceRecord.tag.trim().length == 0) {
			this.layoutUtilsService.showActionNotification(this.translate.instant("TAG_NOT_PROVIDED"));
			return false;
		}

		if (typeOccurrenceRecord.areaId == null || typeOccurrenceRecord.areaId == 0) {
			this.layoutUtilsService.showActionNotification(this.translate.instant("AREA_NOT_PROVIDED"));
			return false;
		}

		if (typeOccurrenceRecord.description == null || typeOccurrenceRecord.description.trim().length == 0) {
			this.layoutUtilsService.showActionNotification(this.translate.instant("DESCRIPTION_NOT_PROVIDED"));
			return false;
		}

		if (typeOccurrenceRecord.typeOccurrenceRecordId == null || typeOccurrenceRecord.typeOccurrenceRecordId == 0) {
			this.layoutUtilsService.showActionNotification(this.translate.instant("TYPE_OCCURRENCE_NOT_PROVIDED"));
			return false;
		}

		if (typeOccurrenceRecord.getExtremeValue == null) {
			this.layoutUtilsService.showActionNotification(this.translate.instant("GET_EXTREME_VALUE_NOT_PROVIDED"));
			return false;
		}
		else {
			if (typeOccurrenceRecord.getExtremeValue == true) {
				if (typeOccurrenceRecord.tagDevice == null || typeOccurrenceRecord.tagDevice.trim().length == 0) {
					this.layoutUtilsService.showActionNotification(this.translate.instant("TAG_DEVICE_NOT_PROVIDED"));
					return false;
				}

				if (typeOccurrenceRecord.compensationValue == null) {
					if (typeOccurrenceRecord.tagDevice == null || typeOccurrenceRecord.tagDevice.trim().length == 0) {
						this.layoutUtilsService.showActionNotification(this.translate.instant("COMPENSATION_VALUE_NOT_PROVIDED"));
						return false;
					}
				}
			}
		}

		return true;
	}

	valueChange(value) {
		this.remainingText = 8000 - value;
	}

	sign()
	{
		this.dialog.open(SignatureComponent, {
			minHeight: '300px',
			width: '400px',
			data: true
		}).afterClosed()
		.subscribe(x => {
			if(x != '' && x != undefined){
				this.prepareTypesOccurrenceRecords();
			}
		});
	}

	save(typesOccurrences) {
		this.loading = true;
		this.app.configureRegister(typesOccurrences, this.comments).subscribe(x => {
			this.loading = false;
			this.layoutUtilsService.showActionNotification(this.translate.instant("DATA_SAVED"), MessageType.Create);
			if(this.typeOccurrenceRecord.notify || this.typeOccurrenceRecord.assessmentNeeded){
				this.dialog.open(NotifyConfigurationComponent, {minHeight: '30%', width: '60%', data: typesOccurrences})
			}
			this.dialogRef.close("success");
		},
		error => {
			this.loading = false;
			this.layoutUtilsService.showErrorNotification(error);
		})
	}

	changeValue() {
		if (this.typeOccurrenceRecord.assessmentNeeded == true) {
			this.typeOccurrenceRecord.notify = true;
		}
	}


}

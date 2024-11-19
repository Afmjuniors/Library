import { ConfigService } from './../../../../core/auth/_services/config.service';
import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { AppService } from '../../../../core/auth/_services';
import { LayoutUtilsService, MessageType } from '../../../../core/_base/crud';
import { TypeOccurenceRecord } from './../../../../core/auth/_models/typeOccurrenceRecord.model';
import { TranslateService } from '@ngx-translate/core';

@Component({
	selector: 'kt-notify-configuration',
	templateUrl: './notify-configuration.component.html',
	styleUrls: ['./notify-configuration.component.scss']
})
export class NotifyConfigurationComponent implements OnInit {
	loading: boolean = false;
	typesOccurrencesRecords: TypeOccurenceRecord[] = [];

	constructor(
		public dialogRef: MatDialogRef<NotifyConfigurationComponent>,
		public translate: TranslateService,
		@Inject(MAT_DIALOG_DATA)
		public data: any,
		public service: ConfigService,
		private layoutUtilsService: LayoutUtilsService
	) { }

	ngOnInit() {
		this.typesOccurrencesRecords = this.data;
		for (let index = 0; index < this.typesOccurrencesRecords.length; index++) {
			this.typesOccurrencesRecords[index].sended = null;
		}
		console.log("types", this.typesOccurrencesRecords)
	}

	sendMessages(id, index){
		this.typesOccurrencesRecords[index].sending = true;
		this.service.SendMessagesByTypeOccurrence(id).subscribe(x => {
			this.typesOccurrencesRecords[index].sending = false;
			this.typesOccurrencesRecords[index].sended = true;
			this.layoutUtilsService.showActionNotification(this.translate.instant("DATA_SAVED"), MessageType.Create);

			if(this.validateCancel()){
				this.dialogRef.close();
			}
		},
		error => {
			this.typesOccurrencesRecords[index].sending = false;
			this.layoutUtilsService.showErrorNotification(error);
		});
	}

	cancelNotifications(index){
		this.typesOccurrencesRecords[index].sended = false;
		if(this.validateCancel()){
			this.dialogRef.close();
		}
	}

	validateCancel(){
		for (let index = 0; index < this.typesOccurrencesRecords.length; index++) {
			if(this.typesOccurrencesRecords[index].sended == null){
				return false;
			}
		}
		return true;
	}

}

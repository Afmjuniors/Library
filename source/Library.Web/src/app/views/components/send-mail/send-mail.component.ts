import { AppService } from './../../../core/auth/_services/app.service';
import { Component, Inject, OnInit } from '@angular/core';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';

@Component({
	selector: 'kt-send-mail',
	templateUrl: './send-mail.component.html',
	styleUrls: ['./send-mail.component.scss']
})
export class SendMailComponent implements OnInit {
	occurrenceRecordId: number;
	impactedAreaId: number;
	includeQas: boolean;
	sending: boolean = false;
	error: boolean = false;
	sent: boolean = false;

	constructor(
		public dialogRef: MatDialogRef<SendMailComponent>,
		@Inject(MAT_DIALOG_DATA)
		public data: any,
		public dialog: MatDialog,
		private appService: AppService
	) { }

	ngOnInit() {
		this.sending = true;

		if(this.data.occurrenceRecordId && this.data.impactedAreaId){
			this.impactedAreaId = this.data.impactedAreaId;
			this.occurrenceRecordId = this.data.occurrenceRecordId;
			this.includeQas = this.data.includeQas;
			this.sendMail();
		}else{
			this.sending = false;
		}
	}

	sendMail(){
		let data = {
			areaId: this.impactedAreaId,
			occurrenceRecordId: this.occurrenceRecordId,
			includeQas: this.includeQas
		}
		this.appService.sendMailNotification(data).subscribe(res =>{
			this.sending = false;
			this.sent = true;
		}, error =>{
			this.sending = false;
			this.error = true;
		})
	}

}

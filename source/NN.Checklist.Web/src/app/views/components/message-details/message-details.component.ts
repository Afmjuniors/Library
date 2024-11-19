import { Component, Inject, OnInit } from '@angular/core';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { Message } from './../../../core/auth/_models/message.model';
import { AppService } from './../../../core/auth/_services';
import { LayoutUtilsService } from './../../../core/_base/crud';

@Component({
  selector: 'kt-message-details',
  templateUrl: './message-details.component.html',
  styleUrls: ['./message-details.component.scss']
})
export class MessageDetailsComponent implements OnInit {
	message: Message = new Message();
	loading: boolean = false;

	constructor(
		public dialogRef: MatDialogRef<MessageDetailsComponent>,
		@Inject(MAT_DIALOG_DATA)
		public data: any,
		public dialog: MatDialog,
		public app: AppService,
		private layoutUtilsService: LayoutUtilsService,
	) { }

	ngOnInit() {
		if(this.data.message){
			this.message = this.data.message;
			this.listStatusByMessage();
		}
	}
	
	
	listStatusByMessage(){
		if(this.message.messageId > 0){
			this.loading = true;
			this.app.listStatusByMessage(this.message.messageId).subscribe(res =>{
				this.message.messageStatus = res;
				this.loading = false;
			},
			error =>{
				this.loading = false;
				this.layoutUtilsService.showErrorNotification(error);
			});
		}
	}


}

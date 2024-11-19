import { TypeOccurrenceRecordGroup } from './../../../../core/auth/_models/typeOccurrenceRecordGroup.model';
import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { AppService } from '../../../../core/auth/_services';
import { LayoutUtilsService, MessageType } from '../../../../core/_base/crud';
import { ConfigService } from '../../../../core/auth/_services/config.service';

@Component({
	selector: 'kt-remove-type-group',
	templateUrl: './remove-type-group.component.html',
	styleUrls: ['./remove-type-group.component.scss']
})
export class RemoveTypeGroupComponent implements OnInit {
	type = new TypeOccurrenceRecordGroup();
	loading: boolean = false;

	constructor(
		public dialogRef: MatDialogRef<RemoveTypeGroupComponent>,
		@Inject(MAT_DIALOG_DATA)
		public data: any,
		public app: AppService,
		public configService: ConfigService,
		private layoutUtilsService: LayoutUtilsService,
	) { }

	ngOnInit() {
		this.type = this.data.type;
	}

	deleteGroup(){
		this.configService.DeleteRegisterGroup({
			groupId: this.type['typeOccurrenceGroupId']
		}).subscribe(x=> {
			this.layoutUtilsService.showActionNotification(x.message, MessageType.Delete);
			this.closeDialog();
		},
		error =>
		{
			this.layoutUtilsService.showErrorNotification(error);
		});
	}

	closeDialog() {
		this.dialogRef.close();
	}
}

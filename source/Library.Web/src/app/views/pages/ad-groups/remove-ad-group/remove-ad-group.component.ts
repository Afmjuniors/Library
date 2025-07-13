import { Component, Inject, OnInit } from '@angular/core';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { TranslateService } from '@ngx-translate/core';
import { SignatureComponent } from '../../../../views/components/signature/signature.component';
import { AdGroup } from './../../../../core/auth/_models/adGroup.model';
import { AppService } from './../../../../core/auth/_services';
import { LayoutUtilsService, MessageType } from './../../../../core/_base/crud';

@Component({
  selector: 'kt-remove-ad-group',
  templateUrl: './remove-ad-group.component.html',
  styleUrls: ['./remove-ad-group.component.scss']
})
export class RemoveAdGroupComponent implements OnInit {

	adGroup = new AdGroup();
	loading: boolean = false;

	remainingText: number = 8000;
	comments: string = "";

	constructor(
		public dialogRef: MatDialogRef<RemoveAdGroupComponent>,
		@Inject(MAT_DIALOG_DATA)
		public data: any,
		public app: AppService,
		private layoutUtilsService: LayoutUtilsService,
		public dialog: MatDialog,
		public translateService: TranslateService,
	) { }

	ngOnInit() {
		this.adGroup = this.data.adGroup;
	}

	remove() {
		this.loading = true;
		const _messageType = MessageType.Delete;
		const erroMessage = "adGroup não pode ser excluído"
		this.app.removeAdGroup(this.adGroup, this.comments)
		.subscribe(res => {
			if (!res) {
				this.loading = false;
				this.layoutUtilsService.showActionNotification(erroMessage, _messageType, 10000, true, true);
				this.dialogRef.close();
				return;
			}

			this.loading = false;
			this.dialogRef.close({
				removido: true
			});
		},
		error =>
		{
			this.loading = false;
			this.layoutUtilsService.showActionNotification(error.error, _messageType, 10000, true, true);
		});
	}

	sign()
	{
		if (this.comments == null || this.comments.trim().length == 0)
		{
			this.layoutUtilsService.showActionNotification(this.translateService.instant("COMMENTS_NOT_PROVIDED"), null,10000);
			return;
		}

		this.dialog.open(SignatureComponent, {
			minHeight: '300px',
			width: '400px',
			data: true
		}).afterClosed()
		.subscribe(x => {
			if(x != '' && x != undefined){
				this.remove();
			}
		});
	}

	closeDialog() {
		this.dialogRef.close();
	}

	valueChange(value) {
		this.remainingText = 8000 - value;
	}
}

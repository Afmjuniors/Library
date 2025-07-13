import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA } from '@angular/material';
import { LayoutUtilsService, MessageType } from '../../../core/_base/crud';
import { AppService } from '../../../core/auth/_services';

@Component({
	selector: 'kt-pdf-view',
	templateUrl: './pdf-view.component.html',
	styleUrls: ['./pdf-view.component.scss']
})
export class PdfViewComponent implements OnInit {

	loading: boolean = false;
	document;
	item;
	pdfSrc = "https://vadimdez.github.io/ng2-pdf-viewer/assets/pdf-test.pdf";
	constructor(
		@Inject(MAT_DIALOG_DATA)
		public data: any,
		public app: AppService,
		private layoutUtilsService: LayoutUtilsService,
	) { }

	ngOnInit() {
		this.item = this.data.item;
		this.download(this.item);
	}

	download(item: string) {
		this.loading = true
		this.app.downloadFile(item)
			.subscribe(x => {
				var newBlob = new Blob([x], { type: "application/pdf" });
				if (window.navigator && window.navigator.msSaveOrOpenBlob) {
					window.navigator.msSaveOrOpenBlob(newBlob);
					return;
				}
				const data = window.URL.createObjectURL(newBlob);
				this.document = data
				this.loading = false
			},
				error => {
					this.loading = false;
					this.layoutUtilsService.showErrorNotification(error, MessageType.Delete, 10000, true, false);
				}
			);

	}

}

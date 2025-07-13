import { Component, Inject, OnInit } from '@angular/core';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';

@Component({
	selector: 'kt-view-text',
	templateUrl: './view-text.component.html',
	styleUrls: ['./view-text.component.scss']
})
export class ViewTextComponent implements OnInit {
	text: string = "";

	constructor(
		public dialogRef: MatDialogRef<ViewTextComponent>,
		@Inject(MAT_DIALOG_DATA)
		public data: any,
		public dialog: MatDialog
	) { }

	ngOnInit() {
		if(this.data.text){
			this.text = this.data.text;
		}
	}

	

}

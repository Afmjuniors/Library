import { Component, Inject, OnInit } from '@angular/core';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { OccurrenceRecordFlux } from './../../../core/auth/_models/occurrenceRecordFlux.model';

@Component({
	selector: 'kt-view-analysis',
	templateUrl: './view-analysis.component.html',
	styleUrls: ['./view-analysis.component.scss']
})
export class ViewAnalysisComponent implements OnInit {
	analysis: OccurrenceRecordFlux = new OccurrenceRecordFlux();

	constructor(
		public dialogRef: MatDialogRef<ViewAnalysisComponent>,
		@Inject(MAT_DIALOG_DATA)
		public data: any,
		public dialog: MatDialog
	) { }

	ngOnInit() {
		if(this.data.analysis){
			this.analysis = this.data.analysis;
		}
	}

}

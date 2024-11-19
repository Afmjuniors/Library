import { ChangeDetectionStrategy, ChangeDetectorRef, Component, Inject, NgZone, OnInit } from '@angular/core';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { AppService } from '../../../../core/auth/_services/app.service';
import { OccurrenceAnalysisDetails } from '../../../../core/auth/_models/occurrenceAnalysisDetails.model';
import { FormBuilder } from '@angular/forms';
import { LayoutUtilsService } from '../../../../core/_base/crud';
import { nextTick } from 'process';
import { OccurrenceRecordFlux } from '../../../../core/auth/_models/occurrenceRecordFlux.model';
import { ViewAnalysisComponent } from '../../../components/view-analysis/view-analysis.component';
import { OccurrenceAnalysisDetailsItem } from '../../../../core/auth/_models/occurrenceAnalysisDetailsItem.model';
import objectPath from 'object-path';

@Component({
	selector: 'kt-impact-analysis-details',
	templateUrl: './impact-analysis-details.component.html',
	styleUrls: ['./impact-analysis-details.component.scss']
})
export class ImpactAnalysisDetailsComponent implements OnInit {
	loading: boolean = false;
	occurrenceId: number;
	occurrenceDetails = new  OccurrenceAnalysisDetails();
	analysis: [] = [];
	showData: boolean = false;
	occurrenceFlux: OccurrenceRecordFlux[] = [];

	constructor(
		public dialogRef: MatDialogRef<ImpactAnalysisDetailsComponent>,
		@Inject(MAT_DIALOG_DATA)
		public data: any,
		private app: AppService,
		public fb: FormBuilder,
		private layoutUtilsService: LayoutUtilsService,
		public dialog: MatDialog,
		private ref: ChangeDetectorRef
	) { }

	ngOnInit() {
		// this.loading = true;
		let ids:number[] = [this.data.occurrenceId]

		if(this.data.occurrenceId != null){
			this.getDetails();
			this.listFlux();
		}
	}

	getDetails(){
		var obj: OccurrenceAnalysisDetailsItem = new OccurrenceAnalysisDetailsItem();

		obj.occurrenceRecordId = this.data.occurrenceId;
		obj.impactedAreaId = this.data.impactedAreaId;

		this.app.getOccurrenceAnalysisDetails(obj).subscribe(x => {
			this.occurrenceDetails = x;
			this.loading = false;
		},
		error =>
		{
			this.layoutUtilsService.showErrorNotification(error);
		});
	}

	listFlux(){
		this.app.listOccurrenceRecordsFlux(this.data.occurrenceId, this.data.impactedAreaId).subscribe(x => {
			this.occurrenceFlux = x;
			this.loading = false;
		},
		error =>
		{
			this.layoutUtilsService.showErrorNotification(error);
		});
	}

	viewSignatureDetails(item){
		const dialogRef = this.dialog.open(ViewAnalysisComponent, {
			minHeight: '300px',
			width: '40%',
			data: {analysis: item}
		});
		dialogRef.afterClosed().subscribe(res => {
			if(res){

			}
		});
	}
}

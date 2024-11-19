import { ChangeDetectorRef, Component, Inject, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { SignatureComponent } from '../../../components/signature/signature.component';
import { OccurrenceAnalysisDetails } from '../../../../core/auth/_models/occurrenceAnalysisDetails.model';
import { AppService } from '../../../../core/auth/_services';
import { LayoutUtilsService } from '../../../../core/_base/crud';
import { OccurrenceAnalysisDetailsItem } from '../../../../core/auth/_models/occurrenceAnalysisDetailsItem.model';
import { AnalysisDetails } from '../../../../core/auth/_models/analysisDetails.model';
import { OccurrenceRecordFlux } from '../../../../core/auth/_models/occurrenceRecordFlux.model';
import { ViewAnalysisComponent } from '../../../components/view-analysis/view-analysis.component';

@Component({
	selector: 'kt-details-perform-impact-analysis',
	templateUrl: './details-perform-impact-analysis.component.html',
	styleUrls: ['./details-perform-impact-analysis.component.scss']
})
export class DetailsPerformImpactAnalysisComponent implements OnInit {

	loading: boolean = false;
	occurrences: OccurrenceAnalysisDetailsItem[];
	occurrenceDetails: OccurrenceAnalysisDetails[] = [];
	occurrenceView: OccurrenceAnalysisDetails = new OccurrenceAnalysisDetails();
	showData: boolean = false;
	impact: boolean = false;
	description: string = "";
	remainingText: number = 8000;
	occurrenceFlux: OccurrenceRecordFlux[] = [];

	constructor(
		public dialogRef: MatDialogRef<DetailsPerformImpactAnalysisComponent>,
		@Inject(MAT_DIALOG_DATA)
		public data: any,
		private app: AppService,
		public fb: FormBuilder,
		private layoutUtilsService: LayoutUtilsService,
		public dialog: MatDialog,
		private ref: ChangeDetectorRef
	) {

	}

	ngOnInit() {
		// this.loading = true;		
		this.occurrences = this.data.occurrences;
		if(this.data.occurrences != null){
			this.listDetails();	
		}
	}

	setOccurrenceView(index: number){
		if(this.occurrenceView.idOccurrenceRecord == this.occurrenceDetails[index].idOccurrenceRecord){
			this.occurrenceView = new OccurrenceAnalysisDetails();
			this.showData = false;
			this.listFlux(this.occurrenceDetails[index].idOccurrenceRecord, this.occurrenceDetails[index].impactedAreaId);
		}else{
			this.occurrenceView = this.occurrenceDetails[index];
			this.showData = true;			
			this.listFlux(this.occurrenceView.idOccurrenceRecord, this.occurrenceView.impactedAreaId);
		}
	}

	listDetails(){
		var obj = new AnalysisDetails();
			obj.occurrences = this.occurrences;
			this.app.listOccurrenceAnalysisDetails(
				obj
			).subscribe(x => {
				this.occurrenceDetails = x;
				if(this.occurrenceDetails.length == 1){
					this.setOccurrenceView(0);
					this.showData = true;
				}
			},
			error =>{
				this.layoutUtilsService.showErrorNotification(error);
			});
	}

	listFlux(occurrenceId, impactedAreaId){
		this.app.listOccurrenceRecordsFlux(occurrenceId, impactedAreaId).subscribe(x => {
			this.occurrenceFlux = x;
			this.loading = false;
			this.ref.detectChanges();
		},
		error =>
		{
			this.layoutUtilsService.showErrorNotification(error);
		});
	}

	setAnalysis(data: any){
		this.loading = true;
		this.app.performAnalysis(data).subscribe(x => {
			this.dialogRef.close();
			this.loading = false;
		},
		error =>{
			this.loading = false;
			this.layoutUtilsService.showErrorNotification(error);
		});
	}

	sign(){
		const dialogRef = this.dialog.open(SignatureComponent, {
			minHeight: '300px',
			width: '400px',
			data: this.impact
		});
		dialogRef.afterClosed().subscribe(res => {
			if(res){
				this.setAnalysis({
					occurrences: this.occurrences,
					stamp: res,
					impact: this.impact,
					description: this.description
				});
			}
		});
	}

	valueChange(value) {
		this.remainingText = 8000 - value;
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

import { Message } from './../../../../../core/auth/_models/message.model';
import { ChangeDetectorRef, Component, Inject, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { SignatureComponent } from '../../../../components/signature/signature.component';
import { OccurrenceAnalysisDetails } from '../../../../../core/auth/_models/occurrenceAnalysisDetails.model';
import { AppService } from '../../../../../core/auth/_services';
import { LayoutUtilsService } from '../../../../../core/_base/crud';
import { QAApproval } from '../../../../../core/auth/_models/QAApproval.model';
import { QAService } from '../../../../../core/auth/_services/qa.service';
import { AnalysisDetails } from '../../../../../core/auth/_models/analysisDetails.model';
import { OccurrenceAnalysisDetailsItem } from '../../../../../core/auth/_models/occurrenceAnalysisDetailsItem.model';
import { ViewAnalysisComponent } from '../../../../components/view-analysis/view-analysis.component';
import { OccurrenceRecordFlux } from '../../../../../core/auth/_models/occurrenceRecordFlux.model';

@Component({
	selector: 'kt-details-qa-analysis',
	templateUrl: './details-qa-analysis.component.html',
	styleUrls: ['./details-qa-analysis.component.scss']
})
export class DetailsQAAnalysisComponent implements OnInit {

	loading: boolean = false;
	occurrences: OccurrenceAnalysisDetailsItem[];
	occurrenceDetails: OccurrenceAnalysisDetails[] = [];
	occurrenceView: OccurrenceAnalysisDetails = new OccurrenceAnalysisDetails();
	showData: boolean = false;
	result: boolean = false;
	description: string = "";
	remainingText: number = 8000;
	review: boolean = false;
	occurrenceFlux: OccurrenceRecordFlux[] = [];

	constructor(
		public dialogRef: MatDialogRef<DetailsQAAnalysisComponent>,
		@Inject(MAT_DIALOG_DATA)
		public data: any,
		private app: AppService,
		private qaService: QAService,
		public fb: FormBuilder,
		private layoutUtilsService: LayoutUtilsService,
		public dialog: MatDialog,
		private ref: ChangeDetectorRef
	) {

	}

	ngOnInit() {
		this.loading = true;
		this.occurrences = this.data.occurrences;
		if(this.data.occurrences != null){

			this.listDetails();
		}
		if (this.data.review != null)
		{
			this.review = this.data.review;
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
				this.loading = false;
			},
			error =>
			{
				this.loading = false;
				this.layoutUtilsService.showErrorNotification(error.message);
			});
	}

	sign()
	{
		this.dialog.open(SignatureComponent, {
			minHeight: '300px',
			width: '400px',
			data: this.result
		}).afterClosed()
		.subscribe(x => {
			if(x != '' && x != undefined){
				this.loading = true;
				let dados:QAApproval = new QAApproval();
				dados.approved = this.result;
				dados.review = this.review;
				dados.description = this.description;
				dados.occurrences = this.occurrences;
				dados.stamp = x;
				this.qaService.assessOccurrences(dados).subscribe(y => {
					this.loading = false;
					this.dialogRef.close(y);
					this.closeModal();
				},
				error =>
				{
					this.loading = false;
					this.layoutUtilsService.showErrorNotification(error);
				});
				this.closeModal();
			}
		});
	}

	closeModal()
	{
		this.dialog.closeAll();
	}

	valueChange(value) {
		this.remainingText = 8000 - value;
	}

	listFlux(occurrenceId, impactedAreaId){
		this.app.listOccurrenceRecordsFlux(occurrenceId, impactedAreaId).subscribe(x => {
			this.occurrenceFlux = x;
			this.loading = false;
			this.ref.detectChanges();
		},
		error =>
		{
			this.loading = false;
			this.ref.detectChanges();
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

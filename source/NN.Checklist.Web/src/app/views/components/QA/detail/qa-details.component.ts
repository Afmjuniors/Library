import { Component, OnInit, ViewChild, ChangeDetectorRef, Inject } from '@angular/core';
import { Observable, merge } from 'rxjs';
import { Permission, AuthService, User, currentUser } from '../../../../core/auth';
import { MatPaginator, MatSort, MatDialog } from '@angular/material';
import { LayoutUtilsService, MessageType, QueryParamsModel } from '../../../../core/_base/crud';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { select, Store } from '@ngrx/store';
import { AppState } from '../../../../core/reducers';
import { Router } from '@angular/router';

import { TranslateService } from '@ngx-translate/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { SignatureService } from '../../../../core/auth/_services/signature.service';
import { Signature } from '../../../../core/auth/_models/signature.model';
import { QADetailsResponsibleDataSource } from '../../../../core/auth/_data-sources/qaDetailsResponsible.datasource';
import { QAService } from '../../../../core/auth/_services/qa.service';
import { QADetailFilter } from '../../../../core/auth/_models/QADetailFilter.model';
import { OccurrenceRecord } from '../../../../core/auth/_models/occurenceRecord.model';
import { QASign } from '../../../../core/auth/_models/qaSign.model';
import { AppService } from '../../../../core/auth/_services';
import { OccurrenceRecordFlux } from '../../../../core/auth/_models/occurrenceRecordFlux.model';
import { OccurrenceAnalysisDetails } from '../../../../core/auth/_models/occurrenceAnalysisDetails.model';
import { ViewAnalysisComponent } from '../../view-analysis/view-analysis.component';
import { OccurrenceAnalysisDetailsItem } from '../../../../core/auth/_models/occurrenceAnalysisDetailsItem.model';

@Component({
	selector: 'apta-qa-details',
	templateUrl: './qa-details.component.html',
	styleUrls: ['./qa-details.component.scss'],
	providers: [SignatureService]
})
export class QADetailsComponent implements OnInit{

	loading: boolean = false;
	permissionTag: string = '';
	user: User = null;
	password: string = '';
	errors: string[] = [];
    analyseStatus: boolean = false;
    ResponsibleDS: QASign[];
    filter: QADetailFilter = new QADetailFilter();
	columnWithAnalyse  = ['responsible', 'dth', 'type'];
	occurrenceFlux: OccurrenceRecordFlux[] = [];
	occurrenceDetails = new  OccurrenceAnalysisDetails();
	showData: boolean = false;

	/**
	 * Component constructor
	 *
	 * @param store: Store<AppState>
	 * @param router: Router
	 */
	constructor(
        public store: Store<AppState>,
		public dialog: MatDialog,
		public translateService: TranslateService,
		public dialogRef: MatDialogRef<string>,
		public signatureService: SignatureService,
        public qaService:QAService,
		private app: AppService,
		private layoutUtilsService: LayoutUtilsService,
        @Inject(MAT_DIALOG_DATA)
		public data: any,
	) {
		let ids:number[] = [this.data.occurrenceId]

		if(this.data.occurrenceRecordId != null){			
			var obj = new OccurrenceAnalysisDetailsItem();
			obj.occurrenceRecordId = this.data.occurrenceRecordId;
			obj.impactedAreaId = this.data.impactedAreaId;
			this.app.getOccurrenceAnalysisDetails(
				obj
			).subscribe(x => {
				this.occurrenceDetails = x;
				this.showData = true;
				this.listFlux();
			},
			error =>{			
				this.layoutUtilsService.showErrorNotification(error);
			});

		}

	}

	/**
	 * On init
	 */
	ngOnInit()
    {
        this.store.pipe(select(currentUser)).subscribe((x:User) => 
		{
			this.user = x;
		},
		error =>{			
			this.layoutUtilsService.showErrorNotification(error);
		});

	}

	/**
	 * On Destroy
	 */
	ngOnDestroy()
	{
		//this.subscriptions.forEach(el => el.unsubscribe());
	}

	validateSignature()
	{

	}

	closeModal()
	{

		this.dialog.closeAll();
	}

	listFlux(){
		this.app.listOccurrenceRecordsFlux(this.occurrenceDetails.idOccurrenceRecord, this.occurrenceDetails.impactedAreaId).subscribe(x => {
			this.occurrenceFlux = x;
			this.loading = false;
		},
		error =>{			
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

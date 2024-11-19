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
import { QAService } from '../../../../core/auth/_services/qa.service';
import { QADetailFilter } from '../../../../core/auth/_models/QADetailFilter.model';
import { OccurrenceRecord } from '../../../../core/auth/_models/occurenceRecord.model';
import { QASign } from '../../../../core/auth/_models/qaSign.model';
import { SignatureComponent } from '../../signature/signature.component';
import { QAApproval } from '../../../../core/auth/_models/QAApproval.model';
import { OccurrenceAnalysisDetailsItem } from '../../../../core/auth/_models/occurrenceAnalysisDetailsItem.model';

@Component({
	selector: 'apta-qa-approval',
	templateUrl: './qa-approval.component.html',
	styleUrls: ['./qa-approval.component.scss'],
	providers: [SignatureService]
})
export class QAApprovalComponent implements OnInit{
	loading: boolean = false;
	permissionTag: string = '';
	user: User = null;
	password: string = '';
	errors: string[] = [];
    analyseStatus: boolean = false;
    filter: QADetailFilter = new QADetailFilter();
	columnWithAnalyse  = ['responsible', 'dth', 'type'];
	description: string = '';
	occurrences: OccurrenceAnalysisDetailsItem[] = [];
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
		private layoutUtilsService: LayoutUtilsService,
        @Inject(MAT_DIALOG_DATA)
		public data: OccurrenceRecord[],
	) {
		data.map(x=>{
			var obj: OccurrenceAnalysisDetailsItem = new OccurrenceAnalysisDetailsItem();
			obj.occurrenceRecordId = x.occurrenceRecordId;

			this.occurrences.push(obj);
		})
	}

	/**
	 * On init
	 */
	ngOnInit() 
    {
        this.store.pipe(select(currentUser)).subscribe((x:User) => {
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
		this.dialog.open(SignatureComponent, {
			minHeight: '300px',
			width: '400px',
			data: this.analyseStatus
		}).afterClosed()
		.subscribe(x => {
			if(x != '' && x != undefined){
				let dados:QAApproval = new QAApproval();
				dados.approved = this.analyseStatus;
				dados.description = this.description;
				dados.occurrences = this.occurrences;
				dados.stamp = x;
				this.qaService.assessOccurrences(dados).subscribe(y => {
					this.dialogRef.close(y);
					this.closeModal();
				},
				error =>{			
					this.layoutUtilsService.showErrorNotification(error);
				});
				this.closeModal();
			}
		},
		error =>{			
			this.layoutUtilsService.showErrorNotification(error);
		});
	}

	closeModal()
	{
		this.dialog.closeAll();
	}
}

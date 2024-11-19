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
import { OccurrenceRecordFilter } from '../../../../core/auth/_models/occurrenceRecordFilter.model';

@Component({
	selector: 'apta-qa-approval-report',
	templateUrl: './qa-approval-report.component.html',
	styleUrls: ['./qa-approval-report.component.scss'],
	providers: [SignatureService]
})
export class QAApprovalReportComponent implements OnInit{

	displayedColumns = ['alarms', 'messages', 'process', 'area', 'date', 'responsible', 'status'];
	@ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;
	@ViewChild('sort1', { static: true }) sort: MatSort;
	loading: boolean = false;
	permissionTag: string = '';
	user: User = null;
	password: string = '';
	errors: string[] = [];
    analyseStatus: boolean = false;
    ResponsibleDS: QASign[];
    filter: OccurrenceRecordFilter = new OccurrenceRecordFilter();
	columnWithAnalyse  = ['responsible', 'dth', 'type'];
	dth_generate:Date = new Date()
	occurrences: OccurrenceRecord[];
	dialogMailControl: boolean = false;
	area:string;
	process:string;
	status:string;
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
		public data: any,
	) {
		
		this.occurrences = data.occurrences;
		data.status.map(x => {
			if(data.filters.status == x.statusId){
				this.status = x.description;
			}
		})
		data.processes.map(x => {
			if(data.filters.process == x.processId){
				this.process = x.description;
			}
		})
		data.areas.map(x => {
			if(data.filters.area == x.areaId){
				this.area = x.description;
			}
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
		if(!this.dialogMailControl)
			this.dialogRef.close('no-mail');
	}

	validateSignature()
	{

	}

	closeModal()
	{

		this.dialog.closeAll();
	}

	sendMail()
	{
		this.dialogMailControl = true;
		this.dialogRef.close("send-mail");
	}
}

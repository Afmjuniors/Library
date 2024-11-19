import { MessageFilter } from './../../../core/auth/_models/messageFilter.model';
import { MessagesDataSource } from './../../../core/auth/_data-sources/messages.datasource';
import { ChangeDetectorRef, Component, Inject, OnInit, ViewChild } from '@angular/core';
import { MatDialog, MatDialogRef, MatPaginator, MatSort, MAT_DIALOG_DATA } from '@angular/material';
import { AppService } from './../../../core/auth/_services';
import { LayoutUtilsService, MessageType } from './../../../core/_base/crud';
import { distinctUntilChanged, finalize, skip, takeUntil, tap } from 'rxjs/operators';
import { Subject } from 'rxjs';
import { Store } from '@ngrx/store';
import { AppState } from './../../../core/reducers';
import { Message } from './../../../core/auth/_models/message.model';
import { BasePageComponent } from '../../pages/BasePage.component';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ViewAnalysisComponent } from '../view-analysis/view-analysis.component';
import { MessageDetailsComponent } from '../message-details/message-details.component';

@Component({
	selector: 'kt-message-history',
	templateUrl: './message-history.component.html',
	styleUrls: ['./message-history.component.scss']
})
export class MessageHistoryComponent implements OnInit {

	firstFormGroup: FormGroup = this._formBuilder.group({firstCtrl: ['']});
	secondFormGroup: FormGroup = this._formBuilder.group({secondCtrl: ['']});


	// Table fields
	dataSource: MessagesDataSource;
	displayedColumns = ['action', 'datetime', 'type', 'destiny', 'addressee', 'subject', 'message'];
	@ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;
	@ViewChild('sort1', { static: true }) sort: MatSort;
	MessageResult: Message[] = [];
	loading: boolean = false;
	filter: MessageFilter = new MessageFilter();
	selectedMessage: Message = new Message();


	private unsubscribe: Subject<any>;
	error: Boolean = false;
	mensagemErro: string = '';

	constructor(
		private _formBuilder: FormBuilder,
		public store: Store<AppState>,
		public dialogRef: MatDialogRef<MessageHistoryComponent>,
		@Inject(MAT_DIALOG_DATA)
		public data: any,
		public app: AppService,
		private layoutUtilsService: LayoutUtilsService,
		private detector: ChangeDetectorRef,
		public dialog: MatDialog,
	) {
		this.unsubscribe = new Subject();
	}

	ngOnInit() {
		this.filter.occurrenceRecordId = this.data.occurrenceRecordId;
		this.initMessages();
	}

	initMessages(){

		this.paginator.page.pipe(
			tap(() => {
				this.loading = true;
				this.loadMessages();
			}, error => {
				this.layoutUtilsService.showErrorNotification(error, MessageType.Create);
			}),
			takeUntil(this.unsubscribe),
			finalize(() => {
				this.loading = false;
				this.detector.detectChanges();
			})
		).subscribe();


		//Init DataSource
		this.dataSource = new MessagesDataSource(this.app, this.store, this.filter);
		const entitiesSubscription = this.dataSource.entitySubject.pipe(
			skip(1),
			distinctUntilChanged()
		).subscribe(res => {
			this.MessageResult = res;
			this.loading = false;
		},error =>{
			this.mensagemErro = '';
			this.error = true;
			this.mensagemErro = error.message;
			this.loading = false;
		});
	}

	loadMessages(): void {
		this.dataSource.load(this.paginator.pageIndex, this.paginator.pageSize, this.filter);
	}

	viewStatus(message: Message){
		if(message.status != null){
			this.selectedMessage = message;
		}
	}

	viewSignatureDetails(item){
		const dialogRef = this.dialog.open(MessageDetailsComponent, {
			minHeight: '300px',
			width: '40%',
			data: {message: item}
		});
		dialogRef.afterClosed().subscribe(res => {
			if(res){

			}
		});
	}
}

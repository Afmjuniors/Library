import { Component, OnInit, ViewChild, ChangeDetectorRef } from '@angular/core';
import { BasePageComponent } from '../BasePage.component';
import { Observable, merge } from 'rxjs';
import { Permission, AuthService } from '../../../core/auth';
import { MatPaginator, MatSort, MatDialog } from '@angular/material';
import { LayoutUtilsService, MessageType, QueryParamsModel } from '../../../core/_base/crud';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { Store } from '@ngrx/store';
import { AppState } from '../../../core/reducers';
import { Router } from '@angular/router';

import { TranslateService } from '@ngx-translate/core';

import { DBParameter } from '../../../core/auth/_models/_parameter/DBParameter.model'
import { DomainParameter } from '../../../core/auth/_models/_parameter/DomainParameter.models'
import { PolicyParameter } from '../../../core/auth/_models/_parameter/PolicyParameter.models'
import { MailParameter } from '../../../core/auth/_models/_parameter/MailParameter.model';
import { ParameterService } from '../../../core/auth/_services/parameter.service';
import { IdleService } from '../../../core/_base/layout/services/idle.service';
import { SignatureComponent } from '../../components/signature/signature.component';


@Component({
	selector: 'kt-parameter',
	templateUrl: './parameter.component.html',
	styleUrls: ['./parameter.component.scss'],
	providers: []
})
export class ParameterComponent extends BasePageComponent implements OnInit  {

	error: Boolean = false;
	errorMessage: string = '';

	//panels
	panelStateDomain: boolean = false;
	panelStateDB: boolean = false;
	panelStatePolicy: boolean = false;
	panelStateMail: boolean = false;

	//permission to edit params
	btnEditDomainStatus: boolean = false;
	btnEditDBStatus: boolean = false;
	btnEditPolicyStatus: boolean = false;
	btnEditMailStatus: boolean = false;

	//available parameters
	domainParameter: DomainParameter = new DomainParameter();
	dbParameter: DBParameter = new DBParameter();
	policyParameter: PolicyParameter = new PolicyParameter();
	mailParameter: MailParameter = new MailParameter();

	remainingTextDB: number = 8000;
	remainingTextDomain: number = 8000;
	remainingTextPolicy: number = 8000;
	remainingTextMail: number = 8000;

	commentsDB: string = "";
	commentsDomain: string = "";
	commentsPolicy: string = "";
	commentsMail: string = "";

	loading: boolean = false;
	permissionTag: string = '';
	/**
	 * Component constructor
	 *
	 * @param store: Store<AppState>
	 * @param router: Router
	 */
	constructor(public store: Store<AppState>,
		router: Router,
		private layoutUtilsService: LayoutUtilsService,
		public dialog: MatDialog,
		private fb: FormBuilder,
		private ref: ChangeDetectorRef,
		public translateService: TranslateService,
		auth: AuthService,
		public parameterService: ParameterService,
		public idleService: IdleService,) {
		super(auth, store, translateService, router,'MANAGE_PARAMETER', [], translateService.instant("PARAMETERS"), parameterService, idleService)
	}

	/**
	 * On init
	 */
	ngOnInit() {
		this.StartParameterComponent();
	}
	/**
	 * On Destroy
	 */
	 ngOnDestroy() {
		//this.subscriptions.forEach(el => el.unsubscribe());
	}

	/**
	 * Flags
	 */
	onAlertClose($event) {
		this.error = false;
	}

	DBParameter_Edit(){

		this.btnEditDBStatus = true;
	}

	DomainParameter_Edit(){

		this.btnEditDomainStatus = true;
	}

	PolicyParameter_Edit(){

		this.btnEditPolicyStatus = true;
	}

	MailParameter_Edit(){
		this.btnEditMailStatus = true;
	}
	/**
	 * Start Section
	 */
	StartParameterComponent() {
		//
		this.loading = true;

		this.parameterService.GetDBParameter().subscribe(x => {
			if (x) {
				this.dbParameter = x;
			}
			this.loading = false;
			this.ref.detectChanges();
		}, error => {
			this.loading = false;
			this.layoutUtilsService.showErrorNotification(error);
		});

		this.parameterService.GetDomainParameter().subscribe(x => {
			if (x) {
				this.domainParameter = x;
			}
			this.loading = false;
			this.ref.detectChanges();
		}, error => {
			this.loading = false;

			this.layoutUtilsService.showErrorNotification(error.error);
		});

		this.parameterService.GetPolicyParameter().subscribe(x => {
			if (x) {
				this.policyParameter = x;
			}
			this.loading = false;
			this.ref.detectChanges();
		}, error => {
			this.loading = false;

			this.layoutUtilsService.showErrorNotification(error);
		});

		this.parameterService.GetMailParameter().subscribe(x => {
			if (x) {
				this.mailParameter = x;
			}
			this.loading = false;
			this.ref.detectChanges();
		}, error => {
			this.loading = false;

			this.layoutUtilsService.showErrorNotification(error);
		});
	}

	/**
	 * Stores
	 */
	DomainParameter_Store(){
		if(this.isDomainParameterValid()){
			this.loading = true;
			this.parameterService.SetDomainParameter(this.domainParameter, this.commentsDomain).subscribe(x => {
				if (x) {
					this.domainParameter = x;

					this.btnEditDomainStatus = false;
				}
				//this.edicaoAD = false;
				this.loading = false;
				this.ref.detectChanges();
				this.StartParameterComponent();
			}, error => {
				this.loading = false;

				this.layoutUtilsService.showErrorNotification(error);
			});
		}
	}

	DBParameter_Store(){
		if(this.isDBParameterValid()){
			this.loading = true;
			this.parameterService.SetDBParameter(this.dbParameter, this.commentsDB).subscribe(x => {
				if (x) {
					this.dbParameter = x;

					this.btnEditDBStatus = false;
				}
				//this.edicaoSQLServer = false;
				this.loading = false;
				this.ref.detectChanges();
				this.StartParameterComponent();
			}, error => {
				this.loading = false;

				this.layoutUtilsService.showErrorNotification(error);
			});
		}
	}

	PolicyParameter_Store(){
		if(this.isPolicyParameterValid()){
			this.loading = true;
			this.parameterService.SetPolicyParameter(this.policyParameter, this.commentsPolicy).subscribe(x => {
				if (x) {
					this.policyParameter = x;
					this.btnEditPolicyStatus = false;
				}
				//this.edicaoAdm = false;
				this.loading = false;
				this.ref.detectChanges();
				this.StartParameterComponent();
				window.location.reload();
			}, error => {
				this.loading = false;
				this.layoutUtilsService.showErrorNotification(error);
			});
		}
	}

	MailParameter_Store(){
		if(this.isMailParameterValid()){
			this.loading = true;
			if (this.mailParameter.smtpEnabledSSL == null)
			{
				this.mailParameter.smtpEnabledSSL = false;
			}
			this.parameterService.SetMailParameter(this.mailParameter, this.commentsMail).subscribe(x => {
				if (x) {
					this.mailParameter = x;
					this.btnEditMailStatus = false;
				}
				//this.edicaoAdm = false;
				this.loading = false;
				this.ref.detectChanges();
				this.StartParameterComponent();
			}, error => {
				this.loading = false;
				this.layoutUtilsService.showErrorNotification(error);
			});
		}
	}
	/**
	 * Validations
	 */
	isDomainParameterValid(): boolean {
		if(this.domainParameter.domainAddress  == null || this.domainParameter.domainAddress.length == 0)
		{
			this.errorMessage = this.translateService.instant("AUTH.PARAMETER_INPUT.DOMAIN_INVALID");
			this.error = true;
			this.loading = false;
		}

		if(this.domainParameter.adminPassword == null || this.domainParameter.adminPassword.length == 0){
			this.errorMessage = this.translateService.instant("AUTH.PARAMETER_INPUT.PASSWORD_INVALID");
			this.error = true;
			this.loading = false;
		}
		if(this.commentsDomain == null || this.commentsDomain.trim().length == 0){
			this.errorMessage = this.translateService.instant("COMMENTS_NOT_PROVIDED");
			this.error = true;
			this.loading = false;
		}
		return !this.error;
	}

	isDBParameterValid(): boolean {
		if(this.dbParameter.connectionStringSqlServer == null || this.dbParameter.connectionStringSqlServer.length == 0){
			this.errorMessage = this.translateService.instant("AUTH.PARAMETER_INPUT.SQL_STRING");
			this.error = true;
			this.loading = false;
		}
		if(this.dbParameter.sqlServerSchema == null || this.dbParameter.sqlServerSchema.length == 0){
			this.errorMessage = this.translateService.instant("AUTH.PARAMETER_INPUT.SQL_SCHEMA");
			this.error = true;
			this.loading = false;
		}
		if(this.commentsDB == null || this.commentsDB.trim().length == 0){
			this.errorMessage = this.translateService.instant("COMMENTS_NOT_PROVIDED");
			this.error = true;
			this.loading = false;
		}
		return !this.error;
	}

	isPolicyParameterValid(): boolean {
		if(this.policyParameter.inactivityTimeLimit  == null){
			this.errorMessage = this.translateService.instant("AUTH.PARAMETER_INPUT.INACTIVITY_TIME");
			this.error = true;
			this.loading = false;
		}

		if(this.policyParameter.timeResendAlarmsNotification.toString().trim().length == 0 || this.policyParameter.timeResendAlarmsNotification == null || this.policyParameter.timeResendAlarmsNotification < 1 || this.policyParameter.timeResendAlarmsNotification > 14400){
			this.errorMessage = this.translateService.instant("AUTH.PARAMETER_INPUT.RESEND_TIME");
			this.error = true;
			this.loading = false;
		}

		if(this.policyParameter.messageNotificationExpirationTime.toString().trim().length == 0 || this.policyParameter.messageNotificationExpirationTime == null || this.policyParameter.messageNotificationExpirationTime < 0 || this.policyParameter.messageNotificationExpirationTime > 1440){
			this.errorMessage = this.translateService.instant("AUTH.PARAMETER_INPUT.NOTIFICATION_EXPIRATION");
			this.error = true;
			this.loading = false;
		}

		if(this.policyParameter.maximumNotificationResendTime.toString().trim().length == 0 || this.policyParameter.maximumNotificationResendTime == null || this.policyParameter.maximumNotificationResendTime < 0 ){
			this.errorMessage = this.translateService.instant("AUTH.PARAMETER_INPUT.MAXIMUM_TIME_EXPIRATION");
			this.error = true;
			this.loading = false;
		}

		if(this.commentsPolicy == null || this.commentsPolicy.trim().length == 0){
			this.errorMessage = this.translateService.instant("COMMENTS_NOT_PROVIDED");
			this.error = true;
			this.loading = false;
		}
		return !this.error;
	}
	isMailParameterValid(): boolean {
		if(this.mailParameter.smtpFromAddress == null){
			this.errorMessage = this.translateService.instant("AUTH.PARAMETER_INPUT.SMTP_FROM_ADDRESS");
			this.error = true;
			this.loading = false;
		}
		if(this.mailParameter.smtpPort == null){
			this.errorMessage = this.translateService.instant("AUTH.PARAMETER_INPUT.SMTP_PORT");
			this.error = true;
			this.loading = false;
		}
		if(this.mailParameter.smtpServer == null){
			this.errorMessage = this.translateService.instant("AUTH.PARAMETER_INPUT.SMTP_SERVER");
			this.error = true;
			this.loading = false;
		}
		if(this.commentsMail == null || this.commentsMail.trim().length == 0){
			this.errorMessage = this.translateService.instant("COMMENTS_NOT_PROVIDED");
			this.error = true;
			this.loading = false;
		}
		return !this.error;
	}

	valueChangeDB(value) {
		this.remainingTextDB = 8000 - value;
	}

	valueChangeDomain(value) {
		this.remainingTextDomain = 8000 - value;
	}

	valueChangePolicy(value) {
		this.remainingTextPolicy = 8000 - value;
	}

	valueChangeMail(value) {
		this.remainingTextMail = 8000 - value;
	}

	signDomain()
	{
		this.dialog.open(SignatureComponent, {
			minHeight: '300px',
			width: '400px',
			data: true
		}).afterClosed()
		.subscribe(x => {
			if(x != '' && x != undefined){
				this.DomainParameter_Store();
			}
		});
	}

	signDB()
	{
		this.dialog.open(SignatureComponent, {
			minHeight: '300px',
			width: '400px',
			data: true
		}).afterClosed()
		.subscribe(x => {
			if(x != '' && x != undefined){
				this.DBParameter_Store();
			}
		});
	}

	signPolicy()
	{
		this.dialog.open(SignatureComponent, {
			minHeight: '300px',
			width: '400px',
			data: true
		}).afterClosed()
		.subscribe(x => {
			if(x != '' && x != undefined){
				this.PolicyParameter_Store();
			}
		});
	}

	signMail()
	{
		this.dialog.open(SignatureComponent, {
			minHeight: '300px',
			width: '400px',
			data: true
		}).afterClosed()
		.subscribe(x => {
			if(x != '' && x != undefined){
				this.MailParameter_Store();
			}
		});
	}
}

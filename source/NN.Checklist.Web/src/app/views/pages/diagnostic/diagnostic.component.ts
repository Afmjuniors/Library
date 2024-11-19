import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { BasePageComponent } from '../BasePage.component';
import { Store } from '@ngrx/store';
import { AppState } from '../../../core/reducers';
import { Router } from '@angular/router';
import { LayoutUtilsService } from '../../../core/_base/crud';
import { MatDialog } from '@angular/material';
import { FormBuilder } from '@angular/forms';
import { AuthService } from '../../../core/auth';
import { AppService, ParameterService } from '../../../core/auth/_services';
import { TranslateService } from '@ngx-translate/core';
import { IdleService } from '../../../core/_base/layout/services/idle.service';
import { WatchdogAlarm } from '../../../core/auth/_models/watchdogAlarm.model';
import { Observable, Subscription, timer } from 'rxjs';

@Component({
	selector: 'kt-diagnostic',
	templateUrl: './diagnostic.component.html',
	styleUrls: ['./diagnostic.component.scss']
})
export class DiagnosticComponent extends BasePageComponent implements OnInit {

	watchdogAlarm: WatchdogAlarm = new WatchdogAlarm();
	loading: boolean = false;
	
	//timer
	updateTimer: Observable<number> = timer(0, 60000);
	timerSubscription: Subscription;

	constructor(
		public store: Store<AppState>,
		public router: Router,
		private layoutUtilsService: LayoutUtilsService,
		public dialog: MatDialog,
		private fb: FormBuilder,
		auth: AuthService,
		public app: AppService,
		public translate: TranslateService,
		private detector: ChangeDetectorRef,
		public parameterService: ParameterService,
		public idleService: IdleService,
	) {
		super(auth, store, translate, router, "DIAGNOSTIC", [], translate.instant("MENU.DIAGNOSTIC"), parameterService, idleService)
	}

	ngOnInit() {
		this.checkWatchdogStatus();
		this.checkTimer();
	}

	checkWatchdogStatus() {
		this.loading = true;
		this.app.checkWatchdogStatus().subscribe(x => {
			this.watchdogAlarm = x;
			let index = this.watchdogAlarm.processes.findIndex(x => x.processId == 5);
			this.watchdogAlarm.processes.splice(index, 1);
			this.loading = false;
			this.detector.detectChanges();
		},
		error => {
			this.loading = false;
			this.layoutUtilsService.showActionNotification(error.message);
			this.detector.detectChanges();
		});
	}

	checkTimer() {
		const subs = this.updateTimer.subscribe(() => {
			this.checkWatchdogStatus();
		});
		this.timerSubscription = subs;
	}


}


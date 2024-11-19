// Angular
import { Component, ChangeDetectionStrategy, OnInit, OnDestroy, ViewChild, ElementRef } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
// RxJS
import { Observable, Subscription, merge, fromEvent, of } from 'rxjs';
// NGRX
import { Store, select } from '@ngrx/store';
// AppState
import { AppState } from '../../../core/reducers';
// Auth
import { AuthService, AuthNoticeService, Logout } from '../../../core/auth';
import { MatDialog } from '@angular/material';
import { LayoutUtilsService, MessageType } from '../../../core/_base/crud';
import { BasePageComponent } from '../BasePage.component';
import { TranslateService } from '@ngx-translate/core';
import { ParameterService } from '../../../core/auth/_services';
import { IdleService } from '../../../core/_base/layout/services/idle.service';


@Component({
	selector: 'apta-inicio',
	templateUrl: './inicio.component.html',
	styleUrls: ['./inicio.component.scss'],
	changeDetection: ChangeDetectionStrategy.OnPush
})
export class InicioComponent extends BasePageComponent implements OnInit, OnDestroy {

	error: Boolean = false;
	mensagemErro: string = '';

	/**
	 * Component constructor
	 *
	 * @param store: Store<AppState>
	 * @param router: Router
	 */
	constructor(public store: Store<AppState>, 
				public router: Router, 
				private route: ActivatedRoute,
				public auth: AuthService, 
				private layoutUtilsService: LayoutUtilsService,
				public dialog: MatDialog,
				public translate: TranslateService,
				private authNoticeService: AuthNoticeService,
				public parameterService: ParameterService,
				public idleService: IdleService,) 				
	{
		super(auth, store, translate, router, null, [], null, parameterService, idleService)
	}

	/**
	 * @ Lifecycle sequences => https://angular.io/guide/lifecycle-hooks
	 */

	/**
	 * On init
	 */
	ngOnInit() {		
		this.loading = false;
		const routeSubscription = this.route.queryParams.subscribe(params => {
			if (params.error) {
				this.layoutUtilsService.showActionNotification(params.error, MessageType.Delete, 5000, true, false, null, "top");
			}		
		},
		error => {
			alert(error);
		});

		this.subscriptions.push(routeSubscription);
	}

	/**
	 * On Destroy
	 */
	ngOnDestroy() {
		this.subscriptions.forEach(el => el.unsubscribe());
	}
}

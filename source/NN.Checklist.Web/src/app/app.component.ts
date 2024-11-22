import { AuthService } from './core/auth/_services/auth.service';
import { MenuConfigService } from './core/_base/layout/services/menu-config.service';
import { AuthNoticeService } from './core/auth/auth-notice/auth-notice.service';
import { Observable, Subject, Subscription } from 'rxjs';
// Angular
import { ChangeDetectionStrategy, Component, HostListener, OnDestroy, OnInit } from '@angular/core';
import { NavigationEnd, Router } from '@angular/router';
// Layout
import { LayoutConfigService, SplashScreenService, TranslationService } from './core/_base/layout';
// language list
import { locale as ptLang } from './core/_config/i18n/pt';
import { locale as enLang } from './core/_config/i18n/en';

import { Store, select } from '@ngrx/store';
import { AppState } from '../app/core/reducers';
import { currentUser, Logout, User } from '../app/core/auth';
import { AppService } from '../app/core/auth/_services';
import { ParameterService } from './core/auth/_services/parameter.service';
import { finalize, takeUntil, tap } from 'rxjs/operators';
import { IdleService } from './core/_base/layout/services/idle.service';

@Component({
	// tslint:disable-next-line:component-selector
	selector: 'body[kt-root]',
	templateUrl: './app.component.html',
	styleUrls: ['./app.component.scss'],
	changeDetection: ChangeDetectionStrategy.OnPush
})
export class AppComponent implements OnInit, OnDestroy {
	// Public properties
	title = 'TechDriven Digital';
	loader: boolean;

	InactivityTimeLimit: number = 60;
	user$: Observable<User>;
	user: User;

	private unsubscribe: Subscription[] = [];
	/**
	 * Component constructor
	 *
	 * @param translationService: TranslationService
	 * @param router: Router
	 * @param layoutConfigService: LayoutCongifService
	 * @param splashScreenService: SplashScreenService
	 */
	constructor(private store: Store<AppState>,
		private translationService: TranslationService,
		private appService: AppService,
		private authService: AuthService,
		private errorNoticeService: AuthNoticeService,
		private menuConfigService: MenuConfigService,
		private parameterServer: ParameterService,
		private idleService: IdleService,
		private router: Router,
		private layoutConfigService: LayoutConfigService,
		private splashScreenService: SplashScreenService) {


		// register translations
		this.translationService.loadTranslations(ptLang, enLang);
		this.idleService = new IdleService(store,parameterServer,router);
	}

	/**
	 * @ Lifecycle sequences => https://angular.io/guide/lifecycle-hooks
	 */

	/**
	 * On init
	 */
	ngOnInit(): void {
		var self = this;
		// enable/disable loader
		this.loader = this.layoutConfigService.getConfig('loader.enabled');

		this.user$ = this.store.pipe(select(currentUser));

		this.user$.subscribe(u => {
			if (u) {
				this.user = u;
				this.loader = true;
				this.idleService.loadPolicy();
				this.menuConfigService.loadConfigs({ header: u.menu, aside: u.menu });

				this.loader = false;
			}
		});

		const routerSubscription = this.router.events.subscribe(event => {
			if (event instanceof NavigationEnd) {
				// hide splash screen
				this.splashScreenService.hide();

				// scroll to top on every route change
				window.scrollTo(0, 0);

				// to display back the body content
				setTimeout(() => {
					document.body.classList.add('kt-page--loaded');
				}, 500);
			}
		});
		this.unsubscribe.push(routerSubscription);

		this.errorNoticeService.setNotice('');

	}

	@HostListener('click')
    onclick() {
        this.idleService.restartTimer();
    }

	@HostListener('keydown')
    onkeydown() {
        this.idleService.restartTimer();
    }

    @HostListener('keypress')
    onkeypress() {
        this.idleService.restartTimer();
    }

    @HostListener('window:click')
    onClick() {
        this.idleService.restartTimer();
    }

    @HostListener('mouseover')
    mouseover() {
        this.idleService.restartTimer();
    }

    @HostListener('mousemove')
    mousemove() {
        this.idleService.restartTimer();
    }

    @HostListener('scroll')
    mouseScroll() {
        this.idleService.restartTimer();
    }


	/**
	 * On Destroy
	 */
	ngOnDestroy() {
		this.unsubscribe.forEach(sb => sb.unsubscribe());
	}
}

// Angular
import { Component, HostBinding, OnInit, Input } from '@angular/core';
import { NavigationStart, Router } from '@angular/router';
// RxJS
import { filter } from 'rxjs/operators';
// Translate
import { TranslationService } from '../../../../../core/_base/layout';
// AuthService
import { AuthService } from './../../../../../core/auth/_services/auth.service';
//Models
import { Language } from './../../../../../core/auth/_models/language.model';
import { currentUser, User } from './../../../../../core/auth';
import { select, Store } from '@ngrx/store';
import { Observable } from 'rxjs';
import { AppState } from './../../../../../core/reducers';
interface LanguageFlag {
	lang: string;
	name: string;
	flag: string;
	active?: boolean;
}

@Component({
	selector: 'kt-language-selector',
	templateUrl: './language-selector.component.html',
})
export class LanguageSelectorComponent implements OnInit {
	// Public properties
	@HostBinding('class') classes = '';
	@Input() iconType: '' | 'brand';

	user$: Observable<User>;
	user: User;

	language: LanguageFlag;
	languages: LanguageFlag[] = [
		{
			lang: 'pt-BR',
			name: 'Português',
			flag: './assets/media/flags/011-brazil.svg'
		},
		{
			lang: 'en-US',
			name: 'English',
			flag: './assets/media/flags/020-flag.svg'
		},
		// {
		// 	lang: 'es',
		// 	name: 'Spanish',
		// 	flag: './assets/media/flags/016-spain.svg'
		// }
	];

	/**
	 * Component constructor
	 *
	 * @param translationService: TranslationService
	 * @param router: Router
	 */
	constructor(private store: Store<AppState>,
		private translationService: TranslationService,
		private router: Router,
		private auth: AuthService) {
	}

	/**
	 * @ Lifecycle sequences => https://angular.io/guide/lifecycle-hooks
	 */

	/**
	 * On init
	 */
	ngOnInit() {
		this.loadUser();
		this.setSelectedLanguage();
		this.router.events
			.pipe(filter(event => event instanceof NavigationStart))
			.subscribe(event => {
				this.setSelectedLanguage();
			});
	}

	/**
	 * Set language
	 *
	 * @param lang: any
	 */
	setLanguage(lang) {
		this.languages.forEach((language: LanguageFlag) => {
			if (language.lang === lang) {
				language.active = true;
				this.language = language;
			} else {
				language.active = false;
			}
		});
		this.translationService.setLanguage(lang);
	}

	changeServerLanguage(){
		let language = new Language();
		language.language = this.language.lang;

		this.auth.updateLanguage(language)
			.subscribe(res => {

			},
			error => {
			});

	}

	/**
	 * Set selected language
	 */
	setSelectedLanguage(): any {
		this.setLanguage(this.translationService.getSelectedLanguage());
	}

	loadUser(){
		this.user$ = this.store.pipe(select(currentUser));

		this.user$.subscribe(user => {
			this.user = user;
		});
	}
}

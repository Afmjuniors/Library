import { UserReload } from './../../../../../core/auth/_actions/auth.actions';
// Angular
import { Component, Input, OnInit } from '@angular/core';
// RxJS
import { Observable } from 'rxjs';
// NGRX
import { select, Store } from '@ngrx/store';
// Translate
import { TranslateService } from '@ngx-translate/core';
// State
import { AppState } from '../../../../../core/reducers';
import { currentUser, Logout, User, UserRequested } from '../../../../../core/auth';
import { DomSanitizer } from '@angular/platform-browser';
import { MatDialog } from '@angular/material';
import { UserEditComponent } from '../../../../pages/users/user-edit/user-edit.component';
import { UserPhonesComponent } from '../../../../pages/users/user-phones/user-phones.component';
import { AppService } from './../../../../../core/auth/_services';
import { AdGroupUser } from './../../../../../core/auth/_models/adGroupUser.model';

@Component({
	selector: 'kt-user-profile',
	templateUrl: './user-profile.component.html',
	styleUrls: ['user-profile.component.scss']
})
export class UserProfileComponent implements OnInit {
	// Public properties
	user$: Observable<User>;
	user: User;
	imagem: any;
	phones = [];
	adGroupAreas: AdGroupUser[] = [];
	exibeFoto: boolean = false;

	@Input() avatar: boolean = true;
	@Input() greeting: boolean = true;
	@Input() badge: boolean;
	@Input() icon: boolean;

	/**
	 * Component constructor
	 *
	 * @param store: Store<AppState>
	 */
	constructor(
		private store: Store<AppState>,
		public dialog: MatDialog,
		private app: AppService,
		private sanitizer: DomSanitizer,
		private translate: TranslateService,
	) {	}

	/**
	 * @ Lifecycle sequences => https://angular.io/guide/lifecycle-hooks
	 */

	/**
	 * On init
	 */
	ngOnInit(): void {
		this.user$ = this.store.pipe(select(currentUser));

		this.imagem = "./assets/media/misc/User.png";

		this.user$.subscribe(obj => {
			if (obj) {
				this.user = obj;
				if(this.user.phonesNumbers){
					this.phones = this.user.phonesNumbers;
				}

				if(this.user.adGroupsUser){
					this.adGroupAreas = this.user.adGroupsUser;
				}

				if (obj != null && obj.userId > 0) {
					this.imagem = "./assets/media/misc/User.png";

				}
			} else {
				this.store.dispatch(new UserReload());
				this.store.dispatch(new UserRequested());
			}
		});

	}

	/**
	 * Log out
	 */
	logout() {
		this.store.dispatch(new Logout(false));
	}

	viewDetails(){
		const dialogRef = this.dialog.open(UserEditComponent, {width:'80%', data: { user: this.user } });
		dialogRef.afterClosed().subscribe(res => {

		});
	}

	viewDetailsPhones(){
		const dialogRef = this.dialog.open(UserPhonesComponent, {width:'30%', data: { user: this.user } });
		dialogRef.afterClosed().subscribe(res => {
			this.listUserPhones();
		});
	}

	listUserPhones(){
		if(this.user.userId > 0){
			this.phones = [];
			this.app.listPhonesNumbersByUser(this.user.userId).subscribe(res =>{
				for (let index = 0; index < res.length; index++) {
					this.phones.push(res[index].country.prefixNumber + res[index].number);
				}
			})
		}
	}
}

// Angular
import { ModuleWithProviders, NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule, Routes } from '@angular/router';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
// Material
import { MatIconModule, MatButtonModule, MatCheckboxModule, MatFormFieldModule, MatInputModule, MatSelectModule, MatRadioModule, MatProgressSpinnerModule } from '@angular/material';
// Translate
import { TranslateModule } from '@ngx-translate/core';
// NGRX
import { StoreModule } from '@ngrx/store';
import { EffectsModule } from '@ngrx/effects';
// CRUD
import { InterceptService } from '../../../core/_base/crud/';
//Webcam
import {WebcamModule} from 'ngx-webcam';
// Module components
import { AuthComponent } from './auth.component';
import { LoginADComponent } from './loginAD/loginAD.component';

//import { AuthNoticeComponent } from './auth-notice/auth-notice.component';
import { ControleAcessoComponent } from './controle-acesso/controle-acesso.component';
// Auth
import { AuthEffects, AuthGuard, authReducer, AuthService } from '../../../core/auth';
import { PagesModule } from '../pages.module';

const routes: Routes = [
	{
		path: '',
		component: AuthComponent,
		children: [
			{
				path: '',
				redirectTo: 'login',
				pathMatch: 'full'
			},
			{
				path: 'acesso/:exibeOpcaoLogin',
				component: ControleAcessoComponent,
			}
		]
	}
];


@NgModule({
	imports: [
		PagesModule,
		CommonModule,
		FormsModule,
		ReactiveFormsModule,
		MatButtonModule,
		MatRadioModule,
		RouterModule.forChild(routes),
		MatSelectModule,
		MatInputModule,
		MatFormFieldModule,
		MatCheckboxModule,
		MatProgressSpinnerModule,
		MatIconModule,
		TranslateModule.forChild(),
		StoreModule.forFeature('auth', authReducer),
		EffectsModule.forFeature([AuthEffects]),
		WebcamModule
	],
	providers: [
		InterceptService,
		{
			provide: HTTP_INTERCEPTORS,
			useClass: InterceptService,
			multi: true
		},
	],
	exports: [AuthComponent],
	declarations: [
		AuthComponent,
		LoginADComponent,
		ControleAcessoComponent
	]
})

export class AuthModule {
	static forRoot(): ModuleWithProviders {
		return {
			ngModule: AuthModule,
			providers: [
				AuthService,
				AuthGuard
			]
		};
	}
}

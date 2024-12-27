// Angular
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
// Components
import { BaseComponent } from './views/theme/base/base.component';
import { ErrorPageComponent } from './views/theme/content/error-page/error-page.component';
// Auth
import { AuthGuard } from './core/auth';

const routes: Routes = [
	{
		path: 'auth', loadChildren: () => import('./views/pages/auth/auth.module').then(m => m.AuthModule)
	},
	{
		path: '',
		component: BaseComponent,
		canActivate: [AuthGuard],
		children: [
			{
				path: 'inicio',
				canActivate: [AuthGuard],
				loadChildren: () => import('../app/views/pages/inicio/inicio.module').then(m => m.InicioModule)
			},
			{
				path: 'parameters',
				//canActivate: [AuthGuard],
				loadChildren: () => import('./views/pages/parameter/parameter.module').then(m => m.ParameterModule)
			},
			{
				path: 'ad-groups',
				canActivate: [AuthGuard],
				loadChildren: () => import('./views/pages/ad-groups/ad-groups.module').then(m => m.AdGroupsModule)
			},
			{
				path: 'audit-trail',
				canActivate: [AuthGuard],
				loadChildren: () => import('./views/pages/audit-trail/audit-trail.module').then(m => m.AuditTrailModule)
			},
			{
				path: 'users',
				canActivate: [AuthGuard],
				loadChildren: () => import('./views/pages/users/users.module').then(m => m.UsersModule)
			},	
			{
				path: 'checklists',
				canActivate: [AuthGuard],
				loadChildren: () => import('./views/pages/checklists/list-checklists/checklists.module').then(m => m.ChecklistsModule)
			},	
			{
				path: 'error/403',
				component: ErrorPageComponent,
				data: {
					'type': 'error-v6',
					'code': 403,
					'title': 'Acesso não autorizado',
					'desc': 'Você não tem acesso a este recurso.'
				}
			},
			{path: 'error/:type', component: ErrorPageComponent},
			{path: '**', redirectTo: 'inicio', pathMatch: 'full'}
		]
	},

	{path: '**', redirectTo: 'inicio', pathMatch: 'full'},
];

@NgModule({
	imports: [
		RouterModule.forRoot(routes)
	],
	exports: [RouterModule]
})
export class AppRoutingModule {
}

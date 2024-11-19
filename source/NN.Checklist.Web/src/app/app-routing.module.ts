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
				path: 'alarms',
				canActivate: [AuthGuard],
				loadChildren: () => import('./views/pages/alarms/alarms.module').then(m => m.AlarmsModule)
			},
			{
				path: 'events',
				canActivate: [AuthGuard],
				loadChildren: () => import('./views/pages/events/events.module').then(m => m.EventsModule)
			},
			{
				path: 'audit-trail',
				canActivate: [AuthGuard],
				loadChildren: () => import('./views/pages/audit-trail/audit-trail.module').then(m => m.AuditTrailModule)
			},
			{
				path: 'impact-analysis-overview',
				canActivate: [AuthGuard],
				loadChildren: () => import('./views/pages/impact-analysis-overview/impact-analysis-overview.module').then(m => m.ImpactAnalysisOverviewModule)
			},
			{
				path: 'perform-impact-analysis',
				canActivate: [AuthGuard],
				loadChildren: () => import('./views/pages/perform-impact-analysis/perform-impact-analysis.module').then(m => m.PerformImpactAnalysisModule)
			},
			{
				path: 'qa-overview',
				canActivate: [AuthGuard],
				loadChildren: () => import('./views/pages/QA/Overview/qa-overview.module').then(m => m.QAOverviewModule)
			},
			{
				path: 'qa-analysis',
				canActivate: [AuthGuard],
				loadChildren: () => import('./views/pages/QA/Analysis/qa-analysis.module').then(m => m.QAAnalysisModule)
			},
			{
				path: 'qa-report',
				canActivate: [AuthGuard],
				loadChildren: () => import('./views/pages/QA/Report/qa-report.module').then(m => m.QAReportModule)
			},
			{
				path: 'batch',
				canActivate: [AuthGuard],
				loadChildren: () => import('./views/pages/batch/release/batch-release.module').then(m => m.BatchReleaseModule)
			},
			{
				path: 'config',
				canActivate: [AuthGuard],
				loadChildren: () => import('./views/pages/configuration/configuration.module').then(m => m.ConfigurationModule)
			},
			{
				path: 'users',
				canActivate: [AuthGuard],
				loadChildren: () => import('./views/pages/users/users.module').then(m => m.UsersModule)
			},
			{
				path: 'areas',
				canActivate: [AuthGuard],
				loadChildren: () => import('./views/pages/areas/areas.module').then(m => m.AreasModule)
			},
			{
				path: 'diagnostic',
				canActivate: [AuthGuard],
				loadChildren: () => import('./views/pages/diagnostic/diagnostic.module').then(m => m.DiagnosticModule)
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

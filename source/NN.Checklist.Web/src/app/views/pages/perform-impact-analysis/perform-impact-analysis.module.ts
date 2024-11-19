
// Angular
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { MatTableModule } from '@angular/material/table';
import { MatPaginatorModule } from '@angular/material';
// NGRX
import { NgxLoadingModule } from 'ngx-loading';
// Translate
import { TranslateModule } from '@ngx-translate/core';
import { PartialsModule } from '../../partials/partials.module';
// Services
import { HttpUtilsService, TypesUtilsService, InterceptService, LayoutUtilsService} from '../../../core/_base/crud';
// Shared
import { ActionNotificationComponent } from '../../partials/content/crud';
// Components
import { PerformImpactAnalysisComponent } from './perform-impact-analysis.component';
import { DetailsPerformImpactAnalysisComponent } from './details-perform-impact-analysis/details-perform-impact-analysis.component';
// Material
import {
	MatInputModule,
	MatProgressSpinnerModule,
	MatSortModule,
	MatSelectModule,
	MatMenuModule,
	MatProgressBarModule,
	MatButtonModule,
	MatCheckboxModule,
	MatDialogModule,
	MatTabsModule,
	MatNativeDateModule,
	MatCardModule,
	MatRadioModule,
	MatIconModule,
	MatDatepickerModule,
	MatExpansionModule,
	MatAutocompleteModule,
	MAT_DIALOG_DEFAULT_OPTIONS,
	MatSnackBarModule,
	MatTooltipModule,
	MatChipsModule,
	MatTreeModule,
	MatGridListModule,
	MatListModule
} from '@angular/material';
import { CdkTableModule } from '@angular/cdk/table';
import { NgxMatDatetimePickerModule, NgxMatTimepickerModule } from '@angular-material-components/datetime-picker';
import { NgxMatMomentModule } from '@angular-material-components/moment-adapter';
import { ParameterService } from '../../../core/auth/_services/parameter.service';
import { NgxMaskModule, IConfig } from 'ngx-mask'
import { EmailComponent } from '../../components/email/email.component';
import { ComponentsModule } from '../../components/components.module';

const routes: Routes = [
	{
		path: '',
		component: PerformImpactAnalysisComponent,
	}
];

const maskConfig: Partial<IConfig> = {
    validation: false,
  };

@NgModule({
	imports: [
		CommonModule,
		HttpClientModule,
		PartialsModule,
		RouterModule.forChild(routes),
		FormsModule,
		ReactiveFormsModule,
		TranslateModule.forChild(),
		MatButtonModule,
		MatMenuModule,
		MatSelectModule,
        MatInputModule,
		MatTableModule,
		MatPaginatorModule,
		MatAutocompleteModule,
		MatRadioModule,
		MatIconModule,
		MatNativeDateModule,
		MatProgressBarModule,
		MatDatepickerModule,
		MatCardModule,
		MatPaginatorModule,
		MatSortModule,
		MatCheckboxModule,
		MatProgressSpinnerModule,
		MatSnackBarModule,
		MatExpansionModule,
		MatTabsModule,
		MatTooltipModule,
		MatDialogModule,
		MatChipsModule,
		CdkTableModule,
		MatTreeModule,
		NgxLoadingModule,
		MatGridListModule,
		MatListModule,
		NgxMatDatetimePickerModule,
		NgxMaskModule.forRoot(maskConfig),
		NgxMatTimepickerModule,
		NgxMatMomentModule,
		ComponentsModule
	],
	providers: [
		InterceptService,
		{
        	provide: HTTP_INTERCEPTORS,
       	 	useClass: InterceptService,
			multi: true
		},
		{
			provide: MAT_DIALOG_DEFAULT_OPTIONS,
			useValue: {
				hasBackdrop: true,
				panelClass: 'kt-mat-dialog-container__wrapper',
				height: 'auto',
				width: '80%'
			}
		},
		HttpUtilsService,
		TypesUtilsService,
		LayoutUtilsService,
		ParameterService
	],
	entryComponents: [
		ActionNotificationComponent,
		DetailsPerformImpactAnalysisComponent
	],
	declarations: [
		PerformImpactAnalysisComponent,
		DetailsPerformImpactAnalysisComponent
	]
})
export class PerformImpactAnalysisModule {}

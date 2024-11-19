
// Angular
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { MatTableModule } from '@angular/material/table';
import { MatPaginatorModule, MatSlideToggle } from '@angular/material';
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
import { CreateTypeGroupComponent } from './create-type-group/create-type-group.component';
import { EditTypeGroupComponent } from './edit-type-group/edit-type-group.component';
import { RemoveTypeGroupComponent } from './remove-type-group/remove-type-group.component';
import { TypeOccurrenceRecordConfigurationComponent } from './type-occurrence-record-configuration/type-occurrence-record-configuration.component';
import { ConfigurationsComponent } from './configurations/configurations.component';
import { NewTypeOccurrenceRecordComponent } from './new-type-occurrence-record/new-type-occurrence-record.component';
import { NotifyConfigurationComponent } from './notify-configuration/notify-configuration.component';

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
	MatListModule,
	MatSlideToggleModule
} from '@angular/material';
import { CdkTableModule } from '@angular/cdk/table';
import { NgxMatDatetimePickerModule, NgxMatTimepickerModule } from '@angular-material-components/datetime-picker';
import { NgxMatMomentModule } from '@angular-material-components/moment-adapter';
import { ParameterService } from '../../../core/auth/_services/parameter.service';
import { NgxMaskModule, IConfig } from 'ngx-mask'
import { EmailComponent } from '../../components/email/email.component';
import { ComponentsModule } from '../../components/components.module';
import { GroupRegisterComponent } from './groups-register/group-register.component';
import { ConfigService } from '../../../core/auth/_services/config.service';
import { AppService } from '../../../core/auth/_services';

const routes: Routes = [
	{
		path: '',
		component: ConfigurationsComponent,
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
		ComponentsModule,
		MatSlideToggleModule
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
		ParameterService,
		ConfigService,
	],
	entryComponents: [
		ActionNotificationComponent,
		CreateTypeGroupComponent,
		EditTypeGroupComponent,
		RemoveTypeGroupComponent,
		TypeOccurrenceRecordConfigurationComponent,
		ConfigurationsComponent,
		NewTypeOccurrenceRecordComponent,
		NotifyConfigurationComponent
	],
	declarations: [
		GroupRegisterComponent,
		CreateTypeGroupComponent,
		EditTypeGroupComponent,
		RemoveTypeGroupComponent,
		TypeOccurrenceRecordConfigurationComponent,
		ConfigurationsComponent,
		NewTypeOccurrenceRecordComponent,
		NotifyConfigurationComponent
	]
})
export class ConfigurationModule {}

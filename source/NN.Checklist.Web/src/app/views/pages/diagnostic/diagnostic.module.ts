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
import { HttpUtilsService, InterceptService, LayoutUtilsService, TypesUtilsService } from '../../../../../src/app/core/_base/crud';
// Shared
import { ActionNotificationComponent } from '../../partials/content/crud';
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
import { NgxMaskModule, IConfig } from 'ngx-mask'
import { DiagnosticComponent } from '../../../../app/views/pages/diagnostic/diagnostic.component';
import { QAService } from '../../../../../src/app/core/auth/_services/qa.service';
import { ComponentsModule } from '../../components/components.module';
import { SignatureComponent } from '../../components/signature/signature.component';
import { DetailsQAAnalysisComponent } from '../QA/Analysis/details-analysis/details-qa-analysis.component';

const routes: Routes = [
	{
		path: '',
		component: DiagnosticComponent,		
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
  QAService
],
entryComponents: [
  ActionNotificationComponent,
  SignatureComponent,
  DetailsQAAnalysisComponent
],
declarations: [
  DiagnosticComponent
]
})
export class DiagnosticModule { }

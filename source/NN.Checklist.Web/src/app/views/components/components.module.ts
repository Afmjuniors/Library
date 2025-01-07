// Angular
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
// NGRX
import { NgxLoadingModule } from 'ngx-loading';

// Translate
import { TranslateModule } from '@ngx-translate/core';
// Services
import { HttpUtilsService, TypesUtilsService, InterceptService, LayoutUtilsService } from '../../core/_base/crud';
// Shared
import { ActionNotificationComponent } from '../partials/content/crud';

// Material
import { MatAutocompleteModule } from '@angular/material/autocomplete';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatChipsModule } from '@angular/material/chips';
import { MatNativeDateModule } from '@angular/material/core';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatDialogModule, MAT_DIALOG_DEFAULT_OPTIONS } from '@angular/material/dialog';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatMenuModule } from '@angular/material/menu';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatRadioModule } from '@angular/material/radio';
import { MatSelectModule } from '@angular/material/select';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { MatSortModule } from '@angular/material/sort';
import { MatTableModule } from '@angular/material/table';
import { MatTabsModule } from '@angular/material/tabs';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MatTreeModule } from '@angular/material/tree';
import {MatStepperModule} from '@angular/material/stepper';
import {CdkStepperModule} from '@angular/cdk/stepper';
import { SignatureComponent } from './signature/signature.component';
import { EmailComponent } from './email/email.component';
import { SendMailComponent } from './send-mail/send-mail.component';
import { PdfViewComponent } from './pdf-view/pdf-view.component';
import { UserEditComponent } from '../pages/users/user-edit/user-edit.component';
import { UserControlComponent } from '../pages/users/user-control/user-control.component';
import { NgxMatDatetimePickerModule, NgxMatTimepickerModule } from '@angular-material-components/datetime-picker';
import { NgxMatMomentModule } from '@angular-material-components/moment-adapter';
import { MatListModule } from '@angular/material';
import { PdfViewerModule } from 'ng2-pdf-viewer';
import { ViewTextComponent } from './view-text/view-text.component';
import { PhoneMaskPipe } from './phone-mask/phone-mask.pipe';
import { NgxMaskModule, IConfig } from 'ngx-mask'
import { NewChecklistForm } from './new-checklist-form/newChecklistForm.component';
import { ComboboxInputComponent } from './input-components/combobox/combobox-input.component';
import { DatePickerInputComponent } from './input-components/date/datepicker-input.component';
import { NumberInputComponent } from './input-components/number/number-input.component';
import { TextInputComponent } from './input-components/text/text-input.component';
import { CheckboxInputComponent } from './input-components/checkbox/checkbox-input.component';

const maskConfig: Partial<IConfig> = {
    validation: false,
};

@NgModule({
	imports: [
		CommonModule,
		HttpClientModule,
		FormsModule,
		ReactiveFormsModule,
		TranslateModule.forChild(),
		MatButtonModule,
		MatMenuModule,
		MatSelectModule,
		MatInputModule,
		MatTableModule,
		MatAutocompleteModule,
		MatRadioModule,
		MatIconModule,
		MatDatepickerModule,
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
		MatChipsModule,
		MatDialogModule,
		MatTreeModule,
		NgxLoadingModule,
		NgxMatTimepickerModule,
		NgxMatDatetimePickerModule,
		NgxMatMomentModule,
		MatListModule,
		MatStepperModule,
		CdkStepperModule,
		NgxMaskModule.forRoot(maskConfig),
		PdfViewerModule
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
		LayoutUtilsService
	],
	entryComponents: [
		ActionNotificationComponent,
		SignatureComponent,
		EmailComponent,
		SendMailComponent,
		ViewTextComponent,
		PdfViewComponent,
		UserEditComponent,
		NewChecklistForm,
		UserControlComponent,
		ComboboxInputComponent,
		DatePickerInputComponent,
		NumberInputComponent,
		TextInputComponent,
		CheckboxInputComponent


	],
	declarations: [
		SignatureComponent,
		EmailComponent,
		SendMailComponent,
		ViewTextComponent,
		PdfViewComponent,
		UserEditComponent,
		UserControlComponent,
		NewChecklistForm,
		PhoneMaskPipe,
		ComboboxInputComponent,
		DatePickerInputComponent,
		NumberInputComponent,
		TextInputComponent,
		CheckboxInputComponent
	],
	exports:[
		PhoneMaskPipe,
		ComboboxInputComponent
	]
})
export class ComponentsModule { }

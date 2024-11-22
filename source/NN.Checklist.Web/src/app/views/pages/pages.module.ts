// Angular
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
// Partials
import { PartialsModule } from '../partials/partials.module';
// Pages
import { CoreModule } from '../../core/core.module';
import { InicioModule } from './inicio/inicio.module';
import { AuthNoticeComponent} from '../pages/auth/auth-notice/auth-notice.component';
// import { FotoColaboradorReduzidaComponent } from '../components/foto-colaborador-reduzida/foto-colaborador-reduzida.component';

import { NgxMaskModule, IConfig } from 'ngx-mask';

// Material
import {
    MatProgressSpinnerModule,
    MatCardModule,
    MatFormFieldModule,
} from '@angular/material';
//Webcam
import {WebcamModule} from 'ngx-webcam';
//customPipes
// import { TelefoneMaskPipe } from '../components/telefone-mask/telefone-mask.pipe';
// import { MacPipe } from '../components/mac-pipe/mac.pipe';
import { NgxLoadingModule } from 'ngx-loading';
import { NgxMatDatetimePickerModule, NgxMatTimepickerModule } from '@angular-material-components/datetime-picker';
import { NgxMatMomentModule } from '@angular-material-components/moment-adapter';
import { ParameterModule } from './parameter/parameter.module';
import { SignatureComponent } from '../components/signature/signature.component';
import { ComponentsModule } from '../components/components.module';



const maskConfig: Partial<IConfig> = {
    validation: false,
  };


@NgModule({
    declarations: [
        AuthNoticeComponent
    ],
    exports: [
        AuthNoticeComponent,
        MatProgressSpinnerModule,
        WebcamModule,
        MatFormFieldModule,
    ],
    imports: [
        WebcamModule,
        MatProgressSpinnerModule,
        CommonModule,
        HttpClientModule,
        FormsModule,
        CoreModule,
        PartialsModule,
        InicioModule,
        ParameterModule,
        MatCardModule,
        MatFormFieldModule,
        NgxMaskModule.forRoot(maskConfig),
        NgxLoadingModule.forRoot({}),
        NgxMatDatetimePickerModule,
        NgxMatTimepickerModule,
        NgxMatMomentModule,
    ],
    providers: []
})
export class PagesModule {
}

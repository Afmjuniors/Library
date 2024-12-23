
import { Component, Inject, Input, OnInit } from '@angular/core';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';

@Component({
    selector: 'kt-datepicker-input',
    templateUrl: './datepicker-input.component.html',
    styleUrls: ['./datepicker-input.component.scss']
})
export class DatePickerInputComponent implements OnInit {
    @Input() label: string = 'Select Date'; // Rótulo do campo
    @Input() placeholder: string = 'Choose a date'; // Placeholder
    @Input() isDisable: boolean = false; // Desabilitar input
    @Input() showSpinners: boolean = true; // Exibir spinners no picker
    @Input() showSeconds: boolean = false; // Exibir segundos
    @Input() stepHour: number = 1; // Intervalo de horas
    @Input() stepMinute: number = 1; // Intervalo de minutos
    @Input() stepSecond: number = 1; // Intervalo de segundos
    @Input() touchUi: boolean = false; // Modo touch-friendly
    @Input() enableMeridian: boolean = false; // Exibir formato AM/PM
    component: { datePicker: Date | null } = { datePicker: null };


    constructor(

    ) { }

    ngOnInit() {

    }

}


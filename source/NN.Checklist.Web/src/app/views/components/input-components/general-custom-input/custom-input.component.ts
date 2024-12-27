
import { Component, Inject, Input, OnInit, Output } from '@angular/core';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';

@Component({
    selector: 'custom-input',
    templateUrl: './custom-input.component.html',
    styleUrls: ['./custom-input.component.scss']
})
export class DatePickerInputComponent implements OnInit {
    @Input() public label: string = 'Select Date'; // Rótulo do campo
    @Input() public name: string ; // Rótulo do campo
    @Input() public id: string ; // Rótulo do campo

    @Input() public options: Array<{ acronym: string; description: string }> = [];

    @Input() public placeholder: string = 'Choose a date'; // Placeholder
    @Input() public isDisable: boolean = false; // Desabilitar input
    @Input() public showSpinners: boolean = true; // Exibir spinners no picker
    @Input() public showSeconds: boolean = false; // Exibir segundos
    @Input() public stepHour: number = 1; // Intervalo de horas
    @Input() public stepMinute: number = 1; // Intervalo de minutos
    @Input() public stepSecond: number = 1; // Intervalo de segundos
    @Input() public touchUi: boolean = false; // Modo touch-friendly
    @Input() public enableMeridian: boolean = false; // Exibir formato AM/PM

    @Output() public  component: { datePicker: Date | null } = { datePicker: null };


    constructor(

    ) { }

    ngOnInit() {

    }

}


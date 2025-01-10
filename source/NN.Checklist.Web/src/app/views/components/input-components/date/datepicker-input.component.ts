
import { Component, Inject, Input, OnInit, Output } from '@angular/core';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';

@Component({
    selector: 'custom-datepicker-input',
    templateUrl: './datepicker-input.component.html',
    styleUrls: ['./datepicker-input.component.scss']
})
export class DatePickerInputComponent implements OnInit {
    @Input() public label: string = 'Select Date'; // Rótulo do campo
    @Input() public name: string ; // Rótulo do campo
    @Input() public id: string ; // Rótulo do campo

    @Input() public placeholder: string = 'Choose a date'; // Placeholder
    @Input() public isDisable: boolean = false; // Desabilitar input
    @Input() public showSpinners: boolean = true; // Exibir spinners no picker
    @Input() public showSeconds: boolean = false; // Exibir segundos
    @Input() public stepHour: number = 1; // Intervalo de horas
    @Input() public stepMinute: number = 1; // Intervalo de minutos
    @Input() public stepSecond: number = 1; // Intervalo de segundos
    @Input() public touchUi: boolean = false; // Modo touch-friendly
    @Input() public enableMeridian: boolean = false; // Exibir formato AM/PM
    @Input() public initialValue: Date ;

    @Input() public isMandatory: boolean=false;
    @Input() public isKey: boolean= false;

    @Output() public  component: { value: Date | null } = { value: null };


    constructor(

    ) { }

    ngOnInit() {

        if(this.initialValue){
            this.component.value = this.initialValue;
        }
        let newText = this.isMandatory?"*":"";
        this.label = this.label + newText;
    }

}


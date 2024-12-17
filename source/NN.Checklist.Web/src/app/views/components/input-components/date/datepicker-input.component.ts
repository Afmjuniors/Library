
import { Component, Inject, OnInit } from '@angular/core';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';

@Component({
    selector: 'kt-datepicker-input',
    templateUrl: './datepicker-input.component.html',
    styleUrls: ['./datepicker-input.component.scss']
})
export class DatePickerInputComponent implements OnInit {
    label: string = "";
    placeholder:string = "";
    isDisable: boolean = false;


    constructor(
        public dialogRef: MatDialogRef<DatePickerInputComponent>,
        @Inject(MAT_DIALOG_DATA)
        public data: any,
        public dialog: MatDialog
    ) { }

    ngOnInit() {
        if(this.data.label){
            this.label = this.data.label;
        }
        if(this.data.placeholder){
            this.placeholder = this.data.placeholder;
        }
    }

}


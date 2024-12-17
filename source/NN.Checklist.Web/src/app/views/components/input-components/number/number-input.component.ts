
import { Component, Inject, OnInit } from '@angular/core';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';

@Component({
    selector: 'kt-number-input',
    templateUrl: './number-input.component.html',
    styleUrls: ['./number-input.component.scss']
})
export class NumberInputComponent implements OnInit {
    label: string = "";
    placeholder:string = "";
    isDisable: boolean = false;
    mask:string="";
    


    constructor(
        public dialogRef: MatDialogRef<NumberInputComponent>,
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


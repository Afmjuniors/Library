
import { Component, Inject, OnInit } from '@angular/core';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';

@Component({
    selector: 'kt-combobox-input',
    templateUrl: './combobox-input.component.html',
    styleUrls: ['./combobox-input.component.scss']
})
export class ComboboxInputComponent implements OnInit {
    label: string = "";
    options: OptionsClass[];
    isDisable: boolean = false;


    constructor(
        public dialogRef: MatDialogRef<ComboboxInputComponent>,
        @Inject(MAT_DIALOG_DATA)
        public data: any,
        public dialog: MatDialog
    ) { }

    ngOnInit() {
        if(this.data.label){
            this.label = this.data.label;
        }
        if(this.data.options){
            this.options = this.data.options;
        }
    }

}

class OptionsClass{
    fieldChecklistTemplateId:number;
    name:string;

}


import { Component, Inject, Input, OnInit, Output } from '@angular/core';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';

@Component({
    selector: 'custom-number-input',
    templateUrl: './number-input.component.html',
    styleUrls: ['./number-input.component.scss']
})
export class NumberInputComponent implements OnInit {
    @Input() label: string = 'Default Label';
    @Input() placeholder: string = 'Enter text';
    @Input() mask: string | null = null; // Máscara opcional
    @Input() isDisable: boolean = false; // Desabilitar input (default: false)
    @Input() name: string = ''; // Nome do input
    @Input() id: string = ''; // ID do input
    
    @Output() component: { value: number | null } = { value: null }; // Modelo associado ao input



    ngOnInit() {

    }

}


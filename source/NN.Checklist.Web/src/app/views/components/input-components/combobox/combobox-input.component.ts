import { Component, Inject, Input, OnInit } from '@angular/core';

@Component({
    selector: 'kt-combobox-input',
    templateUrl: './combobox-input.component.html',
    styleUrls: ['./combobox-input.component.scss']
})
export class ComboboxInputComponent implements OnInit {
    @Input() label: string = 'Select an option'; // Rótulo do combobox
    @Input() options: Array<{ acronym: string; description: string }> = []; // Lista de opções
    @Input() isDisable: boolean = false; // Habilitar/desabilitar combobox
    selectedOption: string | null = null; // Valor selecionado no combobox

    constructor(

    ) {}

    ngOnInit() {

    }
}

export class OptionsClass {
    fieldChecklistTemplateId: number;
    name: string;
}

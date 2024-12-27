import { Component, Inject, Input, OnInit, Output } from '@angular/core';

@Component({
    selector: 'custom-combobox-input',
    templateUrl: './combobox-input.component.html',
    styleUrls: ['./combobox-input.component.scss']
})
export class ComboboxInputComponent implements OnInit {
    @Input() public label: string = 'Select an option'; // Rótulo do combobox

    @Input() public name: string ;
    @Input() public id: string ;

    @Input() public isDisable: boolean = false; // Habilitar/desabilitar combobox
    @Input() public options: Array<{ acronym: string; description: string }> = [];
    
    // Lista de opções
    @Output() public value: string | null = null; // Valor selecionado no combobox



    ngOnInit() {

    }
}


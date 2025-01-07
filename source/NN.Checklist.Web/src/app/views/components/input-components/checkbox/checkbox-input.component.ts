import { Component, EventEmitter, Inject, Input, OnInit, Output } from '@angular/core';
import { OptionFieldVersionChecklistTemplate } from '../../../../core/auth/_models/optionFieldVersionChecklistTemplate.model';
import { OptionFieldVersionChecklist } from '../../../../core/auth/_models/optionFieldVersionChecklist.model';
import { OptionItemVersionChecklistTemplate } from '../../../../core/auth/_models/optionItemVersionChecklistTemplate.model';

@Component({
    selector: 'custom-checkbox-input',
    templateUrl: './checkbox-input.component.html',
    styleUrls: ['./checkbox-input.component.scss']
})
export class CheckboxInputComponent implements OnInit {

    @Input() public isSingle: boolean = false ;
    @Input() public isDisable: boolean = false; // Habilitar/desabilitar combobox
    @Input() public optionsField: OptionFieldVersionChecklistTemplate[] | OptionItemVersionChecklistTemplate[] = [];
    
    @Output() valueChange = new EventEmitter<any[]>();
    


    onCheckboxChange(selectedOption: any, index: number): void {
        if (this.isSingle) {
          this.optionsField.forEach((option, i) => {
            option.checked = i === index; // Apenas o selecionado permanece marcado
          });
        }
        this.valueChange.emit(this.optionsField);
      }

    ngOnInit() {

    }

    


}


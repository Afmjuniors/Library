import { Component, Inject, Input, OnInit, Output } from '@angular/core';

@Component({
  selector: 'custom-text-input',
  templateUrl: './text-input.component.html',
  styleUrls: ['./text-input.component.scss']
})
export class TextInputComponent implements OnInit {
   @Input() public placeholder: string;
   @Input() public mask: string = '';
   @Input() public isDisable: boolean;
   @Input() public isMandatory: boolean=false;
   @Input() public isKey: boolean= false;

   @Input() public name: string = '';
   @Input() public id: string;
   @Input() public label: string;
   @Input() public initialValue: string = '';

    @Input() public error = false;
    @Input() public errorRegex = false;
  
    @Input() public formControlName: string;

   @Output() component: { value: string , formControlName:string} = { value: this.initialValue , formControlName:this.name }; 





  ngOnInit() {
    this.component.value = this.initialValue;
    let newText = this.isMandatory?"*":"";
    this.label = this.label + newText;
  }

}

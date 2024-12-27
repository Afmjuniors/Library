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
   @Input() public name: string;
   @Input() public id: string;
   @Input() public label: string;
   @Input() public initialValue: string = '';


   @Output() component: { value: string } = { value: this.initialValue }; 





  ngOnInit() {
  }

}

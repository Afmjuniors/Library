import { Component, Inject, Input, OnInit } from '@angular/core';

@Component({
  selector: 'kt-text-input',
  templateUrl: './text-input.component.html',
  styleUrls: ['./text-input.component.scss']
})
export class TextInputComponent implements OnInit {
  @Input() component: TextInputComponent;
  placeholder: string;
  mask: string = '';
  isDisable: boolean;
  name: string;
  id: string;



  ngOnInit() {
console.log(this.component);
     this.placeholder = this.component.placeholder;
     this.mask = this.component.mask;
     this.isDisable = this.component.isDisable;
     this.id = this.component.id;


  }
}

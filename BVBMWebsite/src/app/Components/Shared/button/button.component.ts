import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'app-button',
  templateUrl: './button.component.html',
  styleUrls: ['./button.component.css']
})

export class ButtonComponent {
  @Input() buttonText!:string
  @Input() backgroundColor!: string;

  @Input()
  set text(name: string) {
    this.buttonText = name.charAt(0).toUpperCase() + name.slice(1).toLowerCase();
  }
  get name(): string {
    return this.buttonText;
  }

  @Output() btnClick = new EventEmitter();

  constructor() { }

  onClick() {
    this.btnClick.emit();
  }

  ngOnInit()  {

  }
}

import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'app-button',
  standalone: true,
  imports: [],
  templateUrl: './button.component.html',
  styleUrl: './button.component.scss',
})
export class ButtonComponent {
  @Input() type!: string;
  @Input() disabled: boolean = false;
  @Input() title!: string;
  @Input() className!: string;
  @Input() label!: string;
  @Input() iconClass!: string;
  @Input() leftIcon: boolean = true;
  @Output() btnClick: EventEmitter<any> = new EventEmitter<any>();
  name: string = 'clicked';

  constructor() {}

  onClick() {
    this.btnClick.emit(this.name);
  }
}

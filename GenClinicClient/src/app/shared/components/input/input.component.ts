import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Input, Output } from '@angular/core';
import { FormGroup, ReactiveFormsModule } from '@angular/forms';
import { ControlErrorsDirective } from '../../../directives/control-errors.directive';

@Component({
  selector: 'app-input',
  standalone: true,
  imports: [ReactiveFormsModule, CommonModule, ControlErrorsDirective],
  templateUrl: './input.component.html',
  styleUrl: './input.component.scss',
})
export class InputComponent {
  @Input() parentForm!: FormGroup;
  @Input() type!: string;
  @Input() label!: string;
  @Input() labelClass!: string;
  @Input() placeholder!: string;
  @Input() controlName!: string;
  @Input() value!: string;
  @Input() required!: boolean;
  @Input() pattern!: string;
  @Input() readonly!: boolean;
  @Input() className!: string;
  @Input() maxlength!: number;
  @Input() minlength!: number;
  @Input() name!: string;
  @Input() ngClass!: any;
  @Input() styles!: any;
  @Input() errorTitle: string = this.label;
  @Input() customErrors: Record<string, string> = {};
  @Output() onKeyup: EventEmitter<string> = new EventEmitter<string>();

  onKeyUp(inputValue: string) {
    this.onKeyup.emit(inputValue);
  }
}

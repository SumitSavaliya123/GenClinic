// import { CommonModule } from '@angular/common';
// import { Component, Input } from '@angular/core';
// import { FormControl, FormGroup, ReactiveFormsModule } from '@angular/forms';
// import { DropdownItem } from '../../../models/drop-down-items';

// @Component({
//   selector: 'app-dropdown',
//   standalone: true,
//   imports: [CommonModule, ReactiveFormsModule],
//   templateUrl: './dropdown.component.html',
//   styleUrl: './dropdown.component.scss',
// })
// export class DropdownComponent {
//   @Input() showLabel: boolean = true;
//   @Input() label!: string;
//   @Input() labelClass!: string;
//   @Input() className!: string;
//   @Input() selectValue!: string;
//   @Input() controlName!: string;
//   @Input() parentForm!: FormGroup;
//   @Input('options') options!: DropdownItem[];
//   formControl: FormControl = new FormControl('');
// }

import { Component, EventEmitter, Input, Output } from '@angular/core';
import { FormGroup, ReactiveFormsModule } from '@angular/forms';
import { DropdownModule } from 'primeng/dropdown';
import { ControlErrorsDirective } from '../../../directives/control-errors.directive';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-dropdown',
  standalone: true,
  imports: [
    DropdownModule,
    ReactiveFormsModule,
    ControlErrorsDirective,
    CommonModule,
  ],
  templateUrl: './dropdown.component.html',
  styleUrl: './dropdown.component.scss',
})
export class SelectComponent {
  @Input() label!: string;
  @Input() labelClass: string = '';
  @Input() options: any[] = [];
  @Input() placeholder: string = '';
  @Input() className: string = '';
  @Input() errorTitle: string = this.label;
  @Input() value: string = '';
  @Input() parentForm!: FormGroup;
  @Input() controlName!: string;
  @Input({ required: false }) testId = '';
  @Input() required: boolean = false;

  ngOnInit() {
    if (this.parentForm && this.controlName) {
      const control = this.parentForm.get(this.controlName);
      if (control) {
        control.setValue(this.value);
      } else {
        console.error(
          `Control with name '${this.controlName}' not found in parent form`
        );
      }
    }
  }
}

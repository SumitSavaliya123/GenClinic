import { Component } from '@angular/core';
import {
  FormControl,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { FormSubmitDirective } from '../../directives/form-submit.directive';
import { InputComponent } from '../../shared/components/input/input.component';
import { ValidationPattern } from '../../validation/validation-pattern';
import { ButtonComponent } from '../../shared/components/button/button.component';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [
    ReactiveFormsModule,
    FormSubmitDirective,
    InputComponent,
    ButtonComponent,
  ],
  templateUrl: './register.component.html',
  styleUrl: './register.component.scss',
})
export class RegisterComponent {
  registerForm: FormGroup = new FormGroup({
    firstName: new FormControl('', Validators.required),
    lastName: new FormControl('', Validators.required),
    phoneNumber: new FormControl('', Validators.required),
    email: new FormControl('', [
      Validators.required,
      Validators.pattern(ValidationPattern.email),
    ]),
    password: new FormControl('', [
      Validators.required,
      Validators.pattern(ValidationPattern.password),
    ]),
    dateOfBirth: new FormControl(null, Validators.required),
  });

  onSubmit() {
    if (this.registerForm.valid) {
      console.log(this.registerForm);
    }
    this.registerForm.markAllAsTouched();
  }

  navigateBack() {
    history.back();
  }
}

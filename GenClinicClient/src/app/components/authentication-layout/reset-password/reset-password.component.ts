import { Component, Injector } from '@angular/core';
import { FormSubmitDirective } from '../../../directives/form-submit.directive';
import { InputComponent } from '../../../shared/components/input/input.component';
import { ButtonComponent } from '../../../shared/components/button/button.component';
import {
  FormControl,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { ValidationPattern } from '../../../validation/validation-pattern';
import { Router } from '@angular/router';
import { ResetPasswordService } from '../../../../services/authentication/reset-password.service';
import { RoutingPathConstant } from '../../../constants/routing-path';
import { IResetPassword } from '../../../models/authentication/reset-password.interface';
import { PasswordMatchValidator } from '../../../common/validators/compare-password-validator';

@Component({
  selector: 'app-reset-password',
  standalone: true,
  imports: [
    FormSubmitDirective,
    InputComponent,
    ButtonComponent,
    ReactiveFormsModule,
  ],
  templateUrl: './reset-password.component.html',
  styleUrl: './reset-password.component.scss',
})
export class ResetPasswordComponent {
  resetPasswordForm = new FormGroup({
    password: new FormControl('', [
      Validators.required,
      Validators.pattern(ValidationPattern.password),
    ]),
    confirmPassword: new FormControl(
      '',
      [Validators.required],
      [PasswordMatchValidator('password')]
    ),
  });

  constructor(
    private resetPasswordService: ResetPasswordService,
    private router: Router
  ) {}

  onSubmit() {
    this.resetPasswordForm.markAllAsTouched();
    if (this.resetPasswordForm.valid) {
      this.resetPasswordService
        .resetPassword(<IResetPassword>this.resetPasswordForm.value)
        .subscribe({
          next: (res) => {
            this.router.navigate([RoutingPathConstant.loginUrl]);
          },
        });
    }
  }
}

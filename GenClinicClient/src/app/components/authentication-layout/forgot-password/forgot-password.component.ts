import { Component, OnInit } from '@angular/core';
import { InputComponent } from '../../../shared/components/input/input.component';
import { ButtonComponent } from '../../../shared/components/button/button.component';
import { FormSubmitDirective } from '../../../directives/form-submit.directive';
import {
  FormControl,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { ValidationPattern } from '../../../validation/validation-pattern';
import { Router, RouterLink } from '@angular/router';
import { RoutingPathConstant } from '../../../constants/routing-path';
import { ForgotPasswordService } from '../../../../services/authentication/forgot-password.service';

@Component({
  selector: 'app-forgot-password',
  standalone: true,
  imports: [
    InputComponent,
    ButtonComponent,
    FormSubmitDirective,
    ReactiveFormsModule,
    RouterLink,
  ],
  templateUrl: './forgot-password.component.html',
  styleUrl: './forgot-password.component.scss',
})
export class ForgotPasswordComponent implements OnInit {
  loginUrl: string = RoutingPathConstant.loginUrl;

  forgotPasswordForm = new FormGroup({
    email: new FormControl(
      '',
      Validators.compose([
        Validators.required,
        Validators.pattern(ValidationPattern.email),
      ])
    ),
  });

  constructor(
    private forgotPasswordService: ForgotPasswordService,
    private router: Router
  ) {}

  ngOnInit() {
    this.router.events.subscribe(() => {});
  }

  onSubmit() {
    this.forgotPasswordForm.markAllAsTouched();
    if (this.forgotPasswordForm.valid) {
      this.forgotPasswordService
        .forgotPassword(<string>this.forgotPasswordForm.value.email)
        .subscribe();
    }
  }
}

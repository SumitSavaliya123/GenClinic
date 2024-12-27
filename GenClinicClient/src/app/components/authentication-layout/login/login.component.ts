import { Component, OnInit } from '@angular/core';
import { InputComponent } from '../../../shared/components/input/input.component';
import { ButtonComponent } from '../../../shared/components/button/button.component';
import {
  FormControl,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { ValidationPattern } from '../../../validation/validation-pattern';
import { Router, RouterLink } from '@angular/router';
import { FormSubmitDirective } from '../../../directives/form-submit.directive';
import { LoginService } from '../../../../services/authentication/login.service';
import { ILogin } from '../../../models/authentication/login.interface';
import { RoutingPathConstant } from '../../../constants/routing-path';
import { StorageHelperService } from '../../../shared/components/services/storage-helper.service';
import { StorageHelperConstant } from '../../../constants/storage-helper';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [
    InputComponent,
    ButtonComponent,
    ReactiveFormsModule,
    FormSubmitDirective,
    RouterLink,
  ],
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss',
})
export class LoginComponent {
  loginForm = new FormGroup({
    email: new FormControl(
      '',
      Validators.compose([
        Validators.required,
        Validators.pattern(ValidationPattern.email),
      ])
    ),
    password: new FormControl(
      '',
      Validators.compose([
        Validators.required,
        Validators.pattern(ValidationPattern.password),
      ])
    ),
    rememberMe: new FormControl(false),
  });

  constructor(
    private loginService: LoginService,
    private router: Router,
    private storageHelper: StorageHelperService
  ) {}

  rememberMeClick(checkbox: any) {
    this.loginForm.value.rememberMe = checkbox.target.checked;
  }

  onSubmit() {
    this.loginForm.markAllAsTouched();
    if (this.loginForm.valid) {
      this.storageHelper.setAsSession(
        StorageHelperConstant.email,
        this.loginForm.value.email || ''
      );
      this.loginService.login(<ILogin>this.loginForm.value).subscribe({
        next: (res: any) => {
          this.router.navigate([RoutingPathConstant.verifyOtpUrl]);
          this.storageHelper.setAsSession(
            StorageHelperConstant.userName,
            res.data
          );
        },
      });
    }
  }
}

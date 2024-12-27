import { Component } from '@angular/core';
import { InputComponent } from '../../../shared/components/input/input.component';
import { ButtonComponent } from '../../../shared/components/button/button.component';
import {
  FormControl,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { Router } from '@angular/router';
import { StorageHelperConstant } from '../../../constants/storage-helper';
import { StorageHelperService } from '../../../shared/components/services/storage-helper.service';
import { VerifyOtpService } from '../../../../services/authentication/verify-otp.service';
import { AuthService } from '../../../shared/components/services/auth.service';
import { RoutingPathConstant } from '../../../constants/routing-path';

@Component({
  selector: 'app-verify-otp',
  standalone: true,
  imports: [InputComponent, ButtonComponent, ReactiveFormsModule],
  templateUrl: './verify-otp.component.html',
  styleUrl: './verify-otp.component.scss',
})
export class VerifyOtpComponent {
  userName: string | null = '';

  constructor(
    private verifyOtpservice: VerifyOtpService,
    private router: Router,
    private storageHelper: StorageHelperService,
    private authService: AuthService
  ) {}

  ngOnInit() {
    this.userName = this.storageHelper.getFromSession(
      StorageHelperConstant.userName
    );
  }

  verifyOtpForm = new FormGroup({
    otp: new FormControl(
      '',
      Validators.compose([
        Validators.required,
        Validators.minLength(6),
        Validators.maxLength(6),
      ])
    ),
  });

  onSubmit() {
    this.verifyOtpForm.markAllAsTouched();
    if (this.verifyOtpForm.valid) {
      this.verifyOtpservice
        .verifyOtp(<{ otp: string | undefined | null }>this.verifyOtpForm.value)
        .subscribe({
          next: (res: any) => {
            this.authService.decodeToken(res.data);
            this.router.navigate([RoutingPathConstant.doctorDashboardUrl]);
          },
        });
    }
  }

  resendOtp() {
    this.verifyOtpservice.resendOtp().subscribe();
  }
}

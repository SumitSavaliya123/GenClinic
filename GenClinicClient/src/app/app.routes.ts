import { Routes } from '@angular/router';
import { NoPageComponent } from './components/no-page/no-page.component';
import { AuthenticationLayoutComponent } from './components/authentication-layout/authentication-layout.component';
import { LoginComponent } from './components/authentication-layout/login/login.component';
import { ForgotPasswordComponent } from './components/authentication-layout/forgot-password/forgot-password.component';
import { ResetPasswordComponent } from './components/authentication-layout/reset-password/reset-password.component';
import { VerifyOtpComponent } from './components/authentication-layout/verify-otp/verify-otp.component';
import { DoctorDashboardComponent } from './components/doctor/doctor-dashboard/doctor-dashboard.component';

export const routes: Routes = [
  {
    path: '',
    component: AuthenticationLayoutComponent, // Use AuthenticationLayout as a wrapper
    children: [
      { path: '', component: LoginComponent },
      { path: 'login', component: LoginComponent },
      { path: 'forgot-password', component: ForgotPasswordComponent },
      { path: 'reset-password', component: ResetPasswordComponent },
      { path: 'verify-otp', component: VerifyOtpComponent },
    ],
  },
  {
    path: 'doctor',
    children: [{ path: 'dashboard', component: DoctorDashboardComponent }],
  },
  { path: '**', component: NoPageComponent },
];

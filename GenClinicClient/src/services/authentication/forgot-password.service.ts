import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ApiCallConstants } from '../../app/constants/apis';

@Injectable({
  providedIn: 'root',
})
export class ForgotPasswordService {
  forgotPasswordApi = ApiCallConstants.FORGOT_PASSWORD_URL;

  constructor(private http: HttpClient) {}

  forgotPassword(email: string) {
    let body = {
      email: email,
    };
    return this.http.post(this.forgotPasswordApi, body);
  }
}

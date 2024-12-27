import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';
import { ApiCallConstants } from '../../app/constants/apis';
import { IResetPassword } from '../../app/models/authentication/reset-password.interface';

@Injectable({
  providedIn: 'root',
})
export class ResetPasswordService {
  resetPasswordApi = ApiCallConstants.RESET_PASSWORD_URL;

  constructor(
    private http: HttpClient,
    private activatedRoute: ActivatedRoute
  ) {}

  resetPassword(resetPasswordDetails: IResetPassword) {
    let token;
    this.activatedRoute.queryParams.forEach((params: Params) => {
      token = params['token'];
    });
    return this.http.post(
      this.resetPasswordApi + '?token=' + token,
      resetPasswordDetails
    );
  }
}

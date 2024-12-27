import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ApiCallConstants } from '../../app/constants/apis';
import { StorageHelperService } from '../../app/shared/components/services/storage-helper.service';
import { StorageHelperConstant } from '../../app/constants/storage-helper';

@Injectable({
  providedIn: 'root',
})
export class VerifyOtpService {
  verifyOtpApi = ApiCallConstants.VERIFY_OTP_URL;
  resendOtpApi = ApiCallConstants.RESEND_OTP_URL;

  constructor(
    private http: HttpClient,
    private storageHelperservice: StorageHelperService
  ) {}

  verifyOtp(otp: { otp: string | null | undefined }) {
    debugger;
    let body = {
      email: this.storageHelperservice.getFromSession(
        StorageHelperConstant.email
      ),
      otp: otp.otp,
    };
    return this.http.post(this.verifyOtpApi, body, {
      withCredentials: true,
      headers: {
        credentials: 'include',
      },
    });
  }

  resendOtp() {
    debugger;
    let body = {
      email: this.storageHelperservice.getFromSession(
        StorageHelperConstant.email
      ),
    };
    return this.http.post(this.resendOtpApi, body);
  }
}

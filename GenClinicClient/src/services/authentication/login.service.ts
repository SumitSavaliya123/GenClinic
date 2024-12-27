import { Injectable } from '@angular/core';
import { ApiCallConstants } from '../../app/constants/apis';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { ILogin } from '../../app/models/authentication/login.interface';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class LoginService {
  loginApi = ApiCallConstants.LOGIN_URL;

  constructor(private http: HttpClient, private router: Router) {}

  login(loginCredentials: ILogin): Observable<ILogin> {
    return this.http.post<ILogin>(this.loginApi, loginCredentials, {
      withCredentials: true,
      headers: {
        credentials: 'include',
      },
    });
  }
}

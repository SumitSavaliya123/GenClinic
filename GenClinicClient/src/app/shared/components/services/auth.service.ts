import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';
import { StorageHelperService } from './storage-helper.service';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  constructor(
    private router: Router,
    private jwtService: JwtHelperService,
    private http: HttpClient,
    private storageHelper: StorageHelperService
  ) {}
  decodeToken(token: string) {
    const decodedToken = this.jwtService.decodeToken(token);
    const userId = decodedToken['UserId'];
    const userRole =
      decodedToken[
        'http://schemas.xmlsoap.org/ws/2005/05/identity/claims/role'
      ];
    const userName =
      decodedToken[
        'http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name'
      ];
    const userEmail =
      decodedToken[
        'http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress'
      ];
    this.storageHelper.setAsLocal('authToken', token);
    this.storageHelper.setAsLocal('userRole', userRole);
    this.storageHelper.setAsLocal('userId', userId);
    this.storageHelper.setAsLocal('userName', userName);
    this.storageHelper.setAsLocal('userEmail', userEmail);
  }

  isJwtTokenExpire() {
    return this.jwtService.isTokenExpired(this.getJwtToken());
  }

  isAuthenticate() {
    if (this.getJwtToken() === null || this.getJwtToken() === '') {
      return false;
    } else {
      return true;
    }
  }

  getJwtToken() {
    return this.storageHelper.getFromLocal('authToken');
  }

  getUserRole() {
    return this.storageHelper.getFromLocal('userRole');
  }

  getUserId() {
    return this.storageHelper.getFromLocal('userId');
  }

  getUserName() {
    return this.storageHelper.getFromLocal('userName');
  }

  getUserEmail() {
    return this.storageHelper.getFromLocal('userEmail');
  }

  logOut() {
    this.storageHelper.removeFromLocal('authToken');
    this.storageHelper.removeFromLocal('userRole');
    this.storageHelper.removeFromLocal('userId');
    this.storageHelper.removeFromLocal('userName');
    this.storageHelper.removeFromLocal('userEmail');
    this.router.navigate(['/']);
  }
}

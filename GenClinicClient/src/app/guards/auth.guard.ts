import { Injectable, inject } from '@angular/core';
import {
  Router,
  CanActivate,
  ActivatedRouteSnapshot,
  RouterStateSnapshot,
} from '@angular/router';
import { AuthService } from '../shared/components/services/auth.service';
import { MessageService } from '../shared/components/services/message.service';

@Injectable({
  providedIn: 'root',
})
export class AuthGuard implements CanActivate {
  constructor(
    private router: Router,
    private authService: AuthService,
    private messageService: MessageService
  ) {}

  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): boolean {
    const isAuthenticated = this.authService.isAuthenticate();
    const userRole = this.authService.getUserRole();

    if (!isAuthenticated) {
      this.messageService.error('Login First');
      this.router.navigate(['/']);
      return false;
    }
    return true;
  }
}

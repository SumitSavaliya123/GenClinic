import { inject } from '@angular/core';
import { Router, CanActivateFn } from '@angular/router';
import { AuthService } from '../shared/components/services/auth.service';

export function SiginGuard(): CanActivateFn {
  return () => {
    const authService = inject(AuthService);
    const router = inject(Router);

    const isAuthenticated = authService.isAuthenticate();
    if (isAuthenticated) {
      if (authService.getUserRole() == '1') {
        router.navigate(['/doctor/dashboard']);
      } else if (authService.getUserRole() == '2') {
        router.navigate(['/patient/dashboard']);
      } else {
        router.navigate(['/lab/dashboard']);
      }
      return false;
    }
    return true;
  };
}

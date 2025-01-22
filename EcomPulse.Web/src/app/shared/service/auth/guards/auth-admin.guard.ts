import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { AuthService } from '../auth.service';

export const authAdminGuard: CanActivateFn = (route, state) => {
  const authService = inject(AuthService);
  const router = inject(Router);
  if (authService.isAuthenticated()) {
    if (authService.isAdmin()) {
      return true;
    } else {
      router.navigate(['admin/login']);
    }
  } else {
    router.navigate(['admin/login']);
  }
  return false;
};

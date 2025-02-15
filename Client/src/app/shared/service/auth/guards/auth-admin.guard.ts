import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { AuthService } from '../auth.service';

export const authAdminGuard: CanActivateFn = (route, state) => {
  const authService = inject(AuthService);
  const router = inject(Router);

  if (authService.isAuthenticated()) {
    if (authService.isAdmin()) {
      if (state.url === '/admin/login') {
        router.navigate(['/admin']);
      }
      return true;
    } else {
      if (typeof window !== 'undefined') {
        window.alert('Yetkisiz giriş!');
      }
      router.navigate(['/admin/login']);
      return false;
    }
  } else {
    if (typeof window !== 'undefined') {
      window.alert('Yetkisiz giriş!');
    }
    router.navigate(['/admin/login']);
    return false;
  }
};

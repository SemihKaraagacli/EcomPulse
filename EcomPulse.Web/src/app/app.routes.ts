import { Routes } from '@angular/router';
import { authGuard } from './shared/service/auth/guards/auth.guard';

export const routes: Routes = [
  // User routes
  {
    path: '',
    loadChildren: () => import('./user/user.module').then((m) => m.UserModule),
  },
  // Admin routes
  {
    path: 'admin',
    loadChildren: () =>
      import('./admin/admin.module').then((m) => m.AdminModule),
    data: { breadcrumb: { label: 'Admin', disable: true } },
    canActivate: [authGuard],
  },
];

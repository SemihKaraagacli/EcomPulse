import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { AuthService } from '../auth.service';
import { ClientcredentialService } from '../clientcredential.service';

export const tokenInterceptor: HttpInterceptorFn = (req, next) => {
  const authService = inject(AuthService);
  const clientCredential = inject(ClientcredentialService);
  let token = authService.getToken();

  if (req.url.includes('/Auth')) {
    token = clientCredential.getToken();
    console.log('Token:', token);
  }
  if (req.method === 'POST' && req.url.includes('/User')) {
    token = clientCredential.getToken();
  }
  if (req.method === 'GET' && req.url.includes('/Product')) {
    token = clientCredential.getToken();
  }
  if (req.method === 'GET' && req.url.includes('/Category')) {
    token = clientCredential.getToken();
  }
  if (req.method === 'GET' && req.url.includes('/Category/:id')) {
    token = clientCredential.getToken();
  }
  if (token) {
    const reqClone = req.clone({
      setHeaders: {
        'Content-Type': 'application/json',
        Authorization: `Bearer ${token}`,
      },
    });
    return next(reqClone);
  }
  return next(req);
};

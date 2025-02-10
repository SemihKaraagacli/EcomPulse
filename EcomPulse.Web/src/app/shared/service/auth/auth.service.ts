import { jwt_decode } from 'jwt-decode-es';
import { Injectable } from '@angular/core';
import { environment } from '../../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { SignInViewModel } from '../../model/viewmodels/auth/SignInViewModel';
import { Router } from '@angular/router';
import { CookieService } from 'ngx-cookie-service';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private url: string = environment.authBaseUrl;
  model: SignInViewModel = new SignInViewModel('', '');
  errorMessage: string = '';
  userClaims: any = {};
  constructor(
    private http: HttpClient,
    private router: Router,
    private cookieService: CookieService
  ) {}

  signIn(data: SignInViewModel) {
    this.http.post(this.url, data).subscribe({
      next: (res: any) => {
        const token = res.accessToken;
        const decodedToken: any = jwt_decode(token);
        this.userClaims = {
          id: decodedToken.id,
          username: decodedToken.username,
          role: decodedToken.role,
        };
        this.saveTokenAndClaimsCookie(token, this.userClaims);

        const currentRoute = this.router.url;

        if (currentRoute.includes('/admin/login')) {
          if (this.isAdmin()) {
            this.router.navigate(['/admin']).then(() => {
              window.location.reload();
            });
          } else {
            window.alert('Yetkisiz giriş!');
            this.router.navigate(['/admin/login']);
          }
        } else {
          this.router.navigate(['/']).then(() => {
            window.location.reload();
          });
        }
      },
      error: (err) => {
        console.log('SignIn failed.', err);
        this.errorMessage =
          err.error?.detail || 'An error occurred during sign-in.';
      },
    });
  }

  saveTokenAndClaimsCookie(token: string, claims: any): void {
    const cookieData = {
      token: token,
      claims: claims,
    };

    this.cookieService.set('authCookie', JSON.stringify(cookieData), {
      expires: 30,
      path: '/',
    });
  }

  getToken(): string | null {
    const cookieData = this.cookieService.get('authCookie');

    if (cookieData) {
      const parsedData = JSON.parse(cookieData);
      return parsedData.token;
    }

    return null;
  }
  getclaims(): { id: string; username: string; role: string } | null {
    const cookieData = this.cookieService.get('authCookie');

    if (cookieData) {
      const parsedData = JSON.parse(cookieData);
      return parsedData.claims;
    }

    return null;
  }
  isAuthenticated(): boolean {
    return !!this.cookieService.get('authCookie');
  }
  isAdmin(): boolean {
    const claims = this.getclaims();
    return claims?.role ? claims.role.includes('admin') : false;
  }
  logout(): void {
    this.cookieService.delete('authCookie', '/');
  }
}

import { jwt_decode } from 'jwt-decode-es';
import { Injectable } from '@angular/core';
import { environment } from '../../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { SignInViewModel } from '../../model/viewmodels/auth/SignInViewModel';
import { Router } from '@angular/router';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { CookieService } from 'ngx-cookie-service';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private url: string = environment.authBaseUrl;
  form: FormGroup;
  model: SignInViewModel = new SignInViewModel('', '');
  errorMessage: string = '';

  constructor(
    private http: HttpClient,
    private router: Router,
    private cookieService: CookieService
  ) {
    this.form = new FormGroup({
      email: new FormControl('', [Validators.required, Validators.email]),
      password: new FormControl('', [
        Validators.required,
        Validators.minLength(5),
      ]),
    });
  }

  signIn(data: SignInViewModel) {
    this.http.post(this.url, data).subscribe({
      next: (res: any) => {
        const token = res.accessToken;
        console.log('Token:', token);
        const decodedToken: any = jwt_decode(token);
        console.log('Token_doceded:', decodedToken);
        const userClaims = {
          id: decodedToken.id,
          username: decodedToken.username,
          role: decodedToken.role,
        };
        this.saveTokenAndClaimsCookie(token, userClaims);
        console.log('GetToken:', this.getToken());
        console.log('GetClaims:', this.getclaims());
        this.router.navigate(['/']);
      },
      error: (err) => {
        console.log('SignIn failed.', err);
        this.errorMessage = err.error.detail;
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
  getclaims(): string | null {
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
  logout(): void {
    this.cookieService.delete('authCookie');
  }
}

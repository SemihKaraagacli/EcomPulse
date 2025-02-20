import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { CookieService } from 'ngx-cookie-service';
import { environment } from '../../../../environments/environment';
import { jwt_decode } from 'jwt-decode-es';

@Injectable({
  providedIn: 'root',
})
export class ClientcredentialService {
  private url: string = environment.authBaseUrl;
  private clientId: string = environment.ClientId;
  private clientSecretKey: string = environment.ClientSecretKey;
  private tokenExpirationTimer: any;

  constructor(
    private http: HttpClient,
    private router: Router,
    private cookieService: CookieService
  ) {}

  initializeToken() {
    const token = this.getToken();

    const now = new Date().getTime();

    if (!token || this.isTokenExpired(now)) {
      this.refreshToken();
    } else {
      this.startTokenExpirationTimer();
    }
  }

  private refreshToken(): void {
    const clientCredentialUrl = `${this.url}/clientcredential`;
    const data = {
      ClientId: this.clientId,
      ClientSecretKey: this.clientSecretKey,
    };

    this.http.post(clientCredentialUrl, data).subscribe({
      next: (res: any) => {
        const token = res.accessToken;
        if (token) {
          this.saveToken(token);
          this.startTokenExpirationTimer();
        } else {
          console.error('Token alınamadı');
          this.clearToken();
        }
      },
      error: (err) => {
        console.error('Token yenileme hatası:', err);
        this.clearToken();
      },
    });
  }

  private saveToken(token: string): void {
    const decodedToken = jwt_decode(token);
    if (decodedToken && decodedToken.exp) {
      const expiration = decodedToken.exp * 1000;
      const tokenData = JSON.stringify({ token, expiration });
      this.cookieService.set(
        'client_token',
        tokenData,
        new Date(expiration),
        '/'
      );
    } else {
      console.error('Token içinde exp alanı bulunamadı');
    }
  }

  getToken(): string | null {
    const tokenData = this.cookieService.get('client_token');
    console.log(tokenData);
    if (tokenData) {
      const { token, expiration } = JSON.parse(tokenData);
      const now = new Date().getTime();
      if (expiration < now) {
        this.clearToken();
        return null;
      }
      return token;
    }
    return null;
  }

  private isTokenExpired(now: number): boolean {
    const tokenData = this.cookieService.get('client_token');
    if (tokenData) {
      const { expiration } = JSON.parse(tokenData);
      return expiration < now;
    }
    return true;
  }

  private clearToken(): void {
    this.cookieService.delete('client_token', '/');
    if (this.tokenExpirationTimer) {
      clearTimeout(this.tokenExpirationTimer);
    }
    this.router.navigate(['/']);
  }

  private startTokenExpirationTimer(): void {
    const tokenData = this.cookieService.get('client_token');
    if (tokenData) {
      const { expiration } = JSON.parse(tokenData);
      const now = new Date().getTime();
      const timeLeft = expiration - now;

      if (timeLeft > 0) {
        this.tokenExpirationTimer = setTimeout(() => {
          this.refreshToken();
        }, timeLeft - 5000);
      } else {
        this.refreshToken();
      }
    }
  }
}

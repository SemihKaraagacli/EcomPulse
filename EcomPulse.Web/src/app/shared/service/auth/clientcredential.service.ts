import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { environment } from '../../../../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class ClientcredentialService {
  private url: string = environment.authBaseUrl;
  private clientId: string = environment.ClientId;
  private clientSecretKey: string = environment.ClientSecretKey;
  private tokenExpirationTime: number = 30 * 60 * 1000;

  constructor(private http: HttpClient, private router: Router) {}

  clientcredential() {
    const token = this.getToken();
    const now = new Date().getTime();

    if (!token || this.isTokenExpired(now)) {
      this.router.navigate(['/']).then(() => {
        this.refreshToken().catch((error) => {
          console.error('Token yenileme hatası:', error);
        });
      });
    }
  }

  refreshToken(): Promise<void> {
    const clientCrentialUrl = `${this.url}/clientcredential`;
    const data = {
      ClientId: this.clientId,
      ClientSecretKey: this.clientSecretKey,
    };

    return this.http
      .post(clientCrentialUrl, data)
      .toPromise()
      .then((res: any) => {
        const token = res.accessToken;
        if (token) {
          this.saveToken(token);
        } else {
          console.error('Token alınamadı');
        }
      });
  }

  saveToken(token: string): void {
    if (typeof window !== 'undefined' && typeof document !== 'undefined') {
      const now = new Date().getTime();
      const expiration = now + this.tokenExpirationTime;
      document.cookie = `client_token=${token};expires=${new Date(
        expiration
      ).toUTCString()};path=/`;
      document.cookie = `client_token_expiration=${expiration};expires=${new Date(
        expiration
      ).toUTCString()};path=/`;
    }
  }

  getToken(): string | null {
    if (typeof window !== 'undefined' && typeof document !== 'undefined') {
      const token = this.getCookie('client_token');
      const expiration = this.getCookie('client_token_expiration');

      if (token && expiration) {
        const now = new Date().getTime();
        if (this.isTokenExpired(now)) {
          this.clearToken();
        } else {
          return token;
        }
      }
    }
    return null;
  }

  private getCookie(name: string): string | null {
    const value = `; ${document.cookie}`;
    const parts = value.split(`; ${name}=`);
    if (parts.length === 2) {
      return parts.pop()?.split(';').shift() || null;
    }
    return null;
  }

  private isTokenExpired(now: number): boolean {
    const expiration = this.getCookie('client_token_expiration');
    if (expiration) {
      const isExpired = parseInt(expiration) < now;
      if (isExpired) {
        this.clearToken();
      }
      return isExpired;
    }
    return true;
  }

  private clearToken(): void {
    document.cookie =
      'client_token=; expires=Thu, 01 Jan 1970 00:00:00 UTC; path=/';
    document.cookie =
      'client_token_expiration=; expires=Thu, 01 Jan 1970 00:00:00 UTC; path=/';
    this.router.navigate(['/']);
  }
}

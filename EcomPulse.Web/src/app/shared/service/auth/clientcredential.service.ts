import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { environment } from '../../../../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class ClientcredentialService {
  url: string = environment.authBaseUrl;
  clientId: string = environment.ClientId;
  clientScretKey: string = environment.ClientSecretKey;

  constructor(private http: HttpClient, private router: Router) {}

  clientcredential() {
    const token = this.getToken();
    if (!token) {
      const clientCrentialUrl = `${this.url}/clientcredential`;
      const data = {
        ClientId: this.clientId,
        ClientSecretKey: this.clientScretKey,
      };
      this.http.post(clientCrentialUrl, data).subscribe({
        next: (res: any) => {
          const token = res.accessToken;
          this.saveToken(token);
        },
      });
    }
  }

  saveToken(token: string): void {
    if (
      typeof window !== 'undefined' &&
      typeof window.localStorage !== 'undefined'
    ) {
      const now = new Date().getTime();
      localStorage.setItem('client_token', token);
      localStorage.setItem(
        'client_token_expiration',
        (now + 2 * 60 * 1000).toString()
      );
    }
  }

  getToken(): string | null {
    if (
      typeof window !== 'undefined' &&
      typeof window.localStorage !== 'undefined'
    ) {
      const token = localStorage.getItem('client_token');
      const expiration = localStorage.getItem('client_token_expiration');
      const now = new Date().getTime();

      if (token && expiration) {
        if (parseInt(expiration) < now) {
          localStorage.removeItem('client_token');
          localStorage.removeItem('client_token_expiration');
          window.location.href = '/';
        } else {
          return token;
        }
      }
    }
    return null;
  }
}

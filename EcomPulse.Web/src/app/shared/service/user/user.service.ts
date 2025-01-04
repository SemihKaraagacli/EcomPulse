import { Injectable } from '@angular/core';
import { environment } from '../../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { SignUpViewModel } from '../../model/viewmodels/user/SignUpViewModel';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  url: string = environment.userBaseUrl;
  errorMessage: string = '';
  signUpModel: SignUpViewModel = {
    userName: '',
    password: '',
    email: '',
    phoneNumber: '',
    address: '',
    city: '',
    county: '',
  };
  constructor(private http: HttpClient, private router: Router) {}

  signUp(user: SignUpViewModel) {
    this.http.post(this.url, user).subscribe({
      next: (res) => {
        this.router.navigate(['/']);
      },
      error: (err) => {
        console.log(err);
        this.errorMessage = err.error.detail;
      },
    });
  }
}

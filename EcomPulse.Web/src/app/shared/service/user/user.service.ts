import { Injectable } from '@angular/core';
import { environment } from '../../../../environments/environment';
import { HttpClient, HttpParams } from '@angular/common/http';
import { SignUpViewModel } from '../../model/viewmodels/user/SignUpViewModel';
import { Router } from '@angular/router';
import { UserDto } from '../../model/dtos/user/userDto';
import { CitiesDTO } from '../../model/dtos/user/citiesDTO';
import { CountiesDTO } from '../../model/dtos/user/countiesDTO';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  url: string = environment.userBaseUrl;
  errorMessage: string = '';
  userList: UserDto[] = [];
  cities: CitiesDTO[] = [];
  counties: CountiesDTO[] = [];
  constructor(private http: HttpClient, private router: Router) {}

  signUp(user: SignUpViewModel) {
    this.http.post(this.url, user).subscribe({
      next: (res) => {
        alert('User created successfully');
      },
      error: (err) => {
        console.log(err);
      },
    });
  }

  getAll() {
    this.http.get(this.url).subscribe({
      next: (res) => {
        this.userList = res as UserDto[];
      },
      error: (err) => {
        console.log(err);
        this.errorMessage = err.error.detail;
      },
    });
  }

  getCities() {
    this.http.get('/api/iller').subscribe({
      next: (res) => {
        this.cities = res as CitiesDTO[];
      },
      error: (err) => {},
    });
  }

  getCountries(params: HttpParams) {
    this.http.get('/api/ilceler', { params }).subscribe({
      next: (res) => {
        this.counties = res as CountiesDTO[];
      },
      error: (err) => {},
    });
  }
}

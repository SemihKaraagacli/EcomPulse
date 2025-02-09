import { Component, OnInit } from '@angular/core';
import { Router, RouterModule } from '@angular/router';
import { UserService } from '../../shared/service/user/user.service';
import {
  FormBuilder,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { SignUpViewModel } from '../../shared/model/viewmodels/user/SignUpViewModel';
import { CommonModule } from '@angular/common';
import { HttpParams } from '@angular/common/http';

@Component({
  selector: 'app-signup',
  imports: [RouterModule, ReactiveFormsModule, CommonModule],
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.scss'],
})
export class SignupComponent implements OnInit {
  signupForm!: FormGroup;
  submitted: boolean = false;
  selectedCity: string = '';
  constructor(
    public userService: UserService,
    private formbuilder: FormBuilder,
    private router: Router
  ) {}
  ngOnInit(): void {
    this.signupForm = this.formbuilder.group({
      userName: ['', [Validators.required, Validators.minLength(3)]],
      password: [
        '',
        [
          Validators.required,
          Validators.minLength(6),
          Validators.pattern(/^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{6,}$/),
        ],
      ],
      email: ['', [Validators.required, Validators.email]],
      phoneNumber: [
        '',
        [Validators.required, Validators.pattern(/^[0-9]{11}$/)],
      ],
      address: ['', Validators.required],
      city: ['', Validators.required],
      county: ['', Validators.required],
    });
    this.userService.getCities();
  }
  get f() {
    return this.signupForm.controls;
  }
  onSubmit() {
    this.submitted = true;
    if (this.signupForm.invalid) {
    }
    const signUpModel: SignUpViewModel = {
      userName: this.f['userName'].value,
      password: this.f['password'].value,
      email: this.f['email'].value,
      phoneNumber: this.f['phoneNumber'].value,
      address: this.f['address'].value,
      city: this.f['city'].value,
      county: this.f['county'].value,
    };
    this.userService.signUp(signUpModel);
    this.signupForm.reset();
    setTimeout(() => {
      this.router.navigate(['/']);
    }, 3000);
  }
  onCityChange(event: any) {
    this.selectedCity = event.target.value;
    const selectedCity = this.userService.cities.find(
      (x) => x.ilAdi === this.selectedCity
    );
    if (selectedCity) {
      const cityId = selectedCity.ilId;
      const params = new HttpParams().set('ilkod', cityId);
      this.userService.getCountries(params);
    }
  }
}

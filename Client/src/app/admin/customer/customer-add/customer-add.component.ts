import { Component, OnInit } from '@angular/core';
import { UserService } from '../../../shared/service/user/user.service';
import { SignUpViewModel } from '../../../shared/model/viewmodels/user/SignUpViewModel';
import { CommonModule } from '@angular/common';
import {
  FormBuilder,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-customer-add',
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './customer-add.component.html',
  styleUrls: ['./customer-add.component.scss'],
})
export class CustomerAddComponent implements OnInit {
  customerForm!: FormGroup;
  submitted = false;

  constructor(
    private userService: UserService,
    private formBuilder: FormBuilder,
    private router: Router
  ) {}

  ngOnInit() {
    this.customerForm = this.formBuilder.group({
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
  }

  get f() {
    return this.customerForm.controls;
  }

  OnSubmit() {
    this.submitted = true;

    if (this.customerForm.invalid) {
      return;
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
  }
}

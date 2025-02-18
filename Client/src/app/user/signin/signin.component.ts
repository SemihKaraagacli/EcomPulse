import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../shared/service/auth/auth.service';
import {
  FormBuilder,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { SignInViewModel } from '../../shared/model/viewmodels/auth/SignInViewModel';

@Component({
  selector: 'app-signin',
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './signin.component.html',
  styleUrl: './signin.component.scss',
})
export class SigninComponent implements OnInit {
  submitted: boolean = false;
  signinForm!: FormGroup;
  constructor(
    public authService: AuthService,
    private formbuilder: FormBuilder
  ) {}
  ngOnInit(): void {
    this.signinForm = this.formbuilder.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(6)]],
      rememberMe: [false],
    });
  }
  get f() {
    return this.signinForm.controls;
  }
  onSubmit() {
    this.submitted = true;
    if (this.signinForm.invalid) {
      return;
    }

    const signInModel: SignInViewModel = {
      email: this.f['email'].value,
      password: this.f['password'].value,
      rememberMe: this.f['rememberMe'].value,
    };

    this.authService.signIn(signInModel);
  }
}

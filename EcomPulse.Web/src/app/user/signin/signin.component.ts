import { Component } from '@angular/core';
import { AuthService } from '../../shared/service/auth/auth.service';
import { ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';

@Component({
  selector: 'app-signin',
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './signin.component.html',
  styleUrl: './signin.component.scss',
})
export class SigninComponent {
  constructor(public authService: AuthService, private router: Router) {}

  onSubmit() {
    if (this.authService.form.valid) {
      this.authService.model.email =
        this.authService.form.get('email')?.value || '';
      this.authService.model.password =
        this.authService.form.get('password')?.value || '';
      this.authService.signIn(this.authService.model);
    }
  }
}

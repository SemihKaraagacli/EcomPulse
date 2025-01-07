import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../shared/service/auth/auth.service';
import { ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-signin',
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './signin.component.html',
  styleUrl: './signin.component.scss',
})
export class SigninComponent implements OnInit {
  constructor(public authService: AuthService) {}
  ngOnInit(): void {
    console.log('claims', this.authService.getclaims());
    console.log('token', this.authService.getToken());
  }

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

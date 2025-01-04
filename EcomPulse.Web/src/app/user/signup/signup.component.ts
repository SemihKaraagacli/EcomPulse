import { Component } from '@angular/core';
import { UserService } from '../../shared/service/user/user.service';
import { SignUpViewModel } from '../../shared/model/viewmodels/user/SignUpViewModel';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-signup',
  imports: [FormsModule],
  templateUrl: './signup.component.html',
  styleUrl: './signup.component.scss',
})
export class SignupComponent {
  constructor(public service: UserService) {}

  OnSubmit(): void {
    this.service.signUp(this.service.signUpModel);
  }
}

import { Component, OnInit, Type } from '@angular/core';
import { ProductComponent } from '../product/product.component';
import { AuthService } from '../../shared/service/auth/auth.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-user',
  imports: [ProductComponent, CommonModule],
  templateUrl: './user.component.html',
  styleUrl: './user.component.scss',
})
export class UserComponent implements OnInit {
  isAuthentication: boolean = false;
  userCliams: any = {};
  constructor(public authService: AuthService) {}
  ngOnInit(): void {
    this.isAuthentication = this.authService.isAuthenticated();
    this.userCliams = this.authService.getclaims();
  }
  logout(): void {
    this.authService.logout();
    this.isAuthentication = false;
  }
}

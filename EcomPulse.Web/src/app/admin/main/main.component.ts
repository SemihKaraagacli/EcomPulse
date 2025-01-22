import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { Router, RouterModule } from '@angular/router';
import { BreadcrumbComponent, BreadcrumbItemDirective } from 'xng-breadcrumb';
import { AuthService } from '../../shared/service/auth/auth.service';

@Component({
  selector: 'app-main',
  imports: [
    RouterModule,
    BreadcrumbComponent,
    BreadcrumbItemDirective,
    CommonModule,
  ],
  templateUrl: './main.component.html',
  styleUrl: './main.component.scss',
})
export class MainComponent {
  isAuthentication: boolean = false;
  constructor(public authService: AuthService, private router: Router) {}
  logout(): void {
    this.authService.logout();
    this.router.navigate(['admin/login']);
  }
}

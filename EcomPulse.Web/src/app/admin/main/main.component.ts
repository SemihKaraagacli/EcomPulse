import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
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
  styleUrls: ['./main.component.scss'],
})
export class MainComponent implements OnInit {
  isAuthentication: boolean = false;
  username: string = '';

  constructor(public authService: AuthService, private router: Router) {}

  ngOnInit(): void {
    this.isAuthentication = this.authService.isAuthenticated();
    this.username = this.authService.getclaims()?.username || '';
  }

  logout(): void {
    this.authService.logout();
    this.isAuthentication = false;
    setTimeout(() => {
      this.router.navigate(['/admin/login']);
    }, 100);
  }
}

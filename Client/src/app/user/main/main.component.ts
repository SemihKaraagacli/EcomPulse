import { AuthService } from './../../shared/service/auth/auth.service';
import { NavigationEnd, Router, RouterModule } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-main',
  imports: [RouterModule, CommonModule],
  templateUrl: './main.component.html',
  styleUrl: './main.component.scss',
})
export class MainComponent implements OnInit {
  isOpen = false;
  username: string = '';
  constructor(private router: Router, public authService: AuthService) {}

  ngOnInit(): void {
    this.router.events.subscribe((event) => {
      if (event instanceof NavigationEnd) {
        this.isOpen = false;
      }
    });
    this.username = this.authService.getclaims()?.username || '';
  }
  logout() {
    this.authService.logout();
  }

  toggleDropdown() {
    this.isOpen = !this.isOpen;
  }
}

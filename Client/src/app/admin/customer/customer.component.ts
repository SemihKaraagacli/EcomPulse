import { Component, OnInit } from '@angular/core';
import { UserService } from '../../shared/service/user/user.service';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-customer',
  imports: [CommonModule, RouterModule],
  templateUrl: './customer.component.html',
  styleUrl: './customer.component.scss',
})
export class CustomerComponent implements OnInit {
  constructor(public userService: UserService) {}
  ngOnInit(): void {
    this.userService.getAll();
  }
}

import { Component, OnInit } from '@angular/core';
import { ProductComponent } from '../product/product.component';
import { ProductService } from '../../shared/service/product.service';

@Component({
  selector: 'app-user',
  imports: [ProductComponent],
  templateUrl: './user.component.html',
  styleUrl: './user.component.scss',
})
export class UserComponent implements OnInit {
  constructor(public service: ProductService) {}
  ngOnInit(): void {
    this.service.getAll();
  }
}

import { Component, OnInit } from '@angular/core';
import { ProductService } from '../../shared/service/product.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-product',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './product.component.html',
  styleUrl: './product.component.scss',
})
export class ProductComponent implements OnInit {
  constructor(public service: ProductService) {}
  ngOnInit(): void {
    this.service.getAll();
  }
}

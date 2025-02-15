import { Component, OnInit } from '@angular/core';
import { ProductService } from '../../shared/service/product/product.service';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-product',
  imports: [CommonModule, RouterModule],
  templateUrl: './product.component.html',
  styleUrl: './product.component.scss',
})
export class ProductComponent implements OnInit {
  constructor(public productService: ProductService) {}
  ngOnInit(): void {
    this.productService.getAll();
  }
}

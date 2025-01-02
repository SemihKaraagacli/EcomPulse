import { Component, OnInit } from '@angular/core';
import { ProductService } from '../../shared/service/product/product.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-product',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './product.component.html',
  styleUrl: './product.component.scss',
})
export class ProductComponent {
  constructor(public service: ProductService) {}
  submitForm(event: Event): void {
    event.preventDefault();
    if (this.service.selectedCategory === '') {
      this.service.getAll();
    } else {
      this.service.filterProduct(this.service.selectedCategory);
    }
  }
}

import { Component, OnInit } from '@angular/core';
import { ProductService } from '../../shared/service/product/product.service';
import { CommonModule } from '@angular/common';
import { CategoryService } from '../../shared/service/category/category.service';

@Component({
  selector: 'app-product',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './product.component.html',
  styleUrl: './product.component.scss',
})
export class ProductComponent implements OnInit {
  constructor(
    public service: ProductService,
    public categoryService: CategoryService
  ) {}
  ngOnInit(): void {
    this.categoryService.getAllCategory();
    if (this.service.selectedCategory === '') {
      this.service.getAll();
    }
  }
  submitForm(event: Event): void {
    event.preventDefault();
    if (this.service.selectedCategory === '') {
      this.service.getAll();
    } else {
      this.service.filterProduct(this.service.selectedCategory);
    }
  }
}

import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormsModule, NgForm } from '@angular/forms';
import { ProductService } from '../../../shared/service/product/product.service';
import { Router } from '@angular/router';
import { CategoryService } from '../../../shared/service/category/category.service';

@Component({
  selector: 'app-product-add',
  imports: [CommonModule, FormsModule],
  templateUrl: './product-add.component.html',
  styleUrl: './product-add.component.scss',
})
export class ProductAddComponent implements OnInit {
  constructor(
    public productService: ProductService,
    private router: Router,
    public categoryService: CategoryService
  ) {}
  ngOnInit(): void {
    this.categoryService.getAllCategory();
  }
  OnSubmit(productForm: NgForm): void {
    if (productForm.valid) {
      this.productService.createProduct(this.productService.productCreate);
      this.router.navigate(['/admin/product']);
    }
  }
}

import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ProductService } from '../../shared/service/product/product.service';
import { CommonModule } from '@angular/common';
import { CategoryService } from '../../shared/service/category/category.service';

@Component({
  selector: 'app-products',
  imports: [FormsModule, CommonModule],
  templateUrl: './products.component.html',
  styleUrl: './products.component.scss',
})
export class ProductsComponent implements OnInit {
  selectedCategory: string = '';
  selectedSortOption = '';
  constructor(
    public productService: ProductService,
    public categoryService: CategoryService
  ) {}
  onCategorySelect(event: Event, categoryId: string): void {
    const checkbox = event?.target as HTMLInputElement;

    if (checkbox.checked) {
      this.selectedCategory = categoryId;
      this.productService.filterProduct(categoryId);
    } else {
      this.selectedCategory = '';
      this.productService.getAll();
    }
    this.productService.filterProduct(this.selectedCategory);
  }
  ngOnInit(): void {
    this.categoryService.getAllCategory();
    this.productService.getAll();
    this.sortItems();
  }
  sortItems(): void {
    if (this.selectedSortOption === 'nameA') {
      this.productService.list.sort((a, b) => a.name.localeCompare(b.name));
    } else if (this.selectedSortOption === 'nameB') {
      this.productService.list.sort((a, b) => b.name.localeCompare(a.name));
    } else if (this.selectedSortOption === 'priceA') {
      this.productService.list.sort((a, b) => {
        const priceA = Number(a.price);
        const priceB = Number(b.price);
        return priceA - priceB;
      });
    } else if (this.selectedSortOption === 'priceB') {
      this.productService.list.sort((a, b) => {
        const priceA = Number(a.price);
        const priceB = Number(b.price);
        return priceB - priceA;
      });
    }
  }
}

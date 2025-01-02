import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../../environments/environment';
import { ProductDto } from '../../model/dtos/product/ProductDto';

@Injectable({
  providedIn: 'root',
})
export class ProductService {
  url: string = environment.productBaseUrl;
  selectedCategory: string = '';
  list: ProductDto[] = [];

  constructor(private http: HttpClient) {}

  onCategorySelect(event: Event): void {
    const checkbox = event.target as HTMLInputElement;
    const value = String(checkbox.value);

    if (checkbox.checked) {
      this.selectedCategory = value;
    } else {
      this.selectedCategory = '';
    }
  }
  submitForm(event: Event): void {
    event.preventDefault();
    if (this.selectedCategory === '') {
    } else {
    }
  }

  getAll() {
    this.http.get(this.url).subscribe({
      next: (res) => {
        this.list = res as ProductDto[];
      },
      error: (err) => {
        console.log(err);
      },
    });
  }

  filterProduct(categoryId: string) {
    const url = `${this.url}/Filter/${categoryId}`;
    this.http.get(url).subscribe({
      next: (res) => {
        this.list = res as ProductDto[];
      },
      error: (err) => {
        console.log(err);
      },
    });
  }
}

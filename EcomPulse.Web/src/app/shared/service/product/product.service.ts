import { AuthService } from './../auth/auth.service';
import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from '../../../../environments/environment';
import { ProductDto } from '../../model/dtos/product/ProductDto';
import { ProductCreateRequestViewModel } from '../../model/viewmodels/product/ProductCreateViewModel';

@Injectable({
  providedIn: 'root',
})
export class ProductService {
  url: string = environment.productBaseUrl;
  selectedCategory: string = '';
  list: ProductDto[] = [];
  productCreate: ProductCreateRequestViewModel = {
    name: '',
    description: '',
    price: 0,
    stock: 0,
    categoryId: '',
  };

  constructor(private http: HttpClient, private authService: AuthService) {}

  onCategorySelect(event: Event): void {
    const checkbox = event.target as HTMLInputElement;
    const value = String(checkbox.value);

    const checkboxes = document.querySelectorAll(
      'input[type="checkbox"][name="category"]'
    );

    if (checkbox.checked) {
      checkboxes.forEach((cb: Element) => {
        const input = cb as HTMLInputElement;
        if (input !== checkbox) {
          input.checked = false;
        }
      });
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

  createProduct(data: ProductCreateRequestViewModel) {
    this.http.post(this.url, data).subscribe({
      next: (res) => {},
      error: (err) => {
        console.log(`Error3: ${err.detail}`);
      },
    });
  }
}

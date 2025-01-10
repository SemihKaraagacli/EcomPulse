import { AuthService } from './../auth/auth.service';
import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from '../../../../environments/environment';
import { ProductDto } from '../../model/dtos/product/ProductDto';

@Injectable({
  providedIn: 'root',
})
export class ProductService {
  url: string = environment.productBaseUrl;
  selectedCategory: string = '';
  list: ProductDto[] = [];

  constructor(private http: HttpClient, private authService: AuthService) {}

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
    const token = this.authService.getToken()?.toString();
    console.log('Token 5', token);
    const headers = new HttpHeaders({
      Authorization: `Bearer ${token}`,
    });
    console.log('authantication', headers);
    this.http.get(this.url).subscribe({
      next: (res) => {
        this.list = res as ProductDto[];
        console.log(this.list);
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

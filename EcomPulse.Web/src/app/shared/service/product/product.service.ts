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
  list: ProductDto[] = [];
  productCreate: ProductCreateRequestViewModel = {
    name: '',
    description: '',
    price: 0,
    stock: 0,
    categoryId: '',
  };

  constructor(private http: HttpClient, private authService: AuthService) {}

  filterProduct(categoryId: string) {
    const url = `${this.url}/Filter/${categoryId}`;
    this.http.get(url).subscribe({
      next: (res) => {
        this.list = res as ProductDto[];
      },
      error: (err) => {},
    });
  }
  getAll() {
    this.http.get(this.url).subscribe({
      next: (res) => {
        this.list = res as ProductDto[];
      },
      error: (err) => {},
    });
  }

  createProduct(data: ProductCreateRequestViewModel) {
    this.http.post(this.url, data).subscribe({
      next: (res) => {},
      error: (err) => {},
    });
  }
}

import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environment';
import { ProductDto } from '../model/dtos/product/ProductDto';

@Injectable({
  providedIn: 'root',
})
export class ProductService {
  url: string = environment.productBaseUrl;
  list: ProductDto[] = [];
  constructor(private http: HttpClient) {}

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
}

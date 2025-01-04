import { HttpClient } from '@angular/common/http';
import { environment } from './../../../../environments/environment';
import { Injectable } from '@angular/core';
import { CategoryDto } from '../../model/dtos/category/CategoryDto';

@Injectable({
  providedIn: 'root',
})
export class CategoryService {
  url: string = environment.categoryBaseUrl;
  categoryList: CategoryDto[] = [];
  constructor(private http: HttpClient) {}
  getAllCategory() {
    this.http.get(this.url).subscribe({
      next: (res) => {
        this.categoryList = res as CategoryDto[];
      },
      error: (err) => {
        console.log(err);
      },
    });
  }
}

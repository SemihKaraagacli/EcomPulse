import { HttpClient } from '@angular/common/http';
import { environment } from './../../../../environments/environment';
import { Injectable } from '@angular/core';
import { CategoryDto } from '../../model/dtos/category/CategoryDto';
import { CategoryCreateViewModel } from '../../model/viewmodels/category/categoryCreateViewModel';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root',
})
export class CategoryService {
  url: string = environment.categoryBaseUrl;
  categoryList: CategoryDto[] = [];
  category: CategoryDto = {
    id: '',
    name: '',
  };
  categoryAdd: CategoryCreateViewModel = {
    name: '',
  };

  constructor(private http: HttpClient, private router: Router) {}
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

  createCategory(data: CategoryCreateViewModel) {
    this.http.post(this.url, data).subscribe({
      next: (res) => {
        this.router.navigate(['/admin/category']);
      },
      error: (err) => {
        console.log(`Error: ${err}`);
      },
    });
  }

  getById(id: any) {
    this.http.get(`${this.url}/${id}`).subscribe({
      next: (res) => {
        this.category = res as CategoryDto;
      },
      error: (err) => {
        `${err.detail}`;
      },
    });
  }

  updateCategory(id: any, data: CategoryDto) {
    this.http.put(`${this.url}/${id}`, data).subscribe({
      next: (res) => {},
      error: (err) => {
        `Error: ${err.detail}`;
      },
    });
  }
}

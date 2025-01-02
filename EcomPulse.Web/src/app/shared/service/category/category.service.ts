import { HttpClient } from '@angular/common/http';
import { environment } from './../../../../environments/environment';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class CategoryService {
  url: string = environment.categoryBaseUrl;
  categoryList: any[] = [];
  constructor(private http: HttpClient) {}
  getAllCategory() {
    this.http.get(this.url).subscribe({
      next: (res) => {
        this.categoryList = res as any[];
      },
      error: (err) => {
        console.log(err);
      },
    });
  }
}

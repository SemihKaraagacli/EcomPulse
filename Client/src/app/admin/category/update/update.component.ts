import { Component, OnInit } from '@angular/core';
import { CategoryService } from '../../../shared/service/category/category.service';
import { ActivatedRoute, Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-update',
  imports: [CommonModule, FormsModule],
  templateUrl: './update.component.html',
  styleUrl: './update.component.scss',
})
export class UpdateComponent implements OnInit {
  id: any = '';
  constructor(
    public categoryService: CategoryService,
    private route: ActivatedRoute,
    private router: Router
  ) {}
  ngOnInit(): void {
    this.id = this.route.snapshot.paramMap.get('id');
    this.categoryService.getById(this.id);
  }

  OnSubmit() {
    this.categoryService.updateCategory(this.id, this.categoryService.category);
    this.router.navigate(['admin/category']);
  }
}

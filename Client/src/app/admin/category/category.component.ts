import { Component, OnInit } from '@angular/core';
import { CategoryService } from '../../shared/service/category/category.service';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-category',
  imports: [CommonModule, RouterModule],
  templateUrl: './category.component.html',
  styleUrl: './category.component.scss',
})
export class CategoryComponent implements OnInit {
  constructor(public categoryService: CategoryService) {}
  ngOnInit(): void {
    this.categoryService.getAllCategory();
  }
}

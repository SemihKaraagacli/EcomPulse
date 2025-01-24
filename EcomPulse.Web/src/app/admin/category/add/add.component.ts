import { Component } from '@angular/core';
import { CategoryService } from '../../../shared/service/category/category.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-add',
  imports: [CommonModule, FormsModule],
  templateUrl: './add.component.html',
  styleUrl: './add.component.scss',
})
export class AddComponent {
  constructor(public categoryService: CategoryService) {}
  OnSubmit() {
    this.categoryService.createCategory(this.categoryService.categoryAdd);
  }
}

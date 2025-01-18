import { Component } from '@angular/core';
import { RouterModule } from '@angular/router';
import { BreadcrumbComponent, BreadcrumbItemDirective } from 'xng-breadcrumb';

@Component({
  selector: 'app-main',
  imports: [RouterModule, BreadcrumbComponent, BreadcrumbItemDirective],
  templateUrl: './main.component.html',
  styleUrl: './main.component.scss',
})
export class MainComponent {}

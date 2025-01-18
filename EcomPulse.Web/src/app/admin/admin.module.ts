import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { MainComponent } from './main/main.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { CategoryComponent } from './category/category.component';
import { ProductComponent } from './product/product.component';
import { OrderComponent } from './order/order.component';
import { CustomerComponent } from './customer/customer.component';

const routes: Routes = [
  { path: 'login', component: LoginComponent },
  {
    path: '',
    component: MainComponent,
    children: [
      {
        path: 'dashboard',
        component: DashboardComponent,
        data: { breadcrumb: { label: 'Dashboard', disable: true } },
      },
      {
        path: 'category',
        component: ProductComponent,
        data: { breadcrumb: { label: 'Category', disable: true } },
      },
      {
        path: 'product',
        component: ProductComponent,
        data: { breadcrumb: { label: 'Product', disable: true } },
      },
      {
        path: 'order',
        component: OrderComponent,
        data: { breadcrumb: { label: 'Order', disable: true } },
      },
      {
        path: 'customers',
        component: CustomerComponent,
        data: { breadcrumb: { label: 'Customers', disable: true } },
      },
    ],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class AdminRoutingModule {}

@NgModule({
  imports: [AdminRoutingModule, MainComponent],
})
export class AdminModule {}

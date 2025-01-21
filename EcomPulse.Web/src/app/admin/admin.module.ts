import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { MainComponent } from './main/main.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { CategoryComponent } from './category/category.component';
import { ProductComponent } from './product/product.component';
import { OrderComponent } from './order/order.component';
import { CustomerComponent } from './customer/customer.component';
import { AddComponent } from './category/add/add.component';
import { ProductAddComponent } from './product/product-add/product-add.component';
import { CustomerAddComponent } from './customer/customer-add/customer-add.component';

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
        component: CategoryComponent,
        data: { breadcrumb: { label: 'Category', disable: true } },
      },
      {
        path: 'category/add',
        component: AddComponent,
        data: { breadcrumb: { label: 'Category > Add', disable: true } },
      },
      {
        path: 'product',
        component: ProductComponent,
        data: { breadcrumb: { label: 'Product', disable: true } },
      },
      {
        path: 'product/add',
        component: ProductAddComponent,
        data: { breadcrumb: { label: 'Product > Add', disable: true } },
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
      {
        path: 'customers/add',
        component: CustomerAddComponent,
        data: { breadcrumb: { label: 'Customers > Add', disable: true } },
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

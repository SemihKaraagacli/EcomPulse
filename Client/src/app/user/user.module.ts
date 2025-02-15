import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MainComponent } from './main/main.component';
import { ProductsComponent } from './products/products.component';
import { MainpageComponent } from './mainpage/mainpage.component';
import { SignupComponent } from './signup/signup.component';
import { SigninComponent } from './signin/signin.component';
import { notAuthGuard } from '../shared/service/auth/guards/not-auth.guard';

const routes: Routes = [
  {
    path: '',
    component: MainComponent,
    children: [
      {
        path: '',
        component: MainpageComponent,
      },

      {
        path: 'products',
        component: ProductsComponent,
      },
      {
        path: 'signup',
        component: SignupComponent,
      },
      {
        path: 'signin',
        component: SigninComponent,
        canActivate: [notAuthGuard],
      },
    ],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class UserModule {}

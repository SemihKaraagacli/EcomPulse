import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { UserComponent } from './user/user.component';
import { SignupComponent } from './signup/signup.component';
import { SigninComponent } from './signin/signin.component';
import { notAuthGuard } from '../shared/service/auth/guards/not-auth.guard';
import { MainComponent } from './main/main.component';

const routes: Routes = [
  { path: '', component: UserComponent },
  { path: 'signup', component: SignupComponent },
  { path: 'signin', component: SigninComponent, canActivate: [notAuthGuard] },
  { path: 'main', component: MainComponent },
];

@NgModule({
  imports: [RouterModule.forChild(routes), UserComponent],
  exports: [RouterModule],
})
export class UserModule {}

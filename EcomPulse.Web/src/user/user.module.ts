import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { UserComponent } from './user/user.component';
import { SignupComponent } from './signup/signup.component';

const routes: Routes = [
  { path: '', component: UserComponent },
  { path: 'signup', component: SignupComponent },
];

@NgModule({
  imports: [RouterModule.forChild(routes), UserComponent],
  exports: [RouterModule],
})
export class UserModule {}

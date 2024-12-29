import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AdminComponent } from './admin/admin.component';

const routes: Routes = [
  { path: '', component: AdminComponent },
  // Diğer admin rotaları burada tanımlanabilir
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AdminRoutingModule { }

@NgModule({
  imports: [AdminRoutingModule,AdminComponent]
})
export class AdminModule { }

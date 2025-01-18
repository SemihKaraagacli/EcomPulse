import { Component, OnInit } from '@angular/core';
import { RouterModule, RouterOutlet } from '@angular/router';
import { ClientcredentialService } from './shared/service/auth/clientcredential.service';
import { BreadcrumbComponent, BreadcrumbItemDirective } from 'xng-breadcrumb';

@Component({
  selector: 'app-root',
  imports: [
    RouterOutlet,
    RouterModule,
    BreadcrumbComponent,
    BreadcrumbItemDirective,
  ],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss',
})
export class AppComponent implements OnInit {
  constructor(private clientCredential: ClientcredentialService) {}
  ngOnInit(): void {
    this.clientCredential.clientcredential();
  }
  title = 'EcomPulse.Web';
}

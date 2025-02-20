import { Component, OnInit } from '@angular/core';
import { RouterModule, RouterOutlet } from '@angular/router';
import { ClientcredentialService } from './shared/service/auth/clientcredential.service';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, RouterModule],
  template: `<router-outlet></router-outlet>`,
  styleUrl: './app.component.scss',
})
export class AppComponent implements OnInit {
  constructor(private clientCredentialService: ClientcredentialService) {}

  ngOnInit(): void {
    this.clientCredentialService.initializeToken();
  }
}

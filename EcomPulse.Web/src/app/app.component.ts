import { Component, OnInit } from '@angular/core';
import { RouterModule, RouterOutlet } from '@angular/router';
import { ClientcredentialService } from './shared/service/auth/clientcredential.service';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, RouterModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss',
})
export class AppComponent implements OnInit {
  constructor(private clientCredential: ClientcredentialService) {}
  ngOnInit(): void {
    this.clientCredential.clientcredential();
  }
}

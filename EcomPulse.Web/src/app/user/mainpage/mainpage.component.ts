import { Component, OnInit } from '@angular/core';
import { ClientcredentialService } from '../../shared/service/auth/clientcredential.service';

@Component({
  selector: 'app-mainpage',
  imports: [],
  templateUrl: './mainpage.component.html',
  styleUrl: './mainpage.component.scss',
})
export class MainpageComponent implements OnInit {
  constructor(private clientCredential: ClientcredentialService) {}
  ngOnInit(): void {
    this.clientCredential.clientcredential();
  }
}

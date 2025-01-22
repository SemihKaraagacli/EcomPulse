/* tslint:disable:no-unused-variable */

import { TestBed, inject } from '@angular/core/testing';
import { ClientcredentialService } from './clientcredential.service';

describe('Service: Clientcredential', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [ClientcredentialService],
    });
  });

  it('should ...', inject(
    [ClientcredentialService],
    (service: ClientcredentialService) => {
      expect(service).toBeTruthy();
    }
  ));
});

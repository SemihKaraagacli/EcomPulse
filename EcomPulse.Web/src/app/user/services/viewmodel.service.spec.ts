import { TestBed } from '@angular/core/testing';

import { ViewmodelService } from './viewmodel.service';

describe('ViewmodelService', () => {
  let service: ViewmodelService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ViewmodelService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});

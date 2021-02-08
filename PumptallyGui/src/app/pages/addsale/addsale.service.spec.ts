import { TestBed } from '@angular/core/testing';

import { AddsaleService } from './addsale.service';

describe('AddsaleService', () => {
  let service: AddsaleService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(AddsaleService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});

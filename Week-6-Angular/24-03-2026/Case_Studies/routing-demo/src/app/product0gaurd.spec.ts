import { TestBed } from '@angular/core/testing';

import { Product0gaurd } from './product0gaurd';

describe('Product0gaurd', () => {
  let service: Product0gaurd;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(Product0gaurd);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});

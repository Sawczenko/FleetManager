import { TestBed } from '@angular/core/testing';

import { OrderManagementFilterService } from './order-management-filter.service';

describe('OrderManagementFilterService', () => {
  let service: OrderManagementFilterService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(OrderManagementFilterService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});

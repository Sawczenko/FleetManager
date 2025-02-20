import { Injectable } from '@angular/core';
import {ApiService} from '../../../../../../core/services/api/api.service';
import {Observable} from 'rxjs';
import {OrderManagementFilter} from '../../models/order-management-filter';

@Injectable({
  providedIn: 'root'
})
export class OrderManagementFilterService {

  constructor(private apiService: ApiService) { }

  public getOrderManagementFilter(): Observable<OrderManagementFilter> {
    return this.apiService.get<OrderManagementFilter>('/orders/OrderManagementFilter');
  }
}

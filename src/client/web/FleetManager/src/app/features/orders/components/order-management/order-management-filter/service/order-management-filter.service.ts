import { Injectable } from '@angular/core';
import {ApiService} from '../../../../../../core/services/api/api.service';
import {Observable} from 'rxjs';
import {OrderManagementFilterFormData} from '../../models/order-management-filter-form-data';

@Injectable({
  providedIn: 'root'
})
export class OrderManagementFilterService {

  constructor(private apiService: ApiService) { }

  public getOrderManagementFilter(): Observable<OrderManagementFilterFormData> {
    return this.apiService.get<OrderManagementFilterFormData>('/orders/OrderManagementFilter');
  }
}

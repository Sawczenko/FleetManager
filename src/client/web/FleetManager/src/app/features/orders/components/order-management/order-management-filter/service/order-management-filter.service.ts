import { Injectable } from '@angular/core';
import {ApiService} from '../../../../../../core/services/api/api.service';
import {Order} from '../../models/order';
import {Observable} from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class OrderManagementFilterService {

  constructor(private apiService: ApiService) { }

  public getOrderManagementFilter(): Observable<Order[]> {
    return this.apiService.get<Order[]>('/orders/OrderManagementFilter');
  }
}

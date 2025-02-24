import { Injectable } from '@angular/core';
import {ApiService} from '../../../../../core/services/api/api.service';
import {Observable} from 'rxjs';
import {Order} from '../models/order';
import {HttpParams} from '@angular/common/http';
import {OrdersFilter} from '../models/orders-filter';

@Injectable({
  providedIn: 'root'
})
export class OrderManagementService {

  constructor(private apiService: ApiService) { }

  public getOrders(ordersFilter?: OrdersFilter): Observable<Order[]> {
    let params = new HttpParams();

    if(ordersFilter)
    {
      params = params.set('contractorId', ordersFilter.contractorId);
      params = params.set('originLocationId', ordersFilter.originLocationId);
      params = params.set('destinationLocationId', ordersFilter.destinationLocationId);

      if(ordersFilter.pickupDateFrom){
        params = params.set('pickupDateFrom', ordersFilter.pickupDateFrom.toDateString());
      }

      if(ordersFilter.pickupDateTo){
        params = params.set('pickupDateTo', ordersFilter.pickupDateTo.toDateString());
      }

      if(ordersFilter.deliveryDateFrom){
        params = params.set('deliveryDateFrom', ordersFilter.deliveryDateFrom.toDateString());
      }

      if(ordersFilter.deliveryDateTo){
        params = params.set('deliveryDateTo', ordersFilter.deliveryDateTo.toDateString());
      }
    }

    return this.apiService.get<Order[]>('/orders', params);
  }

}

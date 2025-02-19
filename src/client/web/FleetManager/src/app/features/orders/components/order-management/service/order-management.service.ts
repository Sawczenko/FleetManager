import { Injectable } from '@angular/core';
import {ApiService} from '../../../../../core/services/api/api.service';
import {Observable} from 'rxjs';
import {Order} from '../models/order';

@Injectable({
  providedIn: 'root'
})
export class OrderManagementService {

  constructor(private apiService: ApiService) { }

  public getOrders(): Observable<Order[]> {
    return this.apiService.get<Order[]>('/orders');
  }
}

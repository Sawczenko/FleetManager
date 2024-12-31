import { Injectable } from '@angular/core';
import {ApiService} from '../../../../core/services/api/api.service';
import {Observable} from 'rxjs';
import {Vehicle} from '../models/vehicle';

@Injectable({
  providedIn: 'root'
})
export class VehicleListService {

  constructor(private apiService: ApiService) { }

  public getVehicles(): Observable<Vehicle[]>{
    return this.apiService.get<Vehicle[]>('/vehicles');
  }
}

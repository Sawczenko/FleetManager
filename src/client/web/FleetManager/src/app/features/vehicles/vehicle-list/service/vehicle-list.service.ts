import { Injectable } from '@angular/core';
import {ApiService} from '../../../../core/services/api/api.service';
import {Observable} from 'rxjs';
import {Vehicle} from '../models/vehicle';
import {VehiclesFilter} from '../filter/models/vehicles-filter';
import {HttpParams} from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class VehicleListService {

  constructor(private apiService: ApiService) { }

  public getFilteredVehicles(vehiclesFilter: VehiclesFilter): Observable<Vehicle[]>{
    let params = new HttpParams();

    params = params.set('vin', vehiclesFilter.vin)
    params = params.set('licensePlate', vehiclesFilter.licensePlate)
    params = params.set('model', vehiclesFilter.model)


    return this.apiService.get<Vehicle[]>('/vehicles', params);
  }

  public getVehicles(): Observable<Vehicle[]>{
    return this.apiService.get<Vehicle[]>('/vehicles');
  }
}

import { Injectable } from '@angular/core';
import {ApiService} from '../../../../core/services/api/api.service';
import {Observable} from 'rxjs';
import {VehicleManagement} from '../models/vehicle-management';
import {HttpParams} from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class VehicleManagementService {

  constructor(private apiService: ApiService) { }

  public getVehicleManagement(vehicleId: string): Observable<VehicleManagement> {
    return this.apiService.get<VehicleManagement>(`/vehicles/management/${vehicleId}`);
  }
}

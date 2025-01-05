import { Injectable } from '@angular/core';
import {ApiService} from '../../../../core/services/api/api.service';
import {Observable} from 'rxjs';
import {VehicleManagement} from '../models/vehicle-management';
import {Result} from '../../../../core/services/api/models/result';
import {Inspection} from '../models/inspection';
import {Repair} from '../models/repair';

@Injectable({
  providedIn: 'root'
})
export class VehicleManagementService {

  constructor(private apiService: ApiService) { }

  public getVehicleManagement(vehicleId: string): Observable<VehicleManagement> {
    return this.apiService.get<VehicleManagement>(`/vehicles/management/${vehicleId}`);
  }

  public addInspection(inspection: Inspection): Observable<Result> {
    return this.apiService.post<Result>('/vehicles/management/inspection', inspection);
  }

  public addRepair(repair: Repair): Observable<Result> {
    return this.apiService.post<Result>('/vehicles/management/repair', repair);

  }
}

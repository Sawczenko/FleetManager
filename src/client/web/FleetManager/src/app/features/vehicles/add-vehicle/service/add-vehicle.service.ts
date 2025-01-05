import { Injectable } from '@angular/core';
import {ApiService} from '../../../../core/services/api/api.service';
import {AddVehicleRequest} from '../models/add-vehicle-request';
import {Observable} from 'rxjs';
import {Result} from '../../../../core/services/api/models/result';

@Injectable({
  providedIn: 'root'
})
export class AddVehicleService {

  constructor(private apiService: ApiService) { }

  public addVehicle(request: AddVehicleRequest): Observable<Result> {
    return this.apiService.post<Result>('/vehicles', request);
  }
}

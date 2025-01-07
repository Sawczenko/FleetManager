import { Injectable } from '@angular/core';
import {ApiService} from '../../../../core/services/api/api.service';
import {Observable} from 'rxjs';
import {RoutePlannerForm} from '../models/route-planner-form';
import {AddRouteRequest} from '../models/add-route-request';
import {Result} from '../../../../core/services/api/models/result';

@Injectable({
  providedIn: 'root'
})
export class RoutePlannerService {

  constructor(private apiService: ApiService) { }

  public getRoutePlannerForm(): Observable<RoutePlannerForm>{
    return this.apiService.get<RoutePlannerForm>('/routes/route-planner')
  }

  public addRoute(addRouteRequest: AddRouteRequest): Observable<Result>{
    console.log(addRouteRequest)
    return this.apiService.post<Result>('/routes', addRouteRequest);
  }
}

import { Injectable } from '@angular/core';
import {ApiService} from '../../../../core/services/api/api.service';
import {Observable} from 'rxjs';
import {Route} from '../models/route';
import {RoutesFilter} from '../filter/models/routes-filter';
import {HttpParams} from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class RouteListService {

  constructor(private apiService: ApiService) { }

  public getFilteredRoutes(routesFilter: RoutesFilter): Observable<Route[]> {
    let params = new HttpParams();

    params = params.set('userName', routesFilter.userName);
    params = params.set('startLocation', routesFilter.startLocation);
    params = params.set('endLocation', routesFilter.endLocation);
    params = params.set('status', routesFilter.status);

    if(routesFilter.scheduledStartTime){
      params = params.set('scheduledStartTime', routesFilter.scheduledStartTime.toDateString());
    }

    if(routesFilter.endTime){
      params = params.set('endTime', routesFilter.endTime.toDateString());
    }

    return this.apiService.get<Route[]>('/routes', params);
  }

  public getRoutes(): Observable<Route[]> {
    return this.apiService.get<Route[]>('/routes');
  }
}

import { Injectable } from '@angular/core';
import {ApiService} from '../../../../core/services/api/api.service';
import {Observable} from 'rxjs';
import {HomeDashboard} from '../models/home-dashboard';

@Injectable({
  providedIn: 'root'
})
export class HomeDashboardService {

  constructor(private apiService: ApiService) { }

  public getHomeDashboard(): Observable<HomeDashboard> {
    return this.apiService.get<HomeDashboard>('/dashboards');
  }
}

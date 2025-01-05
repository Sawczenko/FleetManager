import {Component, OnInit} from '@angular/core';
import {RouteListService} from './service/route-list.service';
import {Observable} from 'rxjs';
import {Route} from './models/route';
import {VehiclesFilter} from '../../vehicles/vehicle-list/filter/models/vehicles-filter';
import {RoutesFilter} from './filter/models/routes-filter';

@Component({
  selector: 'app-route-list',
  standalone: false,

  templateUrl: './route-list.component.html',
  styleUrl: './route-list.component.css'
})
export class RouteListComponent implements OnInit{

  public routes$: Observable<Route[]> = new Observable<Route[]>();

  constructor(private routeListService: RouteListService) {
  }

  ngOnInit(): void {
    this.routes$ = this.routeListService.getRoutes();
  }

  public handleFilterChanged(routesFilter: RoutesFilter): void {
    this.routes$ = this.routeListService.getFilteredRoutes(routesFilter);
  }
}

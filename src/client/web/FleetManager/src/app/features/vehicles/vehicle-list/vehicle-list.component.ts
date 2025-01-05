import {Component, OnInit} from '@angular/core';
import {VehicleListService} from './service/vehicle-list.service';
import {Vehicle} from './models/vehicle';
import {Observable} from 'rxjs';
import {VehiclesFilter} from './filter/models/vehicles-filter';

@Component({
  selector: 'app-vehicle-list',
  standalone: false,

  templateUrl: './vehicle-list.component.html',
  styleUrl: './vehicle-list.component.css'
})
export class VehicleListComponent implements OnInit{

  public vehicles$: Observable<Vehicle[]> = new Observable<Vehicle[]>();

  constructor(private vehicleService: VehicleListService) {
  }

  ngOnInit(): void {
        this.vehicles$ = this.vehicleService.getVehicles();
  }

  public handleFilterApplied(vehiclesFilter: VehiclesFilter): void {
    this.vehicles$ = this.vehicleService.getFilteredVehicles(vehiclesFilter);
  }
}

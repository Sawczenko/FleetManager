import {Component, OnInit} from '@angular/core';
import {VehicleListService} from './service/vehicle-list.service';
import {Vehicle} from './models/vehicle';
import {Observable} from 'rxjs';

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
}

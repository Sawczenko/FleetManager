import {Component, OnInit} from '@angular/core';
import {VehicleManagementService} from './service/vehicle-management.service';
import {ActivatedRoute} from '@angular/router';
import {Observable} from 'rxjs';
import {VehicleManagement} from './models/vehicle-management';

@Component({
  selector: 'app-vehicle-management',
  standalone: false,

  templateUrl: './vehicle-management.component.html',
  styleUrl: './vehicle-management.component.css'
})
export class VehicleManagementComponent implements OnInit {

  public vehicleManagement$: Observable<VehicleManagement> = new Observable<VehicleManagement>();
  private vehicleId: string;

  constructor(private vehicleManagementService: VehicleManagementService, private activatedRoute: ActivatedRoute) {
    this.vehicleId = activatedRoute.snapshot.params['id'];
  }

  ngOnInit(): void {
      this.vehicleManagement$ = this.vehicleManagementService.getVehicleManagement(this.vehicleId);
    }
}

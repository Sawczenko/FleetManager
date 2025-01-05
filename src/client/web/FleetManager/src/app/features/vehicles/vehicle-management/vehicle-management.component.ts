import {Component, OnInit} from '@angular/core';
import {VehicleManagementService} from './service/vehicle-management.service';
import {ActivatedRoute} from '@angular/router';
import {Observable, Subject} from 'rxjs';
import {VehicleManagement} from './models/vehicle-management';

@Component({
  selector: 'app-vehicle-management',
  standalone: false,

  templateUrl: './vehicle-management.component.html',
  styleUrl: './vehicle-management.component.css'
})
export class VehicleManagementComponent implements OnInit {

  public vehicleManagement$: Subject<VehicleManagement> = new Subject<VehicleManagement>();
  private vehicleId: string;

  constructor(private vehicleManagementService: VehicleManagementService, private activatedRoute: ActivatedRoute) {
    this.vehicleId = activatedRoute.snapshot.params['id'];
  }

  ngOnInit(): void {
      this.getVehicleManagement(this.vehicleId);
    }

    private getVehicleManagement(vehicleId: string): void {

      this.vehicleManagementService.getVehicleManagement(this.vehicleId)
        .subscribe({
          next: vehicleManagement => {
            this.vehicleManagement$.next(vehicleManagement);
          }
        });
    }

    public vehicleModified(eventData: any){
      this.getVehicleManagement(this.vehicleId);
    }
}

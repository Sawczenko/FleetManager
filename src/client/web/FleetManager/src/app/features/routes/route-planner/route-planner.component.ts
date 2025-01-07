import {Component, OnInit} from '@angular/core';
import {FormBuilder, FormGroup, Validators} from '@angular/forms';
import {User} from './models/user';
import {RouteLocation} from './models/route-location';
import {RoutePlannerService} from './service/route-planner.service';
import {Observable} from 'rxjs';
import {RoutePlannerForm} from './models/route-planner-form';
import {Vehicle} from './models/vehicle';
import {AddRouteRequest} from './models/add-route-request';
import {RouteSummary} from './models/route-summary';

@Component({
  selector: 'app-route-planner',
  standalone: false,

  templateUrl: './route-planner.component.html',
  styleUrl: './route-planner.component.css'
})
export class RoutePlannerComponent implements OnInit{
  public routePlannerForm$: Observable<RoutePlannerForm> = new Observable<RoutePlannerForm>();

  public routeForm!: FormGroup;
  public locationForm!: FormGroup;
  public addRouteRequest!: AddRouteRequest;
  public routeSummary!: RouteSummary;

  public users: User[] = [];
  public vehicles: Vehicle[] = [];
  public locations: RouteLocation[] = [];

  constructor(private fb: FormBuilder, private routePlannerService: RoutePlannerService) {}

  ngOnInit(): void {
    this.routeForm = this.fb.group({
      user: [null, Validators.required],
      vehicle: [null, Validators.required],
      scheduledStartTime: [null, Validators.required],
    });

    this.locationForm = this.fb.group({
      startLocation: [null, Validators.required],
      endLocation: [null, Validators.required],
    });

    this.routePlannerForm$ = this.routePlannerService.getRoutePlannerForm();

    this.routePlannerForm$.subscribe({
      next: routePlannerForm => {
        console.log(routePlannerForm);
        this.users = routePlannerForm.users;
        this.locations = routePlannerForm.locations;
        this.vehicles = routePlannerForm.vehicles;
      }
    })
  }

  createRequest() {
    const routeFormValue = this.routeForm.value;
    const locationFormValue = this.locationForm.value;

    this.addRouteRequest = new AddRouteRequest(
      routeFormValue.user.id,
      routeFormValue.vehicle.id,
      routeFormValue.scheduledStartTime,
      locationFormValue.startLocation.id,
      locationFormValue.endLocation.id,
    )

    this.routeSummary = new RouteSummary(
      routeFormValue.user.name,
      routeFormValue.vehicle.name,
      routeFormValue.scheduledStartTime,
      locationFormValue.startLocation.name,
      locationFormValue.endLocation.name,
    )

    console.log(this.routeSummary);
  }

  addRoute() {
    this.routePlannerService.addRoute(this.addRouteRequest).subscribe({
      next: result => {
        console.log(result);
      },
      error: err => {
        console.log(err);
      }
    })
  }
}

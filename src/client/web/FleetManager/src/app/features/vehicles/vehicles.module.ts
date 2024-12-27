import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { VehicleListComponent } from './vehicle-list/vehicle-list.component';
import {RouterModule, Routes} from '@angular/router';
import {HomeDashboardComponent} from '../dashboard/home-dashboard/home-dashboard.component';
import { VehicleComponent } from './vehicle-list/vehicle/vehicle.component';
import {MatCardModule} from '@angular/material/card';

const routes: Routes = [
  {path: '', component: VehicleListComponent},
]


@NgModule({
  declarations: [
    VehicleListComponent,
    VehicleComponent
  ],
  imports: [
    CommonModule,
    MatCardModule,
    RouterModule.forChild(routes),
  ]
})
export class VehiclesModule { }

import { NgModule } from '@angular/core';
import {CommonModule, NgOptimizedImage} from '@angular/common';
import { VehicleListComponent } from './vehicle-list/vehicle-list.component';
import {RouterModule, Routes} from '@angular/router';
import {HomeDashboardComponent} from '../dashboard/home-dashboard/home-dashboard.component';
import { VehicleComponent } from './vehicle-list/vehicle/vehicle.component';
import {MatCardModule} from '@angular/material/card';
import {MatButton, MatButtonModule} from '@angular/material/button';
import { VehicleManagementComponent } from './vehicle-management/vehicle-management.component';
import {MatTabsModule} from '@angular/material/tabs';
import { InspectionsComponent } from './vehicle-management/inspections/inspections.component';
import { RepairsComponent } from './vehicle-management/repairs/repairs.component';
import { MatTableModule } from '@angular/material/table';
import {MatExpansionModule} from '@angular/material/expansion';
import {MatFormFieldModule} from '@angular/material/form-field';
import {MatInputModule} from '@angular/material/input';
import {ReactiveFormsModule} from '@angular/forms';
import {AuthGuard} from '../../core/guards/auth-guard';
import {FilterComponent} from './vehicle-list/filter/filter.component';
import { AddVehicleComponent } from './add-vehicle/add-vehicle.component';
import {MatStepperModule} from '@angular/material/stepper';
import {MatDatepickerModule} from '@angular/material/datepicker';
import { AddVehicleResultDialogComponent } from './add-vehicle/add-vehicle-result-dialog/add-vehicle-result-dialog.component';
import {MatDialogModule} from '@angular/material/dialog';

const routes: Routes = [
  {
    path: '',
    component: VehicleListComponent,
    canActivate: [AuthGuard]
  },
  {
    path: 'vehicle-management/:id',
    component: VehicleManagementComponent,
    canActivate: [AuthGuard]
  },
  {
    path: 'add-vehicle',
    component: AddVehicleComponent,
    canActivate: [AuthGuard]
  }
]


@NgModule({
  declarations: [
    VehicleListComponent,
    VehicleComponent,
    VehicleManagementComponent,
    InspectionsComponent,
    RepairsComponent,
    FilterComponent,
    AddVehicleComponent,
    AddVehicleResultDialogComponent
  ],
  exports: [
    FilterComponent
  ],
  imports: [
    CommonModule,
    MatCardModule,
    MatTabsModule,
    RouterModule.forChild(routes),
    NgOptimizedImage,
    MatExpansionModule,
    MatFormFieldModule,
    MatInputModule,
    ReactiveFormsModule,
    MatTableModule,
    MatButtonModule,
    MatStepperModule,
    MatDatepickerModule,
    MatDialogModule
  ]
})
export class VehiclesModule { }

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
import {
  MatCell,
  MatCellDef,
  MatColumnDef,
  MatHeaderCell,
  MatHeaderRow,
  MatHeaderRowDef,
  MatRow, MatRowDef, MatTable, MatTableModule
} from '@angular/material/table';
import {MatExpansionModule} from '@angular/material/expansion';
import {MatFormFieldModule} from '@angular/material/form-field';
import {MatInputModule} from '@angular/material/input';
import {ReactiveFormsModule} from '@angular/forms';

const routes: Routes = [
  {path: '', component: VehicleListComponent},
  {path: 'vehicle-management/:id', component: VehicleManagementComponent},
]


@NgModule({
  declarations: [
    VehicleListComponent,
    VehicleComponent,
    VehicleManagementComponent,
    InspectionsComponent,
    RepairsComponent
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
  ]
})
export class VehiclesModule { }

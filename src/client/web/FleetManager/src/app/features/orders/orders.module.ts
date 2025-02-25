import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {RouterModule, Routes} from '@angular/router';
import {AuthGuard} from '../../core/guards/auth-guard';
import {MatTableModule} from '@angular/material/table';
import {MatCardModule} from '@angular/material/card';
import {MatGridListModule} from '@angular/material/grid-list';
import {MatPaginatorModule} from '@angular/material/paginator';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import {MatFormField, MatLabel, MatSuffix} from '@angular/material/form-field';
import {MatInput} from '@angular/material/input';
import {MatButton, MatIconButton} from '@angular/material/button';
import {MatDatepicker, MatDatepickerInput, MatDatepickerToggle} from '@angular/material/datepicker';
import {MatAccordion, MatExpansionModule} from '@angular/material/expansion';
import {MatAutocompleteModule} from '@angular/material/autocomplete';
import { OrderManagementFilterComponent } from './components/order-management/order-management-filter/order-management-filter.component';
import {OrderManagementComponent} from './components/order-management/order-management.component';
import {MatIconModule} from '@angular/material/icon';
import {GoogleMap, GoogleMapsModule, MapInfoWindow, MapMarker} from '@angular/google-maps';

const routes: Routes = [
  {
    path: '',
    component: OrderManagementComponent,
    canActivate: [AuthGuard]
  },
]

@NgModule({
  declarations: [
    OrderManagementComponent,
    OrderManagementFilterComponent,
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    MatTableModule,
    MatCardModule,
    MatGridListModule,
    MatPaginatorModule,
    FormsModule,
    MatFormField,
    MatInput,
    MatLabel,
    ReactiveFormsModule,
    MatButton,
    MatDatepicker,
    MatDatepickerInput,
    MatDatepickerToggle,
    MatSuffix,
    MatAccordion,
    MatExpansionModule,
    MatAutocompleteModule,
    MatIconModule,
    MatIconButton,
    GoogleMapsModule
  ]
})
export class OrdersModule { }

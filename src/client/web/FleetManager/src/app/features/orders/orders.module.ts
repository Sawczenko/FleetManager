import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {RouterModule, Routes} from '@angular/router';
import {RouteListComponent} from '../routes/route-list/route-list.component';
import {AuthGuard} from '../../core/guards/auth-guard';
import {RoutePlannerComponent} from '../routes/route-planner/route-planner.component';
import { OrderManagementComponent } from './order-management/order-management.component';
import {MatTableModule} from '@angular/material/table';

const routes: Routes = [
  {
    path: '',
    component: OrderManagementComponent,
    canActivate: [AuthGuard]
  },
]

@NgModule({
  declarations: [
    OrderManagementComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    MatTableModule
  ]
})
export class OrdersModule { }

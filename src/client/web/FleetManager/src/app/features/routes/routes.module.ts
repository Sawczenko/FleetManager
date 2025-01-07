import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouteListComponent } from './route-list/route-list.component';
import {RouterModule, Routes} from '@angular/router';
import {AuthGuard} from '../../core/guards/auth-guard';
import {MatCardModule} from '@angular/material/card';
import {FilterComponent} from './route-list/filter/filter.component';
import { RouteComponent } from './route-list/route/route.component';
import {MatFormFieldModule, MatLabel} from '@angular/material/form-field';
import {MatIconModule} from '@angular/material/icon';
import {MatDividerModule} from '@angular/material/divider';
import {MatChipsModule} from '@angular/material/chips';
import {MatDatepickerModule} from '@angular/material/datepicker';
import {MatOptionModule} from '@angular/material/core';
import {MatSelectModule} from '@angular/material/select';
import {ReactiveFormsModule} from '@angular/forms';
import {MatInputModule} from '@angular/material/input';
import {MatButtonModule} from '@angular/material/button';
import { RoutePlannerComponent } from './route-planner/route-planner.component';
import {MatStepperModule} from '@angular/material/stepper';

const routes: Routes = [
  {
    path: '',
    component: RouteListComponent,
    canActivate: [AuthGuard]
  },
  {
    path: 'planner',
    component: RoutePlannerComponent,
    canActivate: [AuthGuard]
  }
]

@NgModule({
  declarations: [
    RouteListComponent,
    FilterComponent,
    RouteComponent,
    RoutePlannerComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    MatCardModule,
    MatLabel,
    MatIconModule,
    MatDividerModule,
    MatChipsModule,
    MatFormFieldModule,
    MatDatepickerModule,
    MatOptionModule,
    MatSelectModule,
    ReactiveFormsModule,
    MatInputModule,
    MatButtonModule,
    MatStepperModule
  ]
})
export class RoutesModule { }

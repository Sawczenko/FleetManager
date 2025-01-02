import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {MainLayoutComponent} from './layouts/main-layout/main-layout.component';
import {AuthGuard} from './core/guards/auth-guard';
import {AuthenticationLayoutComponent} from './layouts/authentication-layout/authentication-layout.component';
import {HomeDashboardComponent} from './features/dashboard/home-dashboard/home-dashboard.component';

const routes: Routes = [
  {
    path: '',
    component: MainLayoutComponent,
    canActivate: [AuthGuard],
    children: [
      { path: '',
        loadChildren: () => import('./features/dashboard/dashboard.module')
          .then(m => m.DashboardModule),
      canActivate: [AuthGuard]
      },
      { path: 'test', loadChildren: () => import('./features/test/test.module')
          .then(m => m.TestModule),
        canActivate: [AuthGuard] },
      { path: 'vehicles', loadChildren: () => import('./features/vehicles/vehicles.module')
          .then(m => m.VehiclesModule),
        canActivate: [AuthGuard]}
    ]
  },
  {
    path: 'login',
    component: AuthenticationLayoutComponent,
    children:[
      { path: '', loadChildren: () => import('./features/authentication/authentication.module').then(m => m.AuthenticationModule) },
    ]
  },
  { path: '**', redirectTo: '' },
];


@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }

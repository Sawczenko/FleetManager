import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AuthenticationLayoutComponent } from './authentication-layout.component';
import {RouterModule, Routes} from '@angular/router';
import {MainLayoutComponent} from '../main-layout/main-layout.component';
import {AuthenticationModule} from '../../features/authentication/authentication.module';
import {LoginComponent} from '../../features/authentication/login/login.component';

const routes: Routes = [
  { path: '', component: LoginComponent },
]

@NgModule({
  declarations: [
    AuthenticationLayoutComponent
  ],
  imports: [
    CommonModule,
    AuthenticationModule,
    RouterModule.forRoot(routes),
  ]
})
export class AuthenticationLayoutModule { }

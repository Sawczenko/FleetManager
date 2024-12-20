import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TestComponent } from './test.component';
import {RouterModule, Routes} from '@angular/router';
import {HomeDashboardComponent} from '../dashboard/home-dashboard/home-dashboard.component';

const routes: Routes = [
  {path: '', component: TestComponent},
]

@NgModule({
  declarations: [
    TestComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(routes)
  ]
})
export class TestModule { }

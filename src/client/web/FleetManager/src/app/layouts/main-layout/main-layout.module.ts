import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MainLayoutComponent } from './main-layout.component';
import {RouterModule, Routes} from '@angular/router';
import {AppRoutingModule} from '../../app-routing.module';
import {AppModule} from "../../app.module";
import {HeaderComponent} from "./header/header.component";
import {MatIconModule} from "@angular/material/icon";
import {MatToolbarModule} from "@angular/material/toolbar";
import {MatButtonModule} from "@angular/material/button";
import {MatSidenavModule} from "@angular/material/sidenav";
import {MatListModule} from "@angular/material/list";
import {TestComponent} from "../../features/test/test.component";
import {MatAccordion, MatExpansionModule} from '@angular/material/expansion';

const routes: Routes = [
  { path: '', component: MainLayoutComponent },
]

@NgModule({
  declarations: [
    MainLayoutComponent,
    HeaderComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forRoot(routes),
    MatIconModule,
    MatToolbarModule,
    MatButtonModule,
    MatSidenavModule,
    MatListModule,
    MatAccordion,
    MatExpansionModule
  ],
  exports: [MainLayoutComponent]
})
export class MainLayoutModule { }

import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import {AuthenticationModule} from './features/authentication/authentication.module';
import {CoreModule} from './core/core.module';
import { HeaderComponent } from './layouts/main-layout/header/header.component';
import {MatToolbar} from '@angular/material/toolbar';
import {MatIcon} from '@angular/material/icon';
import {MainLayoutModule} from './layouts/main-layout/main-layout.module';
import {AuthenticationLayoutModule} from './layouts/authentication-layout/authentication-layout.module';

@NgModule({
  declarations: [
    AppComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    AuthenticationModule,
    AuthenticationLayoutModule,
    MainLayoutModule,
    CoreModule,
    MatToolbar,
    MatIcon
  ],
  providers: [
    provideAnimationsAsync()
  ],
  exports: [
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }

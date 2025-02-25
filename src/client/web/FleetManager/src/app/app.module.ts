import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import {AuthenticationModule} from './features/authentication/authentication.module';
import {CoreModule} from './core/core.module';
import {MatToolbar} from '@angular/material/toolbar';
import {MatIcon} from '@angular/material/icon';
import {MainLayoutModule} from './layouts/main-layout/main-layout.module';
import {AuthenticationLayoutModule} from './layouts/authentication-layout/authentication-layout.module';
import {provideNativeDateAdapter} from '@angular/material/core';
import {environment} from '../environments/environment';

@NgModule({
  declarations: [
    AppComponent
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
    provideAnimationsAsync(),
    provideNativeDateAdapter()
  ],
  exports: [
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }

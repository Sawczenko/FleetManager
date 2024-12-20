import {NgModule, Optional, SkipSelf} from '@angular/core';
import { CommonModule } from '@angular/common';
import {provideHttpClient, withInterceptors} from '@angular/common/http';
import {tokenInterceptor} from './interceptors/token.interceptor';
import {ApiService} from './services/api/api.service';

@NgModule({
  declarations: [],
  imports: [
    CommonModule
  ],
  providers: [
    ApiService,
    provideHttpClient(
      withInterceptors([tokenInterceptor])
    )
  ]
})
export class CoreModule {
  constructor(@Optional() @SkipSelf() parentModule: CoreModule) {
    if (parentModule) {
      throw new Error('CoreModule is already loaded. Import it in the AppModule only');
    }
  }
}

import { Component } from '@angular/core';
import {Router} from "@angular/router";
import {AuthenticationService} from '../../features/authentication/service/authentication.service';
import {GoogleMapsService} from '../../core/services/maps/google-maps.service';

@Component({
  selector: 'app-main-layout',
  standalone: false,
  templateUrl: './main-layout.component.html',
  styleUrl: './main-layout.component.css'
})
export class MainLayoutComponent {
  public menuOpened: boolean = false;

  constructor(private router: Router, private authenticationService: AuthenticationService, private googleMapsService: GoogleMapsService) {
  }

  public menuButtonClickedEventHandler($event: any){
    this.menuOpened = !this.menuOpened;
  }
  public logout(){
    this.authenticationService.logout();
    this.googleMapsService.unloadGoogleMaps();
  }
}

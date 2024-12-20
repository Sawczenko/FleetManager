import {Component, Input} from '@angular/core';
import {AuthenticationService} from '../../../features/authentication/service/authentication.service';

@Component({
  selector: 'app-side-navigation',
  standalone: false,

  templateUrl: './side-navigation.component.html',
  styleUrl: './side-navigation.component.css'
})
export class SideNavigationComponent {
  @Input() opened: boolean = false ;

  constructor(private authenticationService: AuthenticationService) {
  }

  public logout(){
    this.authenticationService.logout();
  }
}

import { Component } from '@angular/core';
import {Router} from "@angular/router";

@Component({
  selector: 'app-main-layout',
  standalone: false,
  templateUrl: './main-layout.component.html',
  styleUrl: './main-layout.component.css'
})
export class MainLayoutComponent {
  public menuOpened: boolean = false;

  constructor(private router: Router) {
  }

  public menuButtonClickedEventHandler($event: any){
    this.menuOpened = !this.menuOpened;
  }
}

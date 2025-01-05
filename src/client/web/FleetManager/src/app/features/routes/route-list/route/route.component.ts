import {Component, Input,} from '@angular/core';
import {Route} from '../models/route';

@Component({
  selector: 'app-route',
  standalone: false,
  templateUrl: './route.component.html',
  styleUrl: './route.component.css'
})
export class RouteComponent {
  @Input() route!: Route;

}

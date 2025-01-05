import {Component, Input} from '@angular/core';
import {Vehicle} from '../models/vehicle';

@Component({
  selector: 'app-vehicle',
  standalone: false,

  templateUrl: './vehicle.component.html',
  styleUrl: './vehicle.component.css'
})
export class VehicleComponent {
  @Input() vehicle!: Vehicle;

public test(){
  console.log(this.vehicle);
}
}

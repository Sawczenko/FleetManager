import {Component, EventEmitter, OnInit, Output} from '@angular/core';
import {FormBuilder, FormGroup, FormGroupDirective, Validators} from '@angular/forms';
import {VehiclesFilter} from './models/vehicles-filter';
import {Inspection} from '../../vehicle-management/models/inspection';

@Component({
  selector: 'app-filter',
  standalone: false,

  templateUrl: './filter.component.html',
  styleUrl: './filter.component.css'
})
export class FilterComponent implements OnInit{
  @Output() filterApplied = new EventEmitter<VehiclesFilter>();
  public filterForm!: FormGroup;

  constructor(private formBuilder: FormBuilder) {
  }

  ngOnInit(): void {
      this.filterForm = this.formBuilder.group({
        vin: [''],
        licensePlate: [''],
        model: [''],
      })
  }

  public applyFilter(formDirective: FormGroupDirective){
    const formValue = this.filterForm.value;
    const vehiclesFilter = new VehiclesFilter(formValue.vin, formValue.licensePlate, formValue.model);
    this.filterApplied.emit(vehiclesFilter);
  }

}

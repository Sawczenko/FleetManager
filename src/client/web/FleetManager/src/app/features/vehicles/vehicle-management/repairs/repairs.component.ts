import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {Repair} from '../models/repair';
import {FormBuilder, FormGroup, FormGroupDirective, Validators} from '@angular/forms';
import {VehicleManagementService} from '../service/vehicle-management.service';

@Component({
  selector: 'app-repairs',
  standalone: false,

  templateUrl: './repairs.component.html',
  styleUrl: './repairs.component.css'
})
export class RepairsComponent implements OnInit{
  @Input() repairs: Repair[] = [];
  @Input() vehicleId: string = '';
  @Output() repairAdded: EventEmitter<any> = new EventEmitter();
  public displayedColumns: string[] = ['date', 'description', 'cost'];
  public repairForm!: FormGroup;

  constructor(private formBuilder: FormBuilder, private vehicleManagementService: VehicleManagementService) {

  }

  ngOnInit(): void {
    this.repairForm = this.formBuilder.group({
      date: ['', Validators.required],
      description: ['', Validators.required],
      cost: ['', [Validators.required, Validators.min(0)]],
    })
  }

  public addRepair(formDirective: FormGroupDirective): void {
    if (this.repairForm.invalid) {
      return;
    }

    const formValue = this.repairForm.value;
    const repair = new Repair(this.vehicleId, formValue.date, formValue.description, formValue.cost);

    this.vehicleManagementService.addRepair(repair).subscribe({
        next: () => { this.repairAdded.emit() },
        complete: () => { formDirective.resetForm(); },
      }
    );
  }
}

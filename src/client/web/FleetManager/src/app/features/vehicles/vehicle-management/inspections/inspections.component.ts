import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {Inspection} from '../models/inspection';
import {FormBuilder, FormGroup, FormGroupDirective, Validators} from '@angular/forms';
import {VehicleManagementService} from '../service/vehicle-management.service';

@Component({
  selector: 'app-inspections',
  standalone: false,

  templateUrl: './inspections.component.html',
  styleUrl: './inspections.component.css'
})
export class InspectionsComponent implements OnInit{
  @Input() inspections: Inspection[] = [];
  @Input() vehicleId: string = '';
  @Output() inspectionAdded: EventEmitter<any> = new EventEmitter();
  public displayedColumns: string[] = ['date', 'description', 'cost'];
  public inspectionForm!: FormGroup;

  constructor(private formBuilder: FormBuilder, private vehicleManagementService: VehicleManagementService) {

  }

  ngOnInit(): void {
    this.inspectionForm = this.formBuilder.group({
      date: ['', Validators.required],
      description: ['', Validators.required],
      cost: ['', [Validators.required, Validators.min(0)]],
    })
  }

  public addInspection(formDirective: FormGroupDirective): void {
    if (this.inspectionForm.invalid) {
      return;
    }

    const formValue = this.inspectionForm.value;
    const inspection = new Inspection(this.vehicleId, formValue.date, formValue.description, formValue.cost);

    this.vehicleManagementService.addInspection(inspection).subscribe({
        next: () => {
          console.log('added')
          this.inspectionAdded.emit()
        },
        complete: () => {
          formDirective.resetForm();
        }
      }
    );
  }
}

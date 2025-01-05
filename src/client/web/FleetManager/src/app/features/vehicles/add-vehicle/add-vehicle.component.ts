import {Component, inject, OnInit, ViewChild} from '@angular/core';
import {FormBuilder, FormGroup, Validators} from '@angular/forms';
import {AddVehicleRequest} from './models/add-vehicle-request';
import {AddVehicleService} from './service/add-vehicle.service';
import {MatStepper} from '@angular/material/stepper';
import {Result} from '../../../core/services/api/models/result';
import {MatDialog} from '@angular/material/dialog';
import {AddVehicleResultDialogComponent} from './add-vehicle-result-dialog/add-vehicle-result-dialog.component';
import {ApiError} from '../../../core/services/api/models/api-error';

@Component({
  selector: 'app-add-vehicle',
  standalone: false,

  templateUrl: './add-vehicle.component.html',
  styleUrl: './add-vehicle.component.css'
})
export class AddVehicleComponent implements OnInit {

  @ViewChild('stepper') stepper!: MatStepper;
  public vehicleForm!: FormGroup;
  public locationForm!: FormGroup;
  public addVehicleRequest!: AddVehicleRequest;

  private readonly dialog = inject(MatDialog);

  constructor(private formBuilder: FormBuilder, private addVehicleService: AddVehicleService) {
  }

  ngOnInit(): void {
    this.vehicleForm = this.formBuilder.group({
      vin: ['', Validators.required],
      model: ['', Validators.required],
      licensePlate: ['', Validators.required],
      lastInspectionDate: [null, [Validators.required]],
      nextInspectionDate: [null, [Validators.required]]
    })

    this.locationForm = this.formBuilder.group({
      name: [ null, [Validators.required]],
      latitude: [ null , Validators.required],
      longitude: [ null , Validators.required],
    })
  }

  public createRequest(){
    const vehicleFormValue = this.vehicleForm.value;
    const locationFormValue = this.locationForm.value;
    this.addVehicleRequest = new AddVehicleRequest(vehicleFormValue.vin,
      vehicleFormValue.licensePlate,
      vehicleFormValue.model,
      vehicleFormValue.lastInspectionDate,
      vehicleFormValue.nextInspectionDate,
      locationFormValue.name,
      locationFormValue.longitude,
      locationFormValue.latitude)
  }

  public addVehicle(){
    this.addVehicleService.addVehicle(this.addVehicleRequest).subscribe({
      next: (result) => {
        this.stepper.reset();

        result.error!.description = 'Vehicle was added successfully';
        this.openDialog(result);
      },
      error: (err) => {
        let errorMessage: string = 'Error occurred during the request.'

        if(Result.isResult(err.error)){
          const result = err.error as Result;
          errorMessage = result.error!.description;
        }
        const result = new Result(false, new ApiError('', errorMessage));
        this.openDialog(result);
      }
    })
  }

  private openDialog(result: Result){
    const dialogRef = this.dialog.open(AddVehicleResultDialogComponent, {
      data: result,
    });

    dialogRef.afterClosed().subscribe(result => {
      if(result.isSuccess){
        this.stepper.reset();
      }
    })
  }
}

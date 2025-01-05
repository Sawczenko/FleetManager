import {Component, inject, OnInit} from '@angular/core';
import {MAT_DIALOG_DATA, MatDialogRef} from '@angular/material/dialog';
import {Result} from '../../../../core/services/api/models/result';

@Component({
  selector: 'app-add-vehicle-result-dialog',
  standalone: false,

  templateUrl: './add-vehicle-result-dialog.component.html',
  styleUrl: './add-vehicle-result-dialog.component.css'
})
export class AddVehicleResultDialogComponent {
  readonly dialogRef = inject(MatDialogRef<AddVehicleResultDialogComponent>);
  public readonly result = inject<Result>(MAT_DIALOG_DATA);

  public OkButtonClick(){
    this.dialogRef.close();
  }
}

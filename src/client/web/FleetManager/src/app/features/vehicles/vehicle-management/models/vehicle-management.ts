import {Inspection} from './inspection';
import {Repair} from './repair';

export class VehicleManagement {
  public id: string;
  public vin: string;
  public licensePlate: string;
  public model: string;
  public status: string;
  public nextInspectionDate: Date;
  public inspections: Inspection[];
  public repairs: Repair[];


  constructor(id: string, vin: string, licensePlate: string, model: string, status: string, nextInspectionDate: Date, inspections: Inspection[], repairs: Repair[]) {
    this.id = id;
    this.vin = vin;
    this.licensePlate = licensePlate;
    this.model = model;
    this.status = status;
    this.nextInspectionDate = nextInspectionDate;
    this.inspections = inspections;
    this.repairs = repairs;
  }
}

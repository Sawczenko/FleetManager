export class VehicleWithUpcomingMaintenance {
  public nextInspectionDate: Date = new Date();
  public vin: string;
  public model: string;
  public licensePlate: string;

  constructor(nextInspectionDate: Date, vin: string, model: string, licensePlate: string) {
    this.nextInspectionDate = nextInspectionDate;
    this.vin = vin;
    this.model = model;
    this.licensePlate = licensePlate;
  }
}

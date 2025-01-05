export class VehiclesFilter {
  public vin: string;
  public licensePlate: string;
  public model: string;

  constructor(vin: string, licensePlate: string, model: string) {
    this.vin = vin;
    this.licensePlate = licensePlate;
    this.model = model;
  }

}

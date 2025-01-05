export class Vehicle {
  public id: string;
  public vin: string;
  public licensePlate: string;
  public model: string;
  public status: string;

  constructor(id: string, vin: string, licensePlate: string, model: string, status: string) {
    this.id = id;
    this.vin = vin;
    this.licensePlate = licensePlate;
    this.model = model;
    this.status = status;
  }
}

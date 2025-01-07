export class Vehicle {
  public id: string;
  public vin: string;
  public model: string;

  constructor(id: string, vin: string, model: string) {
    this.id = id;
    this.vin = vin;
    this.model = model;
  }
}

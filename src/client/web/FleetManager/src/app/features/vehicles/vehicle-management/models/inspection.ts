export class Inspection{
  public vehicleId: string;
  public date: Date;
  public description: string;
  public cost: number;

  constructor(vehicleId: string, date: Date, description: string, cost: number) {
    this.vehicleId = vehicleId;
    this.date = date;
    this.description = description;
    this.cost = cost;
  }
}

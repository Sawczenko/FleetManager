export class Inspection {
  public date: Date;
  public description: string;
  public cost: number;

  constructor(date: Date, description: string, cost: number) {
    this.date = date;
    this.description = description;
    this.cost = cost;
  }
}

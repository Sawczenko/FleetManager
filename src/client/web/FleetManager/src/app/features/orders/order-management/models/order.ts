export class Order{
  public id: string;
  public contractorName: string;
  public originLocation: string;
  public destinationLocation: string;
  public pickupDate: Date;
  public deliveryDate: Date;
  public status: string;

  constructor(id: string, contractorName: string, originLocation: string, destinationLocation: string, pickupDate: Date, deliveryDate: Date, status: string) {
    this.id = id;
    this.contractorName = contractorName;
    this.originLocation = originLocation;
    this.destinationLocation = destinationLocation;
    this.pickupDate = pickupDate;
    this.deliveryDate = deliveryDate;
    this.status = status;
  }
}

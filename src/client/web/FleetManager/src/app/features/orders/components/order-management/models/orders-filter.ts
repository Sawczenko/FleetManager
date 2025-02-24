export class OrdersFilter {
  public contractorId: string;
  public originLocationId: string;
  public destinationLocationId: string;
  public pickupDateFrom: Date;
  public pickupDateTo: Date;
  public deliveryDateFrom: Date;
  public deliveryDateTo: Date;

  constructor(
    contractorId: string = '',
    originLocationId: string = '',
    destinationLocationId: string = '',
    pickupDateFrom: Date,
    pickupDateTo: Date,
    deliveryDateFrom: Date,
    deliveryDateTo: Date) {
    this.contractorId = contractorId;
    this.originLocationId = originLocationId;
    this.destinationLocationId = destinationLocationId;
    this.pickupDateFrom = pickupDateFrom;
    this.pickupDateTo = pickupDateTo;
    this.deliveryDateFrom = deliveryDateFrom;
    this.deliveryDateTo = deliveryDateTo;
  }
}

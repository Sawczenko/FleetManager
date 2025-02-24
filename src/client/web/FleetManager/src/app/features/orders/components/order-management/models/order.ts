import {ContractorInfo} from '../../../../contractors/models/contractor-info';
import {LocationInfo} from '../../../../locations/models/location-info';

export class Order{
  public id: string;
  public contractor: ContractorInfo
  public originLocation: LocationInfo;
  public destinationLocation: LocationInfo;
  public pickupDate: Date;
  public deliveryDate: Date;
  public status: string;

  constructor(id: string, contractor: ContractorInfo, originLocation: LocationInfo, destinationLocation: LocationInfo, pickupDate: Date, deliveryDate: Date, status: string) {
    this.id = id;
    this.contractor = contractor;
    this.originLocation = originLocation;
    this.destinationLocation = destinationLocation;
    this.pickupDate = pickupDate;
    this.deliveryDate = deliveryDate;
    this.status = status;
  }
}

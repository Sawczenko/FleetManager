export class AddVehicleRequest {
  public vin: string;
  public licensePlate: string;
  public model: string;
  public lastInspectionDate: Date;
  public nextInspectionDate: Date;
  public locationName: string;
  public latitude: number;
  public longitude: number;

  constructor(vin: string,
              licensePlate: string,
              model: string,
              lastInspectionDate: Date,
              nextInspectionDate: Date,
              locationName: string,
              latitude: number,
              longitude: number) {
    this.vin = vin;
    this.licensePlate = licensePlate;
    this.model = model;
    this.lastInspectionDate = lastInspectionDate;
    this.nextInspectionDate = nextInspectionDate;
    this.locationName = locationName;
    this.latitude = latitude;
    this.longitude = longitude;
  }
}

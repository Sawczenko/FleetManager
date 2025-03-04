export class LocationInfo {
  public id: string;
  public name: string;
  public latitude: number;
  public longitude: number;

  constructor(id: string = '', name: string = '', latitude: number = 0, longitude: number = 0) {
    this.id = id;
    this.name = name;
    this.latitude = latitude;
    this.longitude = longitude;
  }
}

export class RouteLocation{
  public name: string;
  public latitude: number;
  public longitude: number;

  constructor(name: string, latitude: number, longitude: number) {
    this.name = name;
    this.latitude = latitude;
    this.longitude = longitude;
  }
}

import {RouteLocation} from './location';

export class Route{
  public userName: string;
  public vehicle: string;
  public scheduledStartTime: Date;
  public endTime: Date;
  public startLocation: RouteLocation;
  public endLocation: RouteLocation;
  public status: string;

  constructor(userName: string, vehicle: string, scheduledStartTime: Date, endTime: Date, startLocation: RouteLocation, endLocation: RouteLocation, status: string) {
    this.userName = userName;
    this.vehicle = vehicle;
    this.scheduledStartTime = scheduledStartTime;
    this.endTime = endTime;
    this.startLocation = startLocation;
    this.endLocation = endLocation;
    this.status = status;
  }
}

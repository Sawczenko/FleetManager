export class AddRouteRequest {
  public userId: string;
  public vehicleId: string;
  public scheduledStartTime: Date;
  public startLocationId: string;
  public endLocationId: string;

  constructor(userId: string, vehicleId: string, scheduledStartTime: Date, startLocationId: string, endLocationId: string) {
    this.userId = userId;
    this.vehicleId = vehicleId;
    this.scheduledStartTime = scheduledStartTime;
    this.startLocationId = startLocationId;
    this.endLocationId = endLocationId;
  }
}

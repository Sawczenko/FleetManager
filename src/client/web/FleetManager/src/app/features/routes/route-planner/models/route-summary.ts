export class RouteSummary{
  public userName: string;
  public vehicleName: string;
  public scheduledStartTime: string;
  public startLocationName: string;
  public endLocationName: string;

  constructor(userName: string, vehicleName: string, scheduledStartTime: string, startLocationName: string, endLocationName: string) {
    this.userName = userName;
    this.vehicleName = vehicleName;
    this.scheduledStartTime = scheduledStartTime;
    this.startLocationName = startLocationName;
    this.endLocationName = endLocationName;
  }
}

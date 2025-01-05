export class RoutesFilter{
  userName: string;
  startLocation: string;
  endLocation: string;
  status: string;
  scheduledStartTime: Date;
  endTime: Date;

  constructor(
    userName: string,
    startLocation: string,
    endLocation: string,
    status: string,
    scheduledStartTime: Date,
    endTime: Date
  ) {
    this.userName = userName;
    this.startLocation = startLocation;
    this.endLocation = endLocation;
    this.status = status;
    this.scheduledStartTime = scheduledStartTime;
    this.endTime = endTime;
  }
}

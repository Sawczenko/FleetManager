import {VehicleWithUpcomingMaintenance} from './vehicle-with-upcoming-maintenance';

export class HomeDashboard {
  public vehiclesCountPerStatus: { [key: string]: number } = {};
  public routesCountPerStatus: { [key: string]: number } = {};
  public vehiclesWithUpcomingMaintenance: VehicleWithUpcomingMaintenance[] = [];
}

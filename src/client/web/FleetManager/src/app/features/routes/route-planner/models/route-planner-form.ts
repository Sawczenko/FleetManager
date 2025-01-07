import {User} from './user';
import {Vehicle} from './vehicle';
import {RouteLocation} from './route-location';

export class RoutePlannerForm {
  public users: User[];
  public vehicles: Vehicle[];
  public locations: RouteLocation[];

  constructor(users: User[], vehicles: Vehicle[], locations: RouteLocation[]) {
    this.users = users;
    this.vehicles = vehicles;
    this.locations = locations;
  }
}

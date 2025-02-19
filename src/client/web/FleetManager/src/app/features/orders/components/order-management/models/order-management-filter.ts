import {LocationInfo} from '../../../../locations/models/location-info';
import {ContractorInfo} from '../../../../contractors/models/contractor-info';

export class OrderManagementFilter {
  public locationsInfo: LocationInfo[];
  public contractorsInfo: ContractorInfo[];

  constructor(locationsInfo: LocationInfo[], contractorsInfo: ContractorInfo[]) {
    this.locationsInfo = locationsInfo;
    this.contractorsInfo = contractorsInfo;
  }
}

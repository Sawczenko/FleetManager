﻿using FleetManager.Domain.Contractors;
using FleetManager.Domain.Locations;

namespace FleetManager.Application.Orders.GetOrdersManagementFilter
{
    public record OrderManagementFilterDto(List<LocationInfo> LocationsInfo, List<ContractorInfo> ContractorsInfo)
    {
    }
}

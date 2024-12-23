﻿using FleetManager.Domain.Routes;

namespace FleetManager.Domain.VehicleUsages
{
    public class VehicleUsage
    {
        public Guid Id { get; private set; }
        public Guid VehicleId { get; private set; }
        public Guid UserId { get; private set; }
        public List<FuelExpense> FuelExpenses { get; private set; }
        public Guid RouteId { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime? EndDate { get; private set; }

        internal VehicleUsage(Guid vehicleId, Guid routeId, Guid userId, DateTime startDate)
        {
            Id = Guid.NewGuid();
            VehicleId = vehicleId;
            UserId = userId;
            FuelExpenses = new List<FuelExpense>();
            RouteId = routeId;
            StartDate = startDate;
            EndDate = null;
        }

        public void EndUsage(DateTime endDate)
        {
            EndDate = endDate;
        }

        public void AddFuelExpense(FuelExpense expense)
        {
            FuelExpenses.Add(expense);
        }
    }

}

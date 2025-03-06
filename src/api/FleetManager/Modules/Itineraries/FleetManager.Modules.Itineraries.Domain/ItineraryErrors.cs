using FleetManager.BuildingBlocks.Domain;

namespace FleetManager.Modules.Itineraries.Domain;

public class ItineraryErrors
{
    public static Error MissingRoute() => new Error("Itinerary.MissingRoute", "Route is missing.");

    public static Error MissingDriver() => new Error("Itinerary.MissingDriver", "Driver is missing.");

    public static Error MissingVehicle() => new Error("Itinerary.MissingVehicle", "Vehicle is missing.");

    public static Error ScheduledStartDateCannotBeInThePast() => new Error(
        "Itinerary.ScheduledStartDateCannotBeInThePast", "Scheduled start date cannot be placed in the past.");

    public static Error ScheduledStartDateCannotBeLaterThanScheduledEndDate() => new Error(
        "Itinerary.ScheduledStartDateCannotBeLaterThanScheduledEndDate",
        "Scheduled start date cannot be placed later than scheduled end date");
}
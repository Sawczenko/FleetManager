using FleetManager.Domain.SeedWork;

namespace FleetManager.Infrastructure.Authentication;

public static class AuthenticationErrors
{
    public static Error IncompleteUserData() => new Error("Authentication.IncompleteUserData", "Incomplete user data.");

    public static Error FailedUserCreation(string[] errors) =>
        new Error("Authentication.FailedUserCreation", $"Failed user creation - {string.Join(",", errors)}");
}
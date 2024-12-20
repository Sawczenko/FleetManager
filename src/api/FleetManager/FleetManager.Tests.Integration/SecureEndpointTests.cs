using FleetManager.Infrastructure.Authentication;
using FleetManager.Application.Account.Login;
using Microsoft.AspNetCore.Identity;
using System.Net.Http.Json;

namespace FleetManager.Tests.Integration
{
    internal class SecureEndpointTests : BaseIntegrationTest
    {
        protected readonly ApplicationUser TestUser;
        private const string TestUserPassword = "SecurePassword123!";

        public SecureEndpointTests(IntegrationTestWebAppFactory factory) : base(factory)
        {
            var hasher = new PasswordHasher<ApplicationUser>();

            string email = "testuser@example.com";

            TestUser = new ApplicationUser
            {
                Id = Guid.NewGuid(),
                Email = email,
                NormalizedEmail = email.ToUpper(),
                UserName = "testuser@example.com",
                PasswordHash = hasher.HashPassword(default, TestUserPassword)
            };
        }

        public override async Task InitializeAsync()
        {
            await base.InitializeAsync();
            await SeedTestUserAsync();
        }

        private async Task SeedTestUserAsync()
        {
            await DbContext.Set<ApplicationUser>().AddAsync(TestUser);
            await DbContext.SaveChangesAsync();
            await AuthenticateClientAsync(TestUser.Email, TestUserPassword);
        }

        protected async Task AuthenticateClientAsync(string? email, string password)
        {
            LoginRequestDto loginRequestRequest = new LoginRequestDto(email, password);

            var response = await HttpClient.PostAsJsonAsync("/account/login", loginRequestRequest);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Login failed with status code: {response.StatusCode}");
            }

            var loginResponse = await response.Content.ReadFromJsonAsync<LoginResponseDto>();
            if (loginResponse == null || string.IsNullOrEmpty(loginResponse.Token))
            {
                throw new Exception("Failed to retrieve JWT token.");
            }

            HttpClient.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", loginResponse.Token);
        }
    }
}

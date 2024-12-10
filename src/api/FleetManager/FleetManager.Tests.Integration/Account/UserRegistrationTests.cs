using FleetManager.Infrastructure.Authentication;
using FleetManager.Application.Account.Register;
using FleetManager.Domain.SeedWork.Results;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Json;
using FluentAssertions;
using System.Net;

namespace FleetManager.Tests.Integration.Account
{
    public class UserRegistrationTests : BaseIntegrationTest
    {
        private const string Endpoint = "/account/register";

        public UserRegistrationTests(IntegrationTestWebAppFactory factory) : base(factory)
        {
        }

        [Fact]
        public async Task RegisterUser_ShouldRegisterUser_WhenUserProvidedCorrectData()
        {
            #region Arrange

            RegisterUserRequestDto registerUserRequestDto =
                new RegisterUserRequestDto("testuser@example.com", "StrongPassword1!", "Test", "User");

            #endregion

            #region Act

            var response = await HttpClient.PostAsJsonAsync(Endpoint, registerUserRequestDto);

            var result = await response.Content.ReadFromJsonAsync<Result>();

            if (!response.IsSuccessStatusCode)
            {
                string error = string.Empty;
                if (result is not null)
                {
                    error = result.Error.Description;
                }

                Assert.Fail($"Error occurred during the request. {error}");
            }

            #endregion

            #region Assert

            var user = await DbContext.Set<ApplicationUser>()
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.Email == registerUserRequestDto.Email, CancellationToken.None);

            Assert.NotNull(user);
            user.FirstName.Should().Be(registerUserRequestDto.FirstName);
            user.LastName.Should().Be(registerUserRequestDto.LastName);

            #endregion
        }

        [Fact]
        public async Task RegisterUser_ShouldReturnIncompleteUserDataError_WhenUserProvidedIncompleteData()
        {
            #region Arrange

            RegisterUserRequestDto registerUserRequestDto =
                new RegisterUserRequestDto("testuser@example.com", "StrongPassword1!", "", "User");

            #endregion

            #region Act

            var response = await HttpClient.PostAsJsonAsync(Endpoint, registerUserRequestDto);

            var result = await response.Content.ReadFromJsonAsync<Result>();

            if (response.IsSuccessStatusCode)
            {
                Assert.Fail($"Error was expected.");
            }

            #endregion

            #region Assert

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            result.Should().NotBeNull();
            result.Error.Code.Should().Be(AuthenticationErrors.IncompleteUserData().Code);

            #endregion
        }

        [Fact]
        public async Task RegisterUser_ShouldReturnBadRequest_WhenRegistrationDtoIsNull()
        {
            #region Arrange

            RegisterUserRequestDto registerUserRequestDto = null;

            #endregion

            #region Act

            var response = await HttpClient.PostAsJsonAsync(Endpoint, registerUserRequestDto);

            if (response.IsSuccessStatusCode)
            {
                Assert.Fail($"Error was expected.");
            }

            #endregion

            #region Assert

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

            #endregion
        }
    }
}

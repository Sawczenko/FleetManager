using FleetManager.Infrastructure.Authentication;
using FleetManager.Application.Account.Login;
using FleetManager.Domain.SeedWork.Results;
using Microsoft.AspNetCore.Identity;
using System.Net.Http.Json;
using FluentAssertions;

namespace FleetManager.Tests.Integration.Account;

public class UserLoginTests : BaseIntegrationTest
{
    private const string Endpoint = "/account/login";

    public UserLoginTests(IntegrationTestWebAppFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task LoginUser_ShouldReturnToken_WhenCredentialsAreCorrect()
    {
        #region Arrange

        PasswordHasher<ApplicationUser> hasher = new PasswordHasher<ApplicationUser>();

        string userPassword = "Test";
        string userMail = "testuser@example.com";

        var user = new ApplicationUser
        {
            Id = Guid.NewGuid(),
            FirstName = "Test",
            LastName = "User",
            Email = userMail,
            NormalizedEmail = userMail.ToUpper(),
            UserName = "testuser@example.com",
            PasswordHash = hasher.HashPassword(null, userPassword)
        };

        await DbContext.Set<ApplicationUser>().AddAsync(user);
        await DbContext.SaveChangesAsync();

        LoginRequestDto loginRequestDto = new LoginRequestDto(user.Email, userPassword);

        #endregion

        #region Act
        var response = await HttpClient.PostAsJsonAsync(Endpoint, loginRequestDto);

        var result = await response.Content.ReadFromJsonAsync<Result<LoginResponseDto>>();

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

        result.Should().NotBeNull();
        result.Value.Should().NotBeNull();
        result.Value.Token.Should().NotBeNull();

        #endregion
    }

    public static IEnumerable<object[]> IncompleteUserDataTest => new List<object[]>
    {
        new object[] { "testuser@example.com", "CorrectPass" , "testuser@example.com", ""},
        new object[] { "testuser@example.com", "CorrectPass" , "", "CorrectPass"},
    };


    [Theory]
    [MemberData(nameof(IncompleteUserDataTest))]
    public async Task LoginUser_ShouldReturnIncompleteUserDataError_WhenEmailOrPasswordIsEmpty(string userEmail, string userPassword, string loginRequestEmail, string loginRequestPassword)
    {
        #region Arrange

        PasswordHasher<ApplicationUser> hasher = new PasswordHasher<ApplicationUser>();

        var user = new ApplicationUser
        {
            Id = Guid.NewGuid(),
            FirstName = "Test",
            LastName = "User",
            Email = userEmail,
            NormalizedEmail = userEmail.ToUpper(),
            UserName = "testuser@example.com",
            PasswordHash = hasher.HashPassword(null, userPassword)
        };

        await DbContext.Set<ApplicationUser>().AddAsync(user);
        await DbContext.SaveChangesAsync();

        LoginRequestDto loginRequestDto = new LoginRequestDto(loginRequestEmail, loginRequestPassword);

        #endregion

        #region Act
        var response = await HttpClient.PostAsJsonAsync(Endpoint, loginRequestDto);

        var result = await response.Content.ReadFromJsonAsync<Result<LoginResponseDto>>();

        if (response.IsSuccessStatusCode)
        {
            Assert.Fail($"Error was expected.");
        }

        #endregion

        #region Assert

        result.Should().NotBeNull();
        result.Value.Should().BeNull();
        result.Error.Should().Be(AuthenticationErrors.IncompleteUserData());

        #endregion
    }

    public static IEnumerable<object[]> InvalidEmailOrPasswordTestData => new List<object[]>
    {
        new object[] { "testuser@example.com", "CorrectPass" , "testuser@example.com", "InvalidPass"},
        new object[] { "testuser@example.com", "CorrectPass" , "invalidmail@example.com", "CorrectPass"},
    };

    [Theory]
    [MemberData(nameof(InvalidEmailOrPasswordTestData))]
    public async Task LoginUser_ShouldReturnInvalidEmailOrPasswordError_WhenEmailOrPasswordAreInvalid(string userEmail, string userPassword, string loginRequestEmail, string loginRequestPassword)
    {
        #region Arrange

        PasswordHasher<ApplicationUser> hasher = new PasswordHasher<ApplicationUser>();

        var user = new ApplicationUser
        {
            Id = Guid.NewGuid(),
            FirstName = "Test",
            LastName = "User",
            Email = userEmail,
            NormalizedEmail = userEmail.ToUpper(),
            UserName = userEmail,
            PasswordHash = hasher.HashPassword(null, userPassword)
        };

        await DbContext.Set<ApplicationUser>().AddAsync(user);
        await DbContext.SaveChangesAsync();

        LoginRequestDto loginRequestDto = new LoginRequestDto(loginRequestEmail, loginRequestPassword);

        #endregion

        #region Act
        var response = await HttpClient.PostAsJsonAsync(Endpoint, loginRequestDto);

        var result = await response.Content.ReadFromJsonAsync<Result<LoginResponseDto>>();

        if (response.IsSuccessStatusCode)
        {
            Assert.Fail($"Error was expected.");
        }

        #endregion

        #region Assert

        result.Should().NotBeNull();
        result.Value.Should().BeNull();
        result.Error.Should().Be(AuthenticationErrors.InvalidEmailOrPassword());

        #endregion
    }
}
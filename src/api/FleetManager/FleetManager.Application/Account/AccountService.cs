using FleetManager.Infrastructure.Authentication;
using FleetManager.Application.Account.Register;
using FleetManager.Domain.SeedWork.Results;
using Microsoft.AspNetCore.Identity;

namespace FleetManager.Application.Account
{
    public class AccountService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly JwtTokenService _jwtTokenService;

        public AccountService(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            JwtTokenService jwtTokenService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtTokenService = jwtTokenService;
        }


        public async Task<Result> RegisterAsync(RegisterUserRequestDto? registerRequestDto,
            CancellationToken cancellationToken)
        {
            if (registerRequestDto is null)
            {
                return Result.Failure(AuthenticationErrors.IncompleteUserData());
            }

            if (string.IsNullOrWhiteSpace(registerRequestDto.Email) ||
                string.IsNullOrWhiteSpace(registerRequestDto.FirstName) ||
                string.IsNullOrWhiteSpace(registerRequestDto.LastName) ||
                string.IsNullOrWhiteSpace(registerRequestDto.Password))
            {
                return Result.Failure(AuthenticationErrors.IncompleteUserData());
            }

            ApplicationUser applicationUser = new()
            {
                UserName = registerRequestDto.Email,
                Email = registerRequestDto.Email,
                FirstName = registerRequestDto.FirstName,
                LastName = registerRequestDto.LastName
            };

            var creationResult = await _userManager.CreateAsync(applicationUser, registerRequestDto.Password);

            if (!creationResult.Succeeded)
            {
                return Result.Failure(AuthenticationErrors.FailedUserCreation(creationResult.Errors.Select(x => x.Description).ToArray()));
            }

            return Result.Success();
        }
    }
}

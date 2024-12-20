using FleetManager.Application.Account.Register;
using FleetManager.Application.Account.Login;
using FleetManager.Domain.SeedWork.Results;
using FleetManager.Application.Account;
using Microsoft.AspNetCore.Mvc;

namespace FleetManager.API.Controllers;

[ApiController]
[Route("[controller]")]
public class AccountController : ControllerBase
{
    private readonly AccountService _accountService;

    public AccountController(AccountService accountService)
    {
        _accountService = accountService;
    }

    [HttpPost("register")]
    public async Task<ActionResult<Result>> RegisterAsync([FromBody] RegisterUserRequestDto registerUserRequestDto, CancellationToken cancellationToken)
    {
        var result = await _accountService.RegisterAsync(registerUserRequestDto, cancellationToken);

        if (result.IsFailure)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }

    [HttpPost("login")]
    public async Task<ActionResult<Result<LoginResponseDto>>> LoginAsync([FromBody] LoginRequestDto loginRequestDto,
        CancellationToken cancellationToken)
    {
        var result = await _accountService.LoginAsync(loginRequestDto, cancellationToken);

        if (result.IsFailure)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }
}
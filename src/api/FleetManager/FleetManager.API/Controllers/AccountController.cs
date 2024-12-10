using FleetManager.Application.Account.Register;
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
    public async Task<IActionResult> RegisterAsync([FromBody] RegisterUserRequestDto registerUserRequestDto, CancellationToken cancellationToken)
    {
        var result = await _accountService.RegisterAsync(registerUserRequestDto, cancellationToken);

        if (result.IsFailure)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }
}
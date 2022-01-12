using DriverLicensePracticerApi.Models;
using DriverLicensePracticerApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace DriverLicensePracticerApi.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {

            private readonly IAccountService _accountService;
            public AccountController(IAccountService service)
            {
                _accountService = service;
            }
            [HttpPost("register")]
            public ActionResult RegisterUser([FromBody] RegisterUserDto dto)
            {
                _accountService.RegisterUser(dto);
                return Ok(dto);
            }

            [HttpPost("login")]
            public ActionResult Login([FromBody] LoginUserDto loginDto)
            {
                var token = _accountService.GenerateJwt(loginDto);
                return Ok(token);
            }
        
    }
}

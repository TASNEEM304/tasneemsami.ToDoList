using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TasneemSami.ToDoList.Api;
using TasneemSami.ToDoList.Api.Controllers;
using TasneemSami.ToDoList.Api.Helper;
using TasneemSami.ToDoList.Services.UserServices;

[ApiController]
[Route("api/[controller]")]
public class AuthController :  BaseController
{
    private readonly IAuthService _AuthService;
    private readonly JwtService _jwtService;
    private readonly IHosException _HosException;

    public AuthController(IAuthService AuthService, JwtService jwtService, IHosException HosException) :base(HosException)
    {
        _AuthService = AuthService;
        _jwtService = jwtService;
        _HosException = HosException;
    }

    
    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login(LoginRequest input)
    {
        var user = await _AuthService.ValidateUserAsync(input);
        if (user == null)
            return Unauthorized();

        var token = _jwtService.GenerateToken(user);
        return Ok(new { token });
    }

 
    [HttpPost("register")]
    [Authorize(Policy = "AdminOnly")]
    public async Task<IActionResult> Register(UserInsertAndUpdateDto input)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        await _AuthService.RegisterAsync(input);
        return Ok("User registered successfully");
    }
}

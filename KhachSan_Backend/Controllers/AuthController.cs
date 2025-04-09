using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly AuthService _authService;
    private readonly ILogger<AuthController> _logger;

    public AuthController(AuthService authService, ILogger<AuthController> logger)
    {
        _authService = authService;
        _logger = logger;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        try
        {
            var response = await _authService.Register(request);
            return Ok(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Registration failed");
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        try
        {
            var response = await _authService.Login(request);
            return Ok(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Login failed");
            return BadRequest(new { message = ex.Message });
        }
    }
}
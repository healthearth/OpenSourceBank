//
[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly AuthService _auth;
    private readonly TokenService _token;

    public AuthController(AuthService auth, TokenService token)
    {
        _auth = auth;
        _token = token;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(LoginRequest request)
    {
        var user = await _auth.Register(request.Email, request.Password);
        return Ok(user.Id);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var user = await _auth.Validate(request.Email, request.Password);

        if (user == null)
            return Unauthorized();

        var token = _token.Generate(user.Email);

        return Ok(new { token });
    }
}

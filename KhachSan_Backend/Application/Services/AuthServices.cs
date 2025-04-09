using System.Security.Cryptography;
using System.Text;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
public class AuthService
{
    private readonly DatabaseContext _dbContext;
    private readonly IConfiguration _configuration;
    private readonly ILogger<AuthService> _logger;

    public AuthService(DatabaseContext dbContext, IConfiguration configuration, ILogger<AuthService> logger)
    {
        _dbContext = dbContext;
        _configuration = configuration;
        _logger = logger;
    }

    public async Task<AuthResponse> Register(RegisterRequest request)
    {
        try
        {
            //var passwordHash = HashPassword(request.Password);

            var user = new User
            {
                UserName = request.UserName,
                Email = request.Email,
                PasswordHash = request.Password,//passwordHash,
                Phone = request.Phone,
                FirstName = request.FirstName,
                LastName = request.LastName,
                //CreatedDate = DateTime.UtcNow,
                //UpdatedAt = DateTime.UtcNow
            };

            var createdUser = await _dbContext.CreateUser(user);

            if (createdUser == null)
            {
                throw new Exception("User creation failed");
            }

            var accessToken = GenerateJwtToken(createdUser);
            var refreshToken = GenerateRefreshToken();

            //await _dbContext.UpdateUserRefreshToken(createdUser.UserId, refreshToken);

            return new AuthResponse
            {
                UserName = createdUser.UserName,
                Id = createdUser.UserId,
                Email = createdUser.Email,
                Phone = createdUser.Phone,
                FirstName = createdUser.FirstName,
                LastName = createdUser.LastName,
                AccessToken = accessToken
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error during registration");
            throw;
        }
    }

    public async Task<AuthResponse> Login(LoginRequest request)
    {
        try
        {
            //var passwordHash = HashPassword(request.Password);
            var user = await _dbContext.AuthenticateUser(request.Email, request.Password);//passwordHash);

            if (user == null)
            {
                throw new Exception("Invalid email or password");
            }

            if (user.IsActive == false)
            {
                throw new Exception("Account is disabled");
            }

            var accessToken = GenerateJwtToken(user);
            var refreshToken = GenerateRefreshToken();

            //await _dbContext.UpdateUserRefreshToken(user.UserId, refreshToken);

            return new AuthResponse
            {
                Id = user.UserId,
                Email = user.Email,
                Phone = user.Phone,
                FirstName = user.FirstName,
                LastName = user.LastName,
                //Avatar = user.Avatar,
                //RefreshToken = refreshToken,
                AccessToken = accessToken
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error during login");
            throw;
        }
    }

    private string HashPassword(string password)
    {
        using (var sha256 = SHA256.Create())
        {
            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(hashedBytes);
        }
    }

    private string GenerateJwtToken(User user)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.UserId.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}")
        };

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(Convert.ToInt32(_configuration["Jwt:ExpiryMinutes"])),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private string GenerateRefreshToken()
    {
        var randomNumber = new byte[32];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
    }
}
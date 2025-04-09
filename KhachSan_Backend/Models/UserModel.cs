public class User
{
    public int UserId { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public string Phone { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public bool? IsActive { get; set; }
    public DateTime? LastLogin { get; set; }
    //public int? HotelId { get; set; }
    public DateTime? CreatedDate { get; set; }
    //public DateTime? UpdatedAt { get; set; }
}

public class LoginRequest
{
    public string Email { get; set; }
    public string Password { get; set; }
}

public class RegisterRequest 
{
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Phone { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
}

public class AuthResponse
{
    public string UserName { get; set; }
    public int Id { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    //public string Avatar { get; set; }
    //public string RefreshToken { get; set; }
    public string AccessToken { get; set; }
}
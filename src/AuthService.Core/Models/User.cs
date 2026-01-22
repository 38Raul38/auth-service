namespace AuthService.Core.Models;

public class User
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string  FullName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public bool IsConfirmed { get; set; } =  false;

    public string?  RefreshToken { get; set; } 
    public DateTime? RefreshTokenExpiryTime { get; set; }
    public ICollection<Role> Roles  { get; set; }
}
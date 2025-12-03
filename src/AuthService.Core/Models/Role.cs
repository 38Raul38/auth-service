namespace AuthService.Core.Models;

public class Role
{
    public int Id { get; set; }
<<<<<<< HEAD
    public string Name { get; set; }
=======
    public required string Name { get; set; }
>>>>>>> 65d69c9ca735abdf7fd91f28c094b5514ed5658d

    public ICollection<User> Users { get; set; }
}
using System.ComponentModel.DataAnnotations.Schema;

[Table("staff")]
public class Staff
{
    [Column("id")]
    public Guid Id { get; set; } = Guid.CreateVersion7();

    [Column("username")]
    public required string Username {get; set;}

    [Column("first_name")]
    public required string FirstName { get; set; }

    [Column("last_name")]
    public required string LastName { get; set; }

    [Column("email")]
    public required string Email { get; set; }

    [Column("password_hash")]
    public string PasswordHash { get; set; } = string.Empty;

    [Column("token_hash")]
    public string? TokenHash { get; set; }
}
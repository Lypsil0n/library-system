
using System.ComponentModel.DataAnnotations.Schema;

[Table("library_card")]
public class LibraryCard
{
    [Column("id")]
    public Guid Id { get; set; } = Guid.CreateVersion7();

    [Column("ssn")]  
    public required string Ssn { get; set; }

    [Column("first_name")]
    public required string FirstName { get; set; }

    [Column("last_name")]
    public required string LastName { get; set; }

    [Column("email")]
    public required string Email { get; set; }

    [Column("home_address")]
    public required string HomeAddress { get; set; }

    [Column("postal_code")]
    public required string PostalCode { get; set; }

    [Column("city")]
    public required string City { get; set; }

    [Column("active")]
    public required bool Active { get; set; }

    [Column("current_loan_amount")]
    public int CurrentLoanAmount { get; set; } = 0;

    [Column("pin_code")]
    public required int PinCode { get; set; }
    
}
using System.ComponentModel.DataAnnotations.Schema;

[Table("book")]
public class Book
{
    [Column("id")]
    public Guid Id { get; set; } = Guid.CreateVersion7();

    [Column("isbn")]
    public required string Isbn {get; set;}

    [Column("title")]
    public required string Title {get; set;}

    [Column("author")]
    public required string Author {get; set;}

    [Column("publisher")]
    public required string Publisher {get; set;}

    [Column("pages")]
    public required int Pages {get; set;}

    [Column("lang")]
    public required string Lang {get; set;}

    [Column("release_date")]
    public required DateOnly ReleaseDate {get; set;}

    [Column("amount_available")]
    public required int AmountAvailable {get; set;}
}
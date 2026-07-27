using System.ComponentModel.DataAnnotations.Schema;

[Table("loan")]
public class Loan
{
    [Column("id")]
    public Guid Id { get; set; } = Guid.CreateVersion7();

    [Column("library_card_id")]
    public Guid LibraryCardId {get; set;}

    [Column("book_id")]
    public Guid BookId {get; set;}

    public Loan(Guid libraryCardId, Guid bookId)
    {
        LibraryCardId = libraryCardId;
        BookId = bookId;
    }
}
using Microsoft.EntityFrameworkCore;
using server.Data;

namespace server.Data;

public class AppDbContext : DbContext
{
    public DbSet<LibraryCard> LibraryCards {get; set;}
    public DbSet<Book> Books {get; set;}

    public DbSet<Loan> Loans {get; set;}

    public DbSet<Staff> Staff {get; set;}

    public AppDbContext(DbContextOptions<AppDbContext> options) 
        : base(options)
    {
    }
}
using Microsoft.EntityFrameworkCore;
using server.Data;

namespace server.Data;

public class AppDbContext : DbContext
{
    public DbSet<LibraryCard> LibraryCards {get; set;}
    public DbSet<Book> Books {get; set;}

    public AppDbContext(DbContextOptions<AppDbContext> options) 
        : base(options)
    {
    }
}
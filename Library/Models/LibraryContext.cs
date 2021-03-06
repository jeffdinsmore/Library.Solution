using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Library.Models
{
  public class LibraryContext : IdentityDbContext<Patron>
  {
    public virtual DbSet<Book> Books { get; set; }
    public virtual DbSet<Catalog> Catalogs { get; set; }
    public virtual DbSet<Author> Authors { get; set; }
    public virtual DbSet<BookCopy> BookCopies { get; set; }
    public virtual DbSet<Checkout> Checkouts { get; set; }
    public DbSet<AuthorBookCatalog> AuthorBookCatalog { get; set; }
    public LibraryContext(DbContextOptions options) : base(options) { }
  }
}
using BookStorePatika.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookStorePatika.DBOperations
{
    public interface IBookStoreDbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<User> Users { get; set; }

        int SaveChanges();
    }
}

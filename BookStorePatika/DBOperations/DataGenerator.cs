using BookStorePatika.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace BookStorePatika.DBOperations
{
    public class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new BookStoreDbContext(serviceProvider.GetRequiredService<DbContextOptions<BookStoreDbContext>>()))
            {
                if (context.Books.Any())
                {
                    return;
                }

                context.Authors.AddRange
                (
                   new Author()
                   {
                       Name="Yasin",
                       Surname="Gülcü",
                       DateOfBirth=DateTime.Now,
                   },

                   new Author()
                   {
                       Name = "Sümeyra",
                       Surname = "Gülcü",
                       DateOfBirth = DateTime.Now,
                   },

                   new Author()
                   {
                       Name = "Eftal",
                       Surname = "Gülcü",
                       DateOfBirth = DateTime.Now,
                   }
                );

                context.Genres.AddRange
                (
                   new Genre()
                   {
                       Name = "Personal Growth"
                   },

                   new Genre()
                   {
                       Name = "Science Fiction"
                   },

                   new Genre()
                   {
                       Name = "Romance"
                   }
                );

                context.Books.AddRange
                (
                   new Book()
                   {
                       //Id = 1,
                       Title = "Learn Startup",
                       GenreId = 1,
                       AuthorId = 1,
                       PageCount = 200,
                       PublishDate = new DateTime(2001, 06, 12),
                       IsPublished=true
                   },

                   new Book()
                   {
                       //Id = 2,
                       Title = "Herland",
                       GenreId = 2,
                       AuthorId = 2,
                       PageCount = 250,
                       PublishDate = new DateTime(2010, 05, 23),
                       IsPublished=true
                   },

                   new Book()
                   {
                       //Id = 3,
                       Title = "Dune",
                       GenreId = 2,
                       AuthorId = 3,
                       PageCount = 540,
                       PublishDate = new DateTime(2001, 12, 21),
                       IsPublished=false
                   }

                );

                context.SaveChanges();
            }
        }
    }
}

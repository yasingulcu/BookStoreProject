using BookStorePatika.DBOperations;
using BookStorePatika.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStorePatika.Application.GenreOperations.Commands.DeleteGenre
{
    public class DeleteGenreCommand
    {
        private readonly BookStoreDbContext _context;

        public int GenreId { get; set; }

        public DeleteGenreCommand(BookStoreDbContext context)
        {
            _context = context;
        }

        public void Handle()
        {
            Genre genre = _context.Genres.FirstOrDefault(x => x.Id == GenreId);

            if (genre == null)
            {
                throw new InvalidOperationException("Kitap Türü Bulunamadı");
            }

            _context.Genres.Remove(genre);

            _context.SaveChanges();
        }
    }
}

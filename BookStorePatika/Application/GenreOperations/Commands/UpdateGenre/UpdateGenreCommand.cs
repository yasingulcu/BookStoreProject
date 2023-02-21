using BookStorePatika.DBOperations;
using BookStorePatika.Entities;
using System;
using System.Linq;

namespace BookStorePatika.Application.GenreOperations.Commands.UpdateGenre
{
    public class UpdateGenreCommand
    {
        public int GenreId { get; set; }
        public UpdateGenreViewModel Model { get; set; }

        private readonly BookStoreDbContext _context;

        public UpdateGenreCommand(BookStoreDbContext context)
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

            if (_context.Genres.Any(x => x.Name.ToLower() == Model.Name.ToLower() && x.Id != GenreId))
            {
                throw new InvalidOperationException("Aynı İsimli Bir Kitap Türü Zaten Mevcut");
            }

            genre.Name = string.IsNullOrWhiteSpace(Model.Name.Trim()) ? genre.Name : Model.Name;
            genre.IsActive = Model.IsActive;
            _context.SaveChanges();
        }

        public class UpdateGenreViewModel
        {
            public string Name { get; set; }
            public bool IsActive { get; set; } = true;
        }
    }
}

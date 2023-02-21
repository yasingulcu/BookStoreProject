using BookStorePatika.DBOperations;
using System;
using System.Linq;

namespace BookStorePatika.Application.Commands.BookOperations.UpdateBook
{
    public class UpdateBookCommand
    {
        private readonly BookStoreDbContext _context;

        public int BookId { get; set; }
        public UpdateBookViewModel Model { get; set; }

        public UpdateBookCommand(BookStoreDbContext context)
        {
            _context = context;
        }

        public void Handle()
        {
            var book = _context.Books.FirstOrDefault(x => x.Id == BookId);

            if (book == null)
            {
                throw new InvalidOperationException("Kitap Bulunamadı");
            }

            book.Title = Model.Title != default ? Model.Title : book.Title;
            book.GenreId = Model.GenreId != default ? Model.GenreId : book.GenreId;
            book.IsPublished = Model.IsPublished != default ? Model.IsPublished : book.IsPublished;

            _context.SaveChanges();
        }

        public class UpdateBookViewModel
        {
            public string Title { get; set; }
            public int GenreId { get; set; }
            public bool IsPublished { get; set; }
        }
    }
}

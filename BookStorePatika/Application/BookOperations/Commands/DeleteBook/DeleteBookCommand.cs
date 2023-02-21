using BookStorePatika.DBOperations;
using BookStorePatika.Entities;
using System;
using System.Linq;

namespace BookStorePatika.Application.Commands.BookOperations.DeleteBook
{
    public class DeleteBookCommand
    {
        private readonly BookStoreDbContext _context;

        public int BookId { get; set; }

        public DeleteBookCommand(BookStoreDbContext context)
        {
            _context = context;
        }

        public void Handle()
        {
            Book book = _context.Books.FirstOrDefault(x => x.Id == BookId);

            if (book == null)
            {
                throw new InvalidOperationException("Kitap Bulunamadı");
            }

            _context.Books.Remove(book);

            _context.SaveChanges();
        }
    }
}

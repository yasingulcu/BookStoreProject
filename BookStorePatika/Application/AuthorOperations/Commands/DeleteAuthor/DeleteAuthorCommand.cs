using BookStorePatika.DBOperations;
using BookStorePatika.Entities;
using System;
using System.Linq;

namespace BookStorePatika.Application.AuthorOperations.Commands.DeleteAuthor
{
    public class DeleteAuthorCommand
    {
        private readonly IBookStoreDbContext _context;

        public int AuthorId { get; set; }

        public DeleteAuthorCommand(IBookStoreDbContext context)
        {
            _context = context;
        }

        public void Handle()
        {
            Book authorBook = (from ab in _context.Authors.Where(aut => aut.Id == AuthorId)
                               from b in _context.Books.Where(x => x.AuthorId == ab.Id)
                               select b).FirstOrDefault();

            if(authorBook == null)
            {
                throw new InvalidOperationException("Yazarın Kitabı Bulunamadı");
            }

            if (authorBook.IsPublished)
            {
                throw new InvalidOperationException("Yazarın Kitabı Yayında Olduğu İçin Silinemez..");
            }

            Author author = _context.Authors.FirstOrDefault(x => x.Id == AuthorId);

            

            if (author == null)
            {
                throw new InvalidOperationException("Yazar Bulunamadı");
            }

            _context.Authors.Remove(author);

            _context.SaveChanges();
        }
    }
}

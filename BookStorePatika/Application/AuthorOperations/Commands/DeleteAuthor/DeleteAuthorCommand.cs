using BookStorePatika.DBOperations;
using BookStorePatika.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStorePatika.Application.AuthorOperations.Commands.DeleteAuthor
{
    public class DeleteAuthorCommand
    {
        private readonly BookStoreDbContext _context;

        public int AuthorId { get; set; }

        public DeleteAuthorCommand(BookStoreDbContext context)
        {
            _context = context;
        }

        public void Handle()
        {
            Book authorBook = (from ab in _context.Authors.Where(aut => aut.Id == AuthorId)
                               from b in _context.Books.Where(x => x.AuthorId == ab.Id)
                               select b).FirstOrDefault();

            Author author = _context.Authors.FirstOrDefault(x => x.Id == AuthorId);

            if (authorBook.IsPublished)
            {
                throw new InvalidOperationException("Yazarın Kitabı Yayında Olduğu İçin Silinemez..");
            }

            if (author == null)
            {
                throw new InvalidOperationException("Yazar Bulunamadı");
            }

            _context.Authors.Remove(author);

            _context.SaveChanges();
        }
    }
}

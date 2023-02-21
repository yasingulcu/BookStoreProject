using BookStorePatika.DBOperations;
using BookStorePatika.Entities;
using System;
using System.Linq;

namespace BookStorePatika.Application.AuthorOperations.Commands.UpdateAuthor
{
    public class UpdateAuthorCommand
    {
        public int AuthorId { get; set; }
        public UpdateAuthorViewModel Model { get; set; }

        private readonly BookStoreDbContext _context;

        public UpdateAuthorCommand(BookStoreDbContext context)
        {
            _context = context;
        }


        public void Handle()
        {
            Author author = _context.Authors.FirstOrDefault(x => x.Id == AuthorId);

            if (author == null)
            {
                throw new InvalidOperationException("Yazar Bulunamadı");
            }

            if (author.FullName == Model.FullName)
            {
                throw new InvalidOperationException("Aynı İsimli Bir Yazar Zaten Mevcut");
            }

            author.Name = string.IsNullOrWhiteSpace(Model.Name.Trim()) ? author.Name : Model.Name;
            author.Surname = string.IsNullOrWhiteSpace(Model.Surname.Trim()) ? author.Surname : Model.Surname;
            _context.SaveChanges();
        }

        public class UpdateAuthorViewModel
        {
            public string Name { get; set; }
            public string Surname { get; set; }
            public string FullName => $"{Name} {Surname}";
        }
    }
}


using System;
using System.Linq;
using AutoMapper;
using BookStorePatika.DBOperations;
using BookStorePatika.Entities;


namespace BookStorePatika.Application.Commands.BookOperations.CreateBook
{
    public class CreateBookCommand
    {
        public CreateBookModel Model { get; set; }

        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;

        public CreateBookCommand(IBookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void Handle()
        {
            var book = _context.Books.Where(x => x.Title == Model.Title).FirstOrDefault();

            if (book != null)
            {
                throw new InvalidOperationException("Kitap zaten mevcut.");
            }

            book = _mapper.Map<Book>(Model);

            _context.Books.Add(book);
            _context.SaveChanges();
        }

        public class CreateBookModel
        {
            public string Title { get; set; }
            public int PageCount { get; set; }
            public DateTime PublishDate { get; set; }
            public int GenreId { get; set; }
            public bool IsPublished { get; set; }
        }
    }
}

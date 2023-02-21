using AutoMapper;
using BookStorePatika.DBOperations;
using BookStorePatika.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace BookStorePatika.Application.Queries.BookOperations.GetBookDetail
{
    public class GetBookDetailQuery
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public int BookId { get; set; }

        public GetBookDetailQuery(BookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public BookDetailViewModel Handle()
        {
            Book book = _context.Books.Include(x => x.Genre).Include(y => y.Author).Where(x => x.Id == BookId).FirstOrDefault();

            if (book is null)
            {
                throw new InvalidOperationException("Kitap Bulunamadı");
            }

            return _mapper.Map<BookDetailViewModel>(book);

        }
    }

    public class BookDetailViewModel
    {
        public string Title { get; set; }
        public string Genre { get; set; }
        public string Author { get; set; }
        public int PageCount { get; set; }
        public string PublishDate { get; set; }
        public bool IsPublished { get; set; }
    }
}

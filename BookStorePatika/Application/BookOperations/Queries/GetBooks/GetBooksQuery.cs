using AutoMapper;
using BookStorePatika.DBOperations;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace BookStorePatika.Application.BookOperations.GetBooks
{
    public class GetBooksQuery
    {
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;

        public GetBooksQuery(IBookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<BooksViewModel> Handle()
        {
            var bookList = _context.Books.Include(g => g.Author).Include(x => x.Genre).OrderBy(x => x.Id).ToList();

            List<BooksViewModel> viewModels = _mapper.Map<List<BooksViewModel>>(bookList);

            return viewModels;
        }

    }

    public class BooksViewModel
    {
        public string Title { get; set; }
        public int PageCount { get; set; }
        public string PublishDate { get; set; }
        public string Genre { get; set; }
        public string Author { get; set; }
        public bool IsPublished { get; set; }
    }
}


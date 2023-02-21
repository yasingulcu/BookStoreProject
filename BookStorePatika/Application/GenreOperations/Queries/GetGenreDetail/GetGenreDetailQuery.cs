using AutoMapper;
using BookStorePatika.DBOperations;
using BookStorePatika.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStorePatika.Application.GenreOperations.Queries.GetGenreDetail
{
    public class GetGenreDetailQuery
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public int GenreId { get; set; }

        public GetGenreDetailQuery(BookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public GenreDetailViewModel Handle()
        {
            Genre genre = _context.Genres.FirstOrDefault(x => x.IsActive && x.Id == GenreId);

            if (genre is null)
            {
                throw new InvalidOperationException("Kitap Türü Bulunamadı");
            }

            return _mapper.Map<GenreDetailViewModel>(genre);
        }

        public class GenreDetailViewModel
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }
    }
}

using AutoMapper;
using BookStorePatika.DBOperations;
using BookStorePatika.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStorePatika.Application.AuthorOperations.Queries.GetAuthorsDetail
{
    public class GetAuthorDetailQuery
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public int AuthorId { get; set; }

        public GetAuthorDetailQuery(BookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public AuthorDetailViewModel Handle()
        {
            Author author = _context.Authors.FirstOrDefault(x => x.Id == AuthorId);

            if (author is null)
            {
                throw new InvalidOperationException("Yazar Bulunamadı");
            }

            return _mapper.Map<AuthorDetailViewModel>(author);
        }

        public class AuthorDetailViewModel
        {
            public int Id { get; set; }
            public string FullName { get; set; }
            public DateTime DateOfBirth { get; set; }
        }
    }
}

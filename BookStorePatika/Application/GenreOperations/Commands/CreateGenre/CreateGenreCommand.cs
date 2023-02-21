using AutoMapper;
using BookStorePatika.DBOperations;
using BookStorePatika.Entities;
using System;
using System.Linq;

namespace BookStorePatika.Application.GenreOperations.Commands.CreateGenre
{
    public class CreateGenreCommand
    {
        public CreateGenreModel Model { get; set; }

        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public CreateGenreCommand(BookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void Handle()
        {
            Genre genre = _context.Genres.FirstOrDefault(x => x.Name == Model.Name);

            if (genre != null)
            {
                throw new InvalidOperationException("Kitap Türü Zaten Mevcut");
            }

            genre = _mapper.Map<Genre>(Model);

            _context.Genres.Add(genre);
            _context.SaveChanges();
        }

        public class CreateGenreModel
        {
            public string Name { get; set; }
        }
    }
}

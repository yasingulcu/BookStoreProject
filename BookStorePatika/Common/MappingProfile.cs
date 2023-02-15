using AutoMapper;
using BookStorePatika.BookOperations.GetBookDetail;
using BookStorePatika.BookOperations.GetBooks;
using BookStorePatika.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static BookStorePatika.BookOperations.CreateBook.CreateBookCommand;
using static BookStorePatika.BookOperations.UpdateBook.UpdateBookCommand;

namespace BookStorePatika.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateBookModel, Book>();
            CreateMap<Book, BooksViewModel>().ForMember(dest => dest.PublishDate, opt => opt.MapFrom(src => src.PublishDate.ToString("dd/MM/yyyy"))).ForMember(dest => dest.Genre, opt => opt.MapFrom(src => ((GenreEnum)src.GenreId).ToString()));
            CreateMap<Book, BookDetailViewModel>().ForMember(dest => dest.PublishDate, opt => opt.MapFrom(src => src.PublishDate.ToString("dd/MM/yyyy"))).ForMember(dest => dest.Genre, opt => opt.MapFrom(src => ((GenreEnum)src.GenreId).ToString()));
        }
    }
}

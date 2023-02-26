using AutoMapper;
using BookStorePatika.Application.AuthorOperations.Queries.GetAuthors;
using BookStorePatika.Application.BookOperations.GetBooks;
using BookStorePatika.Application.GenreOperations.Queries.GetGenres;
using BookStorePatika.Application.Queries.BookOperations.GetBookDetail;
using BookStorePatika.Entities;
using static BookStorePatika.Application.AuthorOperations.Commands.CreateAuthor.CreateAuthorCommand;
using static BookStorePatika.Application.AuthorOperations.Queries.GetAuthorsDetail.GetAuthorDetailQuery;
using static BookStorePatika.Application.Commands.BookOperations.CreateBook.CreateBookCommand;
using static BookStorePatika.Application.Commands.UserOperations.CreateUser.CreateUserCommand;
using static BookStorePatika.Application.GenreOperations.Commands.CreateGenre.CreateGenreCommand;
using static BookStorePatika.Application.GenreOperations.Queries.GetGenreDetail.GetGenreDetailQuery;

namespace BookStorePatika.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateBookModel, Book>();
            CreateMap<Book, BooksViewModel>().ForMember(dest => dest.PublishDate, opt => opt.MapFrom(src => src.PublishDate.ToString("dd/MM/yyyy"))).ForMember(aut => aut.Author, opt => opt.MapFrom(src => src.Author.FullName)).ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.Name));
            CreateMap<Book, BookDetailViewModel>().ForMember(dest => dest.PublishDate, opt => opt.MapFrom(src => src.PublishDate.ToString("dd/MM/yyyy"))).ForMember(dest => dest.Author, opt => opt.MapFrom(src => src.Author.FullName)).ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.Name));

            CreateMap<CreateGenreModel, Genre>();
            CreateMap<Genre, GenresViewModel>();
            CreateMap<Genre, GenreDetailViewModel>();

            CreateMap<CreateAuthorModel, Author>();
            CreateMap<Author, AuthorsViewModel>().ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => src.DateOfBirth.ToString("dd/MM/yyyy")));
            CreateMap<Author, AuthorDetailViewModel>().ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => src.DateOfBirth.ToString("dd/MM/yyyy")));


            CreateMap<CreateUserModel, User>();
        }
    }
}

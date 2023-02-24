using AutoMapper;
using BookStore.UnitTests.TestSetup;
using BookStorePatika.Application.Commands.BookOperations.UpdateBook;
using BookStorePatika.DBOperations;
using FluentAssertions;
using System;
using System.Linq;
using Xunit;
using static BookStorePatika.Application.Commands.BookOperations.UpdateBook.UpdateBookCommand;

namespace BookStore.UnitTests.Application.BookOperations.Commands.UpdateBook
{
    public class UpdateBookCommandTest : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public UpdateBookCommandTest(CommonTestFixture commonTestFixture)
        {
            _context = commonTestFixture.Context;
            _mapper = commonTestFixture.Mapper;
        }

        [Theory]
        [InlineData(5)]
        [InlineData(4)]
        [InlineData(-2)]
        public void WhenGivenBookIdIsNotExist_InvalidOperationException_ShouldBeReturnErrors(int id)
        {
            // Arrange
            UpdateBookCommand command = new UpdateBookCommand(_context);
            command.BookId = id;
            command.Model = new UpdateBookViewModel();

            // Act and Assert
            FluentActions.Invoking(() => command.Handle()).Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap Bulunamadı");
        }

        [Fact]
        public void WhenValidInputsAreGiven_Book_ShouldBeUpdated()
        {
            // Arrange (preparation)
            int bookId = 1;
            UpdateBookCommand command = new UpdateBookCommand(_context);
            UpdateBookViewModel model = new UpdateBookViewModel() { Title = "Atomik Alışkanlıklar",  GenreId = 1 };
            command.Model = model;
            command.BookId = bookId;

            // Act
            FluentActions.Invoking(() => command.Handle()).Invoke();

            // Assert
            var book = _context.Books.SingleOrDefault(x => x.Id == bookId);

            book.Should().NotBeNull();
            book.GenreId.Should().Be(model.GenreId);
        }
    }
}

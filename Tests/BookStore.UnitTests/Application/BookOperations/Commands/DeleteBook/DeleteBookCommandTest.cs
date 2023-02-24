using BookStore.UnitTests.TestSetup;
using BookStorePatika.Application.Commands.BookOperations.DeleteBook;
using BookStorePatika.DBOperations;
using FluentAssertions;
using System;
using System.Linq;
using Xunit;

namespace BookStore.UnitTests.Application.BookOperations.Commands.DeleteBook
{
    public class DeleteBookCommandTest : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;

        public DeleteBookCommandTest(CommonTestFixture commonTestFixture)
        {
            _context = commonTestFixture.Context;
        }

        [Theory]
        [InlineData(-5)]
        [InlineData(99999)]
        public void WhenGivenBookIdIsNotExist_InvalidOperationException_ShouldBeReturnError(int bookId)
        {
            // Arrange (preparation)
            DeleteBookCommand command = new DeleteBookCommand(_context);
            command.BookId = bookId;

            // Act & Assert (run and confirmation)
            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap Bulunamadı");
        }

        [Theory]
        [InlineData(3)]
        [InlineData(1)]
        public void WhenValidInputsAreGiven_Book_ShouldBeDeleted(int bookId)
        {
            // Arrange (preparation)
            DeleteBookCommand command = new DeleteBookCommand(_context);
            command.BookId = bookId;

            // Act
            FluentActions
               .Invoking(() => command.Handle()).Invoke();

            // Assert 
            var book = _context.Books.SingleOrDefault(x => x.Id == bookId);
            book.Should().BeNull();
        }
    }
}

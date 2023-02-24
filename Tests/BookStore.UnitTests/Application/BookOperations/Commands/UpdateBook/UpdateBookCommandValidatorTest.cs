using BookStore.UnitTests.TestSetup;
using BookStorePatika.Application.Commands.BookOperations.UpdateBook;
using FluentAssertions;
using Xunit;
using static BookStorePatika.Application.Commands.BookOperations.UpdateBook.UpdateBookCommand;

namespace BookStore.UnitTests.Application.BookOperations.Commands.UpdateBook
{
    public class UpdateBookCommandValidatorTest : IClassFixture<CommonTestFixture>
    {

        [Theory]
        [InlineData(1, "Lord Of The Rings", 0)]
        [InlineData(2, "Lord Of The Rings", 0)]
        [InlineData(3, "", 0)]
        [InlineData(555, "Lord Of The Rings", 0)]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(int bookId, string title, int genreId)
        {
            // Arrange
            UpdateBookCommand command = new UpdateBookCommand(null);
            command.BookId = bookId;
            command.Model = new UpdateBookViewModel()
            {
                Title = title,
                GenreId = genreId
            };

            // Act
            UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
            var result = validator.Validate(command);

            // Assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnError()
        {
            // Arrange
            UpdateBookCommand command = new UpdateBookCommand(null);
            command.BookId = 1;
            command.Model = new UpdateBookViewModel()
            {
                Title = "PürDikkat",
                GenreId = 1
            };

            // Act
            UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
            var result = validator.Validate(command);

            // Assert
            result.Errors.Count.Should().Be(0);
        }
    }
}
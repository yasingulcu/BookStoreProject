using BookStore.UnitTests.TestSetup;
using BookStorePatika.Application.GenreOperations.Commands.DeleteGenre;
using FluentAssertions;
using Xunit;

namespace BookStore.UnitTests.Application.GenreOperations.Commands.DeleteGenre
{
    public class DeleteGenreCommandValidatorTest : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData(-3766)]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-66)]
        public void WhenLowerThanAndEqualToZeroIdIsGiven_Validator_ShouldBeReturnError(int genreId)
        {
            // Arrange
            DeleteGenreCommand command = new DeleteGenreCommand(null);
            command.GenreId = genreId;

            // Act
            DeleteGenreCommandValidator validator = new DeleteGenreCommandValidator();
            var result = validator.Validate(command);


            // Assert
            result.Errors.Count.Should().NotBe(0);
        }

        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnError()
        {
            // Arrange
            DeleteGenreCommand command = new DeleteGenreCommand(null);
            command.GenreId = 66;

            // Act
            DeleteGenreCommandValidator validator = new DeleteGenreCommandValidator();
            var result = validator.Validate(command);

            // Assert
            result.Errors.Count.Should().Be(0);
        }
    }
}
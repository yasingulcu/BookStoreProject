using BookStore.UnitTests.TestSetup;
using BookStorePatika.Application.GenreOperations.Commands.UpdateGenre;
using FluentAssertions;
using System.Linq;
using Xunit;
using static BookStorePatika.Application.GenreOperations.Commands.UpdateGenre.UpdateGenreCommand;

namespace BookStore.UnitTests.Application.GenreOperations.Commands.UpdateGenre
{
    public class UpdateGenreCommandValidatorTest : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData(4, "Ph")]
        [InlineData(2, "P")]
        [InlineData(3, "Phy")]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(int genreId, string genreName)
        {
            // Arrange
            UpdateGenreCommand command = new UpdateGenreCommand(null);
            command.GenreId = genreId;
            command.Model = new UpdateGenreViewModel()
            {
                Name = genreName,
            };

            // Act
            UpdateGenreCommandValidator validator = new UpdateGenreCommandValidator();
            var result = validator.Validate(command);

            // Assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnError()
        {
            // Arrange
            UpdateGenreCommand command = new UpdateGenreCommand(null);
            command.GenreId = 1;
            command.Model = new UpdateGenreViewModel()
            {
                Name = "Physiology",
            };

            // Act
            UpdateGenreCommandValidator validator = new UpdateGenreCommandValidator();
            var result = validator.Validate(command);

            // Assert
            result.Errors.Count.Should().Be(0);
        }
    }
}
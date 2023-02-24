using AutoMapper;
using BookStore.UnitTests.TestSetup;
using BookStorePatika.Application.GenreOperations.Commands.UpdateGenre;
using BookStorePatika.DBOperations;
using FluentAssertions;
using System;
using System.Linq;
using Xunit;
using static BookStorePatika.Application.GenreOperations.Commands.UpdateGenre.UpdateGenreCommand;

namespace BookStore.UnitTests.Application.GenreOperations.Commands.UpdateGenre
{
    public class UpdateGenreCommandTest : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;

        public UpdateGenreCommandTest(CommonTestFixture commonTestFixture)
        {
            _context = commonTestFixture.Context;
        }

        [Theory]
        [InlineData(0)]
        [InlineData(4)]
        [InlineData(-2)]
        public void WhenGivenGenreIdIsNotExist_InvalidOperationException_ShouldBeReturnErrors(int id)
        {
            // Arrange
            UpdateGenreCommand command = new UpdateGenreCommand(_context);
            command.GenreId = id;
            command.Model = new UpdateGenreViewModel();

            // Act and Assert
            FluentActions.Invoking(() => command.Handle()).Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap türü bulunamadı.");
        }

        [Fact]
        public void WhenValidInputsAreGiven_Genre_ShouldBeUpdated()
        {
            // Arrange (preparation)
            int genreId = 1;
            UpdateGenreCommand command = new UpdateGenreCommand(_context);
            UpdateGenreViewModel model = new UpdateGenreViewModel()
            {
                Name = "Physiology"
            };
            command.Model = model;
            command.GenreId = genreId;

            // Act
            FluentActions.Invoking(() => command.Handle()).Invoke();

            // Assert
            var genre = _context.Genres.SingleOrDefault(x => x.Id == genreId);

            genre.Should().NotBeNull();
            genre.Name.Should().Be(model.Name);
        }

    }
}
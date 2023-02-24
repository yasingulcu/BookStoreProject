﻿using AutoMapper;
using BookStore.UnitTests.TestSetup;
using BookStorePatika.Application.GenreOperations.Commands.CreateGenre;
using BookStorePatika.DBOperations;
using BookStorePatika.Entities;
using FluentAssertions;
using System;
using System.Linq;
using Xunit;
using static BookStorePatika.Application.GenreOperations.Commands.CreateGenre.CreateGenreCommand;

namespace BookStore.UnitTests.Application.GenreOperations.Commands.CreateGenre
{
    public class CreateGenreCommandTest : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public CreateGenreCommandTest(CommonTestFixture commonTestFixture)
        {
            _context = commonTestFixture.Context;
            _mapper = commonTestFixture.Mapper;
        }

        [Fact]
        public void WhenAlreadyExistGenreNameIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            // Arrange (preparation)
            var genre = new Genre() { Name = "Test_WhenAlreadyExistGenreNameIsGiven_InvalidOperationException_ShouldBeReturn" };
            _context.Genres.Add(genre);
            _context.SaveChanges();

            CreateGenreCommand command = new CreateGenreCommand(_context,_mapper);
            command.Model = new CreateGenreModel() { Name = genre.Name };

            // Act & Assert (run and confirmation)
            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Bu kitap türü zaten mevcut.");
        }

        [Fact]
        public void WhenValidInputsAreGiven_Genre_ShouldBeCreated()
        {
            // Arrange (preparation)
            CreateGenreCommand command = new CreateGenreCommand(_context,_mapper);
            CreateGenreModel model = new CreateGenreModel() { Name = "Physiology" };
            command.Model = model;

            // Act
            FluentActions.Invoking(() => command.Handle()).Invoke();

            // Assert
            var genre = _context.Genres.SingleOrDefault(x => x.Name == model.Name);

            genre.Should().NotBeNull();
            genre.Name.Should().Be(model.Name);
        }
    }
}
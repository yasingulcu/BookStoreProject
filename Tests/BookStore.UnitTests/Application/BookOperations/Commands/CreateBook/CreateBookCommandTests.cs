﻿using AutoMapper;
using BookStore.UnitTests.TestSetup;
using BookStorePatika.Application.Commands.BookOperations.CreateBook;
using BookStorePatika.DBOperations;
using BookStorePatika.Entities;
using FluentAssertions;
using System;
using System.Linq;
using Xunit;
using static BookStorePatika.Application.Commands.BookOperations.CreateBook.CreateBookCommand;

namespace BookStore.UnitTests.Application.BookOperations.Commands.CreateBook
{
    public class CreateBookCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public CreateBookCommandTests(CommonTestFixture commonTestFixture)
        {
            _context = commonTestFixture.Context;
            _mapper = commonTestFixture.Mapper;
        }

        [Fact]

        public void WhenAlreadyExistBookTitleIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            // Arrange (preparation)
            var book = new Book() { Title = "Test_WhenAlreadyExistBookTitleIsGiven_InvalidOperationException_ShouldBeReturn", AuthorId = 4, GenreId = 1, PageCount = 2207, PublishDate = new System.DateTime(2022, 07, 22) };
            _context.Books.Add(book);
            _context.SaveChanges();

            CreateBookCommand command = new CreateBookCommand(_context, _mapper);
            command.Model = new CreateBookModel() { Title = book.Title };

            // Act & Assert (run and confirmation)
            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap zaten mevcut.");
        }

        [Fact]
        public void WhenValidInputsAreGiven_Book_ShouldBeCreated()
        {
            // Arrange (preparation)
            CreateBookCommand command = new CreateBookCommand(_context, _mapper);
            CreateBookModel model = new CreateBookModel() { Title = "PürDikkat", PageCount = 150, PublishDate = DateTime.Now.Date.AddYears(-2), GenreId = 1 };
            command.Model = model;

            // Act
            FluentActions.Invoking(() => command.Handle()).Invoke();

            // Assert
            var book = _context.Books.SingleOrDefault(x => x.Title == model.Title);

            book.Should().NotBeNull();
            book.PageCount.Should().Be(model.PageCount);
            book.PublishDate.Should().Be(model.PublishDate);
            book.GenreId.Should().Be(model.GenreId);
        }
    }
}

using AutoMapper;
using BookStore.UnitTests.TestSetup;
using BookStorePatika.Application.GenreOperations.Queries.GetGenreDetail;
using BookStorePatika.DBOperations;
using FluentAssertions;
using System;
using Xunit;

namespace BookStore.UnitTests.Application.GenreOperations.Queries.GetGenreDetail
{
    public class GetGenreDetailQueryTest : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public GetGenreDetailQueryTest(CommonTestFixture commonTestFixture)
        {
            _context = commonTestFixture.Context;
            _mapper = commonTestFixture.Mapper;
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(66)]
        [InlineData(3766)]
        public void WhenGenreIdIsNotFound_InvalidOperationException_ShouldReturnError(int id)
        {
            GetGenreDetailQuery query = new GetGenreDetailQuery(_context, _mapper);
            query.GenreId = id;
            FluentActions.Invoking(() => query.Handle()).Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap türü bulunamadı.");
        }
    }
}
using AutoMapper;
using BookStore.UnitTests.TestSetup;
using BookStorePatika.Application.Queries.BookOperations.GetBookDetail;
using BookStorePatika.DBOperations;
using FluentAssertions;
using System;
using Xunit;

namespace BookStore.UnitTests.Application.BookOperations.Queries
{
    public class GetBookDetailQueryTest : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public GetBookDetailQueryTest(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }
        [Theory]
        [InlineData(10)]
        [InlineData(66)]
        [InlineData(6637)]
        public void WhenBookIdIsNotFound_InvalidOperationException_ShouldReturnError(int id)
        {
            GetBookDetailQuery query = new GetBookDetailQuery(_context, _mapper);
            query.BookId = id;
            FluentActions.Invoking(() => query.Handle()).Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap Bulunamadı");

        }
    }
}
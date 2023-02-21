﻿using FluentValidation;

namespace BookStorePatika.Application.GenreOperations.Queries.GetGenreDetail
{
    public class GetGenreDetailQueryValidator : AbstractValidator<GetGenreDetailQuery>
    {
        public GetGenreDetailQueryValidator()
        {
            RuleFor(query => query.GenreId).GreaterThan(0);
        }
    }
}

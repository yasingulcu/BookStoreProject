using BookStorePatika.DBOperations;
using BookStorePatika.Entities;
using System;

namespace BookStore.UnitTests.TestSetup
{
    public static class Authors
    {
        public static void AddAuthors(this BookStoreDbContext context)
        {
            context.Authors.AddRange
                (
                   new Author()
                   {
                       Name = "Yasin",
                       Surname = "Gülcü",
                       DateOfBirth = DateTime.Now,
                   },

                   new Author()
                   {
                       Name = "Sümeyra",
                       Surname = "Gülcü",
                       DateOfBirth = DateTime.Now,
                   },

                   new Author()
                   {
                       Name = "Eftal",
                       Surname = "Gülcü",
                       DateOfBirth = DateTime.Now,
                   }
                );
        }
    }
}

using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStorePatika.Entities
{
    public class Author
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string FullName => $"{Name} {Surname}";
        public DateTime DateOfBirth { get; set; }
    }
}

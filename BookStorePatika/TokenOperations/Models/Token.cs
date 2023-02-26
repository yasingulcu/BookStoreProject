using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStorePatika.TokenOperations.Models
{
    public class Token
    {
        public string AccesToken { get; set; }
        public DateTime Expiration { get; set; }
        public string RefreshToken { get; set; }
    }
}

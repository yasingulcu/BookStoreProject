using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStorePatika.Services
{
    public interface ILoggerService
    {
        void Write(string message);
    }
}

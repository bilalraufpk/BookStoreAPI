using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BookStoreAPI.Models;

namespace BookStoreAPI.Repositories
{
    interface IBookCategory
    {
        IEnumerable<BookCategory> Get();
        BookCategory Get(int Id);
        void Add(BookCategory Model);
        void Update(BookCategory Model, int Id);
        void Delete(int Id);
        bool AlreadyExists(string Name);
        bool ValidateInput(BookCategory Model, out string Message);
        void Save();
    }
}
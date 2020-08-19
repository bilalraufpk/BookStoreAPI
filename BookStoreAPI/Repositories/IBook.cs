using BookStoreAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookStoreAPI.Repositories
{
    interface IBook
    {
        IEnumerable<View_Books> Get();
        View_Books Get(int Id);
        Book GetBook(int Id);
        void Add(Book Model);
        void Update(Book Model, int Id);
        void Delete(int Id);
        bool AlreadyExists(string ISBN);
        bool ValidateInput(Book Model, out string Message);
        void Save();
    }
}
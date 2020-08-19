using BookStoreAPI.Models;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BookStoreAPI.Repositories
{
    public class BookRepository : IBook
    {
        private readonly BookStoreEntities _context;
        public BookRepository(BookStoreEntities Context)
        {
            _context = Context;
        }

        
        public IEnumerable<View_Books> Get()
        {
            return _context.View_Books.ToList();
        }         
        public View_Books Get(int Id)
        {
            return _context.View_Books.FirstOrDefault(x => x.Id == Id);
        }       
        public Book GetBook(int Id)
        {
            return _context.Books.FirstOrDefault(x => x.Id == Id);
        }
        public void Add(Book Model)
        {
            Model.DateTime = DateTime.Now;
            _context.Books.Add(Model);
        }
        public void Update(Book Model, int Id)
        {
            _context.Entry(Model).State = EntityState.Modified;
        }
        public void Delete(int Id)
        {
            Book _model = _context.Books.Find(Id);
            _context.Books.Remove(_model);
        }
        public bool AlreadyExists(string ISBN)
        {
           Book _book = _context.Books.FirstOrDefault(x => x.ISBN == ISBN);
            if (_book == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public bool ValidateInput(Book Model, out string Message)
        {
            // Any validation checks
            Message = null;
            if (Model.Year < 1000 || Model.Year > DateTime.Now.Year)
            {
                Message = "Year should be between 1000 to " + DateTime.Now.Year;
                return false;
            }
            else if (Model.Price <= 0)
            {
                Message = "Price should be more than 0";
                return false;
            }
            else if (Model.Title.Length < 5)
            {
                Message = "Book title should be minimum 5 charactors";
                return false;
            }
            else if (Model.ISBN.Length < 10 || Model.ISBN.Length > 13)
            {
                Message = "ISBN should be of length between 10 to 13";
                return false;
            }

            return true;
        }
        public void Save()
        {
            _context.SaveChanges();
        }
        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
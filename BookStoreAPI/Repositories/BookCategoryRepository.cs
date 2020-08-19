using BookStoreAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BookStoreAPI.Repositories
{
    public class BookCategoryRepository : IBookCategory
    {
        private readonly BookStoreEntities _context;
        public BookCategoryRepository(BookStoreEntities context)
        {
            _context = context;
        }

        public IEnumerable<BookCategory> Get() 
        {
            return _context.BookCategories.ToList();
        }
        public BookCategory Get(int Id)
        {
            return _context.BookCategories.FirstOrDefault(x=>x.Id == Id);
        }
        public void Add(BookCategory Model)
        {
            Model.DateTime = DateTime.Now;
            _context.BookCategories.Add(Model);
        }
        public void Update(BookCategory Model, int Id)
        {
            _context.Entry(Model).State = EntityState.Modified;
        }
        public void Delete(int Id)
        {
            BookCategory _model = _context.BookCategories.Find(Id);
            _context.BookCategories.Remove(_model);
        }
        public bool AlreadyExists(string Name)
        {
            BookCategory _category = _context.BookCategories.FirstOrDefault(x => x.Name == Name);
            if (_category == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public bool ValidateInput(BookCategory Model, out string Message)
        {
            // Any validation checks
            Message = null;
            if (Model.Name.Length < 5)
            {
                Message = "Category name should be minimum 5 charactors";
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
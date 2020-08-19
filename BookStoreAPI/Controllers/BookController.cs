using BookStoreAPI.Models;
using BookStoreAPI.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace BookStoreAPI.Controllers
{
    public class BookController : ApiController
    {
        private IBook _bookRepository;
        private IBookCategory _bookCategoryRepository;
        public BookController()
        {
            BookStoreEntities _entity = new BookStoreEntities();
            this._bookRepository = new BookRepository(_entity);
            this._bookCategoryRepository = new BookCategoryRepository(_entity);
        }

        /// <summary>
        /// Gets list of Books.
        /// </summary>
        [HttpGet]
        [Route("api/Book/Get")]
        public IEnumerable<View_Books> Get()
        {
            return _bookRepository.Get();
        }

        /// <summary>
        /// Get book by id with category.
        /// </summary>      
        [HttpGet]
        [Route("api/Book/Get/{Id}")]
        public IHttpActionResult Get(int Id)
        {
            View_Books _book = _bookRepository.Get(Id);
            if (_book == null)
            {
                return NotFound();
            }

            return Ok(_book);
        }

        /// <summary>
        /// Add book.
        /// </summary>
        [HttpPost]
        [Route("api/Book/Add")]
        public IHttpActionResult Add(Book Model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Data can not be saved");
                }

                BookCategory _category = _bookCategoryRepository.Get(Model.CategoryId);
                if (_category == null)
                {
                    return NotFound();
                }

                if (_bookRepository.AlreadyExists(Model.ISBN))
                {
                    return BadRequest("ISBN already assigned to a book");
                }

                string _message;
                if (!_bookRepository.ValidateInput(Model, out _message))
                {
                    return BadRequest(_message);
                }

                _bookRepository.Add(Model);
                _bookRepository.Save();

                return Ok("Record Saved");
            }
            catch (Exception) { return BadRequest("Record can not be saved"); }
        }

        /// <summary>
        /// Update book.
        /// </summary>
        [HttpPut]
        [Route("api/Book/Update")]
        public IHttpActionResult Update(Book Model, int Id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Data can not be updated");
                }

                if (Id != Model.Id)
                {
                    return BadRequest("Invalid Data");
                }

                Book _book = _bookRepository.GetBook(Id);
                if (_book == null)
                {
                    return NotFound();
                }

                string _message;
                if (!_bookRepository.ValidateInput(Model, out _message))
                {
                    return BadRequest(_message);
                }

                _bookRepository.Update(Model, Id);
                _bookRepository.Save();

                return Ok("Record Updated");
            }
            catch (Exception) { return BadRequest("Record can not be updated"); }
        }

        /// <summary>
        /// Delete book.
        /// </summary>
        [HttpDelete]
        [Route("api/Book/Delete")]
        public IHttpActionResult Delete(int Id)
        {
            try
            {
                Book _book = _bookRepository.GetBook(Id);
                if (_book == null)
                {
                    return NotFound();
                }

                _bookRepository.Delete(Id);
                _bookRepository.Save();

                return Ok("Record Deleted");
            }
            catch (Exception) { return BadRequest("Record can not be deleted"); }
        }
    }
}
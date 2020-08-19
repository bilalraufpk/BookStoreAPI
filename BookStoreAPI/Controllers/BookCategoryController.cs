using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;
using BookStoreAPI.Models;
using BookStoreAPI.Repositories;
using Microsoft.Ajax.Utilities;

namespace BookStoreAPI.Controllers
{
    public class BookCategoryController : ApiController
    {
        private IBookCategory _bookCategoryRepository;
        public BookCategoryController()
        {
            this._bookCategoryRepository = new BookCategoryRepository(new BookStoreEntities());
        }

        /// <summary>
        /// Gets list of categories.
        /// </summary>
        [HttpGet]
        [Route("api/BookCategory/Get")]
        public IEnumerable<BookCategory> Get()
        {
            return _bookCategoryRepository.Get();
        }

        /// <summary>
        /// Gets specific category by its id.
        /// <param name="Id">The ID of the data.</param>
        /// </summary>
        [HttpGet]
        [Route("api/BookCategory/Get/{Id}")]
        public IHttpActionResult Get(int Id)
        {
            BookCategory _category = _bookCategoryRepository.Get(Id);
            if (_category == null)
            {
                return NotFound();
            }

            return Ok(_category);
        }

        /// <summary>
        /// Add book category.        
        /// </summary>
        [HttpPost]
        [Route("api/BookCategory/Add")]
        public IHttpActionResult Add(BookCategory Model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Data can not be saved");                    
                }

                if (_bookCategoryRepository.AlreadyExists(Model.Name))
                {
                    return BadRequest("Category already exists");
                }

                string _message;
                if (!_bookCategoryRepository.ValidateInput(Model, out _message))
                {
                    return BadRequest(_message);
                }

                _bookCategoryRepository.Add(Model);
                _bookCategoryRepository.Save();

                return Ok("Record Saved");
            }
            catch (Exception) { return BadRequest("Record can not be saved"); }
        }

        /// <summary>
        /// Update book category.        
        /// </summary>
        [HttpPut]
        [Route("api/BookCategory/Update")]
        public IHttpActionResult Update(BookCategory Model, int Id)
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

                BookCategory _category = _bookCategoryRepository.Get(Id);
                if (_category == null)
                {
                    return NotFound();
                }

                _bookCategoryRepository.Update(Model, Id);
                _bookCategoryRepository.Save();

                return Ok("Record Updated");
            }
            catch (Exception) { return BadRequest("Record can not be updated"); }
        }

        /// <summary>
        /// Delete category by its id.
        /// <param name="Id">The ID of the data.</param>
        /// </summary>
        [HttpDelete]
        [Route("api/BookCategory/Delete")]
        public IHttpActionResult Delete(int Id)
        {
            try
            {
                BookCategory _category = _bookCategoryRepository.Get(Id);
                if (_category == null)
                {
                    return NotFound();
                }

                _bookCategoryRepository.Delete(Id);
                _bookCategoryRepository.Save();

                return Ok("Record Deleted");
            }
            catch (Exception) { return BadRequest("Record can not be deleted"); }
        }
    }
}
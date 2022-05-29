using BMS.models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class tblBMSController : ControllerBase
    {
        private readonly BMSContext _context;
        public tblBMSController(BMSContext BMSContext)
        {
            _context = BMSContext;
        }

        [HttpGet("Books")]
        public IActionResult GetBooks()
        {
            var booksdetails = _context.tblBMS.AsQueryable();
            return Ok(booksdetails);
        }
        [HttpPost("add_Book")]
        public IActionResult AddBook([FromBody] tblBMS bmsobj)
        {
            if (bmsobj == null)
            {
                return BadRequest();
            }
            else
            {
                _context.tblBMS.Add(bmsobj);
                _context.SaveChanges();
                return Ok(new
                {
                    StatusCode = 200,
                    Message = "Your Book Added"
                });
            }
        }
        [HttpPut("Edit_Book")]
        public IActionResult EditBook([FromBody] tblBMS bmsobj)
        {
            if(bmsobj == null)
            {
                return BadRequest();

            }
            var Book = _context.tblBMS.AsNoTracking().FirstOrDefault(x => x.ISBM == bmsobj.ISBM);
            if(Book == null)
            {
                return NotFound(new
                {
                    StatusCode = 404,
                    message = "ISBM is not found "
                });
            }
            else
            {
                _context.Entry(bmsobj).State = EntityState.Modified;
                _context.SaveChanges();
                return Ok(new
                {
                    StatusCode = 200,
                    Message = "Edited Successfully"
                });
            }
        }
        [HttpDelete("delete_Book/{ISBM}")]
        public IActionResult DeleteBook(int ISBM)
        {
            var Book = _context.tblBMS.Find(ISBM);
            if(Book == null)
            {
                return NotFound(new
                {
                    StatusCode = 404,
                    Message = "Book Not Found"
                });
            }
            else
            {
                _context.Remove(Book);
                _context.SaveChanges();
                return Ok(new
                {
                    StatusCode = 200,
                    Message = "Book Deleted"
                });
            }
        }
        //[HttpGet("getAllBooks")]
        //public IActionResult GetAllBooks()
        //{
        //    var books = _context.tblBMS.AsQueryable();
        //    return Ok(new
        //    {
        //        StatusCode = 200,
        //        BookDetails = books
        //    });
        //}
        [HttpGet("getBook/ISBN")]
        public IActionResult Getbook(int ISBN)
        {
            var book = _context.tblBMS.Find(ISBN);
            if(book == null)
            {
                return NotFound(new
                {
                    StatusCode = 400,
                    Message = "UserNot Found"
                });
            }
            else
            {
                return Ok(new
                {
                    StatusCode = 200,
                    BookDetails = book
                });
            }
        }
    }
}

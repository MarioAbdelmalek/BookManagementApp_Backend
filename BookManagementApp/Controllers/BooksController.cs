using BookManagement.BL.Models;
using BookManagement.BL.Services;
using BookManagement.DAL.Entities;
using Microsoft.AspNetCore.Mvc;

namespace BookManagementApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly BookService bookService;

        public BooksController(BookService bookService)
        {
            this.bookService = bookService;
        }

        [HttpGet]
        public async Task<ActionResult<List<BookDto>>> GetAllBooks()
        {
            var books = await bookService.GetAllBooks();
            return Ok(books);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BookDto>> GetBookByID(int id)
        {
            var book = await bookService.GetBookByID(id);
            return Ok(book);
        }

        [HttpPost]
        public async Task<ActionResult<bool>> CreateBook([FromBody] BookDto bookDto)
        {
            var result = await bookService.CreateBook(bookDto);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<bool>> UpdateBook(int id, [FromBody] BookDto bookDto)
        {
            var result = await bookService.UpdateBook(id, bookDto);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteBook(int id)
        {
            var result = await bookService.DeleteBookByID(id);
            return Ok(result);
        }
    }
}

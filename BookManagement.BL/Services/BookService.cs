using AutoMapper;
using BookManagement.BL.Models;
using BookManagement.DAL.Entities;
using BookManagement.DAL.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BookManagement.BL.Services
{
    public class BookService
    {
        private readonly BookRepository bookRepository;
        private readonly IMapper mapper;

        public BookService(BookRepository bookRepository, IMapper mapper)
        {
            this.bookRepository = bookRepository;
            this.mapper = mapper;
        }

        public async Task<List<BookDto>> GetAllBooks()
        {
            var books = await bookRepository.GetAll<Book>().OrderByDescending(item => item.Id).ToListAsync();
            return mapper.Map<List<BookDto>>(books);
        }

        public async Task<BookDto> GetBookByID(int bookID)
        {
            var book = await bookRepository.GetAll<Book>().FirstOrDefaultAsync(item => item.Id == bookID);
            if (book != null)
                return mapper.Map<BookDto>(book);
            else
                throw new Exception("Not Found");
        }

        public async Task<bool> CreateBook(BookDto bookDto)
        {
            var book = mapper.Map<Book>(bookDto);
            await bookRepository.CreateAsync(book);
            await bookRepository.CommitAsync();
            return true;
        }

        public async Task<bool> UpdateBook(int bookID, BookDto bookDto)
        {
            var bookToUpdate = await bookRepository.GetAll<Book>().FirstOrDefaultAsync(item => item.Id == bookID);
            if (bookToUpdate != null)
            {
                bookToUpdate.PublishedYear = bookDto.PublishedYear;
                bookToUpdate.Author = bookDto.Author;
                bookToUpdate.Title = bookDto.Title;
                bookToUpdate.Genre = bookDto.Genre;
                bookRepository.Update(bookToUpdate);
                await bookRepository.CommitAsync();
                return true;
            }
            else
                throw new Exception("Not Found");
        }

        public async Task<bool> DeleteBookByID(int bookID)
        {
            var bookToDelete = await bookRepository.GetAll<Book>().FirstOrDefaultAsync(item => item.Id == bookID);
            if (bookToDelete != null)
            {
                bookRepository.Delete(bookToDelete);
                await bookRepository.CommitAsync();
                return true;
            }
            else
                throw new Exception("Not Found");
        }
    }
}

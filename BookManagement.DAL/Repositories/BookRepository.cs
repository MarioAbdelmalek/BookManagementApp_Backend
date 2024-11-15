using BookManagement.DAL.Context;
using BookManagement.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookManagement.DAL.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly BookContext bookContext;

        public BookRepository(BookContext bookContext)
        {
            this.bookContext = bookContext ?? throw new ArgumentNullException(nameof(bookContext));
        }
        public async Task<TEntity> CreateAsync<TEntity>(TEntity entity) where TEntity : class
        {
            var result = await bookContext.AddAsync(entity);
            return entity;
        }

        public void Delete<TEntity>(TEntity entity) where TEntity : class
        {
            bookContext.Remove(entity);
        }

        public IQueryable<TEntity> GetAll<TEntity>() where TEntity : class
        {
            return bookContext.Set<TEntity>().AsNoTracking();
        }

        public void Update<TEntity>(TEntity entity) where TEntity : class
        {
            bookContext.Update(entity);
        }

        public async Task CommitAsync()
        {
            await bookContext.SaveChangesAsync();
        }
    }
}
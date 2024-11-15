using BookManagement.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookManagement.DAL.Context
{
    public class BookContext : DbContext
    {
        public DbSet<Book> Books => Set<Book>();
        public BookContext(DbContextOptions<BookContext> options) : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite("Data Source=books.db"); // Provide the connection string for migrations
            }
        }
    }
}

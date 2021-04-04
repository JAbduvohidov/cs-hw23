using homework23.Models;
using Microsoft.EntityFrameworkCore;

namespace homework23.DataContext
{
    public class QuotesDbContext : DbContext
    {
        public QuotesDbContext(DbContextOptions options) : base(options)
        {
        }

        public QuotesDbContext()
        {
        }

        public DbSet<Quote> Quotes { get; set; }
    }
}
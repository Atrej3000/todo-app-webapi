using Microsoft.EntityFrameworkCore;

namespace TodoAPI.Models
{
    public class CardContext : DbContext
    {
        public CardContext()
        {

        }
        public CardContext(DbContextOptions<CardContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
        public DbSet<Card> Cards { get; set; }
    }
}

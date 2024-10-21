using Microsoft.EntityFrameworkCore;

namespace ToDoList.Models
{
    public class ListContext : DbContext
    {
        public ListContext(DbContextOptions<ListContext> options) : base(options)
        {
            
        }

        public DbSet<List> lists { get; set; }
    }
}

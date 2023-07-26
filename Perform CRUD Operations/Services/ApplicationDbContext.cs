using Microsoft.EntityFrameworkCore;
using Perform_CRUD_Operations.Models;

namespace Perform_CRUD_Operations.Services
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
            
        }

        public DbSet<Product> Products { get; set; }

    }
}

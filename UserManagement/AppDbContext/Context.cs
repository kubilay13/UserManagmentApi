using Microsoft.EntityFrameworkCore;
using UserManagement.Models;

namespace UserManagement.AppDbContext
{
    public class Context : DbContext
    {
        public DbSet<ResponseModel> ResponseModels { get; set; }

        public Context(DbContextOptions<Context> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ResponseModel>()
                .HasKey(t => t.Id);

        }

       
    }
  
}



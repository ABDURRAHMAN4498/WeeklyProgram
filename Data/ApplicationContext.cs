using System.Runtime.Intrinsics.X86;
using Microsoft.EntityFrameworkCore;
using WeeklyProgram.Models;

namespace WeeklyProgram.Data
{

    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {

        }
        public DbSet<Template> Templates { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<UserProject> MyProperty { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>()
            .HasMany(c => c.SubCategories)
            .WithOne(c => c.ParentCategory)
            .HasForeignKey(c => c.ParentCategoryId);
            
        base.OnModelCreating(modelBuilder);
    }
    }
}
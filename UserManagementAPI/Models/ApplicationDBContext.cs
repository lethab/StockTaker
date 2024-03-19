using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UserManagementAPI.Models.Invoice;
using UserManagementAPI.Models.Product;

namespace UserManagementAPI.Models
{
    public class ApplicationDBContext : IdentityDbContext<IdentityUser>
    {
        public DbSet<ProductModel> Products { get; set; }
        public DbSet<InvoiceModel> Invoice { get; set; }

        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            SeedRoles(builder);
        }

        private static void SeedRoles(ModelBuilder builder)
        {
            builder.Entity<IdentityRole>().HasData
                (
                new IdentityRole() { Name = "Admin", ConcurrencyStamp = "1", NormalizedName = "Admin"},
                new IdentityRole() { Name = "User", ConcurrencyStamp = "2", NormalizedName = "User"},
                new IdentityRole() { Name = "Manager", ConcurrencyStamp = "3", NormalizedName = "Manager" }
                );
        }
    }
}

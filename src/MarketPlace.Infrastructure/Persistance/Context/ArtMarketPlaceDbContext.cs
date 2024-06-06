using MarketPlace.Domain.Models;
using MarketPlace.Domain.Models.Auth;
using MarketPlace.Infrastructure.Persistance.Configurations;
using MarketPlace.Infrastructure.Persistance.Constants;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace MarketPlace.Infrastructure.Persistance.Context
{
    public class ArtMarketPlaceDbContext : IdentityDbContext<User, Role, int, UserClaim, UserRole, UserLogin, RoleClaim, UserToken>
    {
        public ArtMarketPlaceDbContext(DbContextOptions<ArtMarketPlaceDbContext> options) : base(options)
        {

        }

        public DbSet<Author> Authors { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }    
        public DbSet<Product> Products{ get; set; }
        public DbSet<Promocode> Promocodes{ get; set; }
        public DbSet<SubCategory> SubCategories{ get; set; }
        public DbSet<AuthorCategory> AuthorCategories { get; set; } 


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            ApplyIdentityMapConfiguration(builder);

            var assembly = typeof(CategSubCategConfig).Assembly;
            builder.ApplyConfigurationsFromAssembly(assembly);
        }

        private void ApplyIdentityMapConfiguration(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("Users", IdentitySchemaConstants.Auth);
            modelBuilder.Entity<UserClaim>().ToTable("UserClaims", IdentitySchemaConstants.Auth);
            modelBuilder.Entity<UserLogin>().ToTable("UserLogins", IdentitySchemaConstants.Auth);
            modelBuilder.Entity<UserToken>().ToTable("UserRoles", IdentitySchemaConstants.Auth);
            modelBuilder.Entity<Role>().ToTable("Roles", IdentitySchemaConstants.Auth);
            modelBuilder.Entity<RoleClaim>().ToTable("RoleClaims", IdentitySchemaConstants.Auth);
            modelBuilder.Entity<UserRole>().ToTable("UserRole", IdentitySchemaConstants.Auth);
        }


    }
}

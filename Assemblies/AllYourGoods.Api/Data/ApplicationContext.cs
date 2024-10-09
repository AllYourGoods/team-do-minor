using AllYourGoods.Api.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AllYourGoods.Api.Data;

public class ApplicationContext : IdentityDbContext<User>
{

    public ApplicationContext(DbContextOptions<ApplicationContext> options): base(options) 
    {
    }

    public ApplicationContext()
    {
    }

    public virtual DbSet<Restaurant> Restaurants { get; set; } = null!;
    // public override DbSet<User> Users { get; set; } = null!;
    public virtual DbSet<Tag> Tags { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
     
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>().Property(u => u.UserName).HasMaxLength(15);
        modelBuilder.Entity<User>().Ignore(u => u.PhoneNumber);
        modelBuilder.Entity<User>().Ignore(u => u.PhoneNumberConfirmed);
        modelBuilder.Entity<User>().Ignore(u => u.EmailConfirmed);
        modelBuilder.Entity<User>().Ignore(u => u.TwoFactorEnabled);
        modelBuilder.Entity<User>().Ignore(u => u.LockoutEnd);
        modelBuilder.Entity<User>().Ignore(u => u.LockoutEnabled);
    }
}



using AllYourGoods.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace AllYourGoods.Api.Data;

public class ApplicationContext : DbContext
{

    public ApplicationContext(DbContextOptions<ApplicationContext> options): base(options) 
    {
    }

    public ApplicationContext()
    {
    }

    public virtual DbSet<Restaurant> Restaurants { get; set; } = null!;
    public virtual DbSet<Tag> Tags { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
     
        base.OnModelCreating(modelBuilder);
    }
}



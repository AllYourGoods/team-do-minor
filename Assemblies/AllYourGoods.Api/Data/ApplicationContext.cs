using AllYourGoods.Api.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using AllYourGoods.Api.Models.Enums;
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
    public virtual DbSet<RefreshToken> RefreshTokens { get; set; } = null!;
    public virtual DbSet<Address> Addresses { get; set; } = null!;
    public virtual DbSet<ImageFile> ImageFiles { get; set; } = null!;
    public virtual DbSet<Menu> Menus { get; set; } = null!;
    public virtual DbSet<Category> Categories { get; set; } = null!;
    public virtual DbSet<Product> Products { get; set; } = null!;
    public virtual DbSet<Order> Orders { get; set; } = null!;
    public virtual DbSet<DeliveryPerson> DeliveryPersons { get; set; } = null!;
    public virtual DbSet<OpeningsTime> OpeningsTimes { get; set; } = null!;
    public virtual DbSet<FrequentlyAskedQuestion> FrequentlyAskedQuestions { get; set; } = null!;
    public virtual DbSet<Tag> Tags { get; set; } = null!;
    public virtual DbSet<ProductHasTag> ProductTags { get; set; } = null!;
    public virtual DbSet<CategoryHasProduct> CategoryProducts { get; set; } = null!;
    public virtual DbSet<OrderHasProduct> OrderProducts { get; set; } = null!;
    public virtual DbSet<RestaurantHasTags> RestaurantTags { get; set; } = null!;

    public virtual DbSet<Shift> Shifts { get; set; } = null!;


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Specify singular table names
        modelBuilder.Entity<Restaurant>().ToTable("Restaurant");
        modelBuilder.Entity<Address>().ToTable("Address");
        modelBuilder.Entity<ImageFile>().ToTable("ImageFile");
        modelBuilder.Entity<Menu>().ToTable("Menu");
        modelBuilder.Entity<Category>().ToTable("Category");
        modelBuilder.Entity<Product>().ToTable("Product");
        modelBuilder.Entity<Order>().ToTable("Order");
        modelBuilder.Entity<DeliveryPerson>().ToTable("DeliveryPerson");
        modelBuilder.Entity<OpeningsTime>().ToTable("OpeningsTime");
        modelBuilder.Entity<FrequentlyAskedQuestion>().ToTable("FrequentlyAskedQuestion");
        modelBuilder.Entity<Tag>().ToTable("Tag");
        modelBuilder.Entity<ProductHasTag>().ToTable("ProductHasTag");
        modelBuilder.Entity<CategoryHasProduct>().ToTable("CategoryHasProduct");
        modelBuilder.Entity<OrderHasProduct>().ToTable("OrderHasProduct");
        modelBuilder.Entity<RestaurantHasTags>().ToTable("RestaurantHasTags");
        modelBuilder.Entity<Shift>().ToTable("Shift");
        modelBuilder.Entity<User>().ToTable("User");
        modelBuilder.Entity<User>().Property(u => u.UserName).HasMaxLength(15);
        modelBuilder.Entity<User>().Ignore(u => u.PhoneNumber);
        modelBuilder.Entity<User>().Ignore(u => u.PhoneNumberConfirmed);
        modelBuilder.Entity<User>().Ignore(u => u.EmailConfirmed);
        modelBuilder.Entity<User>().Ignore(u => u.TwoFactorEnabled);
        modelBuilder.Entity<User>().Ignore(u => u.LockoutEnd);
        modelBuilder.Entity<User>().Ignore(u => u.LockoutEnabled);

        modelBuilder.Entity<RefreshToken>()
            .HasOne(rt => rt.User)  
            .WithMany()  
            .HasForeignKey(rt => rt.UserFK) 
            .OnDelete(DeleteBehavior.Cascade); 

        // TODO create 1 to 1 relation
        modelBuilder.Entity<DeliveryPerson>()
            .HasOne(dp => dp.User)  // A DeliveryPerson must have a User
            .WithOne(u => u.DeliveryPerson)  // A User can optionally have a DeliveryPerson
            .HasForeignKey<DeliveryPerson>(dp => dp.UserId)  // Foreign Key on DeliveryPerson
            .IsRequired();

        modelBuilder.Entity<Restaurant>()
            .HasOne(r => r.Owner)
            .WithOne(u => u.Restaurant)
            .HasForeignKey<Restaurant>(r => r.OwnerId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<CategoryHasProduct>()
            .HasKey(cp => new { cp.CategoryId, cp.ProductId });

        modelBuilder.Entity<ProductHasTag>()
            .HasKey(pt => new { pt.ProductId, pt.TagId });

        modelBuilder.Entity<OrderHasProduct>()
            .HasKey(op => new { op.OrderId, op.ProductId });

        modelBuilder.Entity<RestaurantHasTags>()
            .HasKey(rt => new { rt.RestaurantId, rt.TagId });

        modelBuilder.Entity<Order>()
            .HasOne(o => o.Address)
            .WithMany()
            .HasForeignKey(o => o.AddressId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Order>()
            .HasOne(o => o.Restaurant)
            .WithMany()
            .HasForeignKey(o => o.RestaurantId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Order>()
            .HasOne(o => o.Customer)
            .WithMany()
            .HasForeignKey(o => o.CustomerId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Order>()
            .HasOne(o => o.DeliveryPerson)
            .WithMany()
            .HasForeignKey(o => o.DeliveryPersonId)
            .OnDelete(DeleteBehavior.Restrict);

        // One-to-Many: User and Shifts
        modelBuilder.Entity<Shift>()
            .HasOne(s => s.User)
            .WithMany(u => u.Shifts)
            .HasForeignKey(s => s.UserId)
            .OnDelete(DeleteBehavior.Cascade);  // Choose delete behavior

        // One-to-Many: Shift and Orders
        modelBuilder.Entity<Order>()
            .HasOne(o => o.Shift)
            .WithMany(s => s.Orders)
            .HasForeignKey(o => o.ShiftId)
            .OnDelete(DeleteBehavior.Restrict);  // Choose delete behavior to avoid cascading issues
    }
}



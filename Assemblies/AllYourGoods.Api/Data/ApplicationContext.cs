using AllYourGoods.Api.Models;
using AllYourGoods.Api.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace AllYourGoods.Api.Data
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }

        public virtual DbSet<Restaurant> Restaurants { get; set; } = null!;
        public virtual DbSet<Address> Addresses { get; set; } = null!;
        public virtual DbSet<ImageFile> ImageFiles { get; set; } = null!;
        public virtual DbSet<Menu> Menus { get; set; } = null!;
        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<Product> Products { get; set; } = null!;
        public virtual DbSet<Order> Orders { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<DeliveryPerson> DeliveryPersons { get; set; } = null!;
        public virtual DbSet<OpeningsTime> OpeningsTimes { get; set; } = null!;
        public virtual DbSet<FrequentlyAskedQuestion> FrequentlyAskedQuestions { get; set; } = null!;
        public virtual DbSet<Tag> Tags { get; set; } = null!;
        public virtual DbSet<ProductHasTag> ProductTags { get; set; } = null!;
        public virtual DbSet<CategoryHasProduct> CategoryProducts { get; set; } = null!;
        public virtual DbSet<OrderHasProduct> OrderProducts { get; set; } = null!;
        public virtual DbSet<RestaurantHasTags> RestaurantTags { get; set; } = null!;

        public virtual DbSet<UserRoles> UserRoles { get; set; } = null!;
        public virtual DbSet<Roles> Roles { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

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

            modelBuilder.Entity<UserRoles>()
                .HasKey(ur => new { ur.UserId, ur.RoleId });

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

            modelBuilder.Entity<User>()
                .HasMany(u => u.UserRoles)
                .WithOne(ur => ur.User)
                .HasForeignKey(ur => ur.UserId);

            modelBuilder.Entity<Roles>()
                .HasMany(r => r.UserRoles)
                .WithOne(ur => ur.Role)
                .HasForeignKey(ur => ur.RoleId);
        }
    }
}

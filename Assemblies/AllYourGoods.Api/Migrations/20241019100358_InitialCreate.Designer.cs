﻿// <auto-generated />
using System;
using AllYourGoods.Api.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AllYourGoods.Api.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20241019100358_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("AllYourGoods.Api.Models.Address", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("HouseNumber")
                        .IsRequired()
                        .HasMaxLength(6)
                        .HasColumnType("nvarchar(6)");

                    b.Property<double?>("Latitude")
                        .HasColumnType("float");

                    b.Property<double?>("Longitude")
                        .HasColumnType("float");

                    b.Property<string>("StreetName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("ZipCode")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.HasKey("Id");

                    b.ToTable("Address", (string)null);
                });

            modelBuilder.Entity("AllYourGoods.Api.Models.Category", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("MenuId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<double>("Weight")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("MenuId");

                    b.ToTable("Category", (string)null);
                });

            modelBuilder.Entity("AllYourGoods.Api.Models.CategoryHasProduct", b =>
                {
                    b.Property<Guid>("CategoryId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("CategoryId", "ProductId");

                    b.HasIndex("ProductId");

                    b.ToTable("CategoryHasProduct", (string)null);
                });

            modelBuilder.Entity("AllYourGoods.Api.Models.DeliveryPerson", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<TimeSpan>("EndTime")
                        .HasColumnType("time");

                    b.Property<TimeSpan>("EstimatedTime")
                        .HasColumnType("time");

                    b.Property<double>("MaxDistance")
                        .HasColumnType("float");

                    b.Property<string>("Region")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<TimeSpan>("StartTime")
                        .HasColumnType("time");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("WayOfTransport")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("DeliveryPerson", (string)null);
                });

            modelBuilder.Entity("AllYourGoods.Api.Models.FrequentlyAskedQuestion", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Answer")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Question")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("RestaurantId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("RestaurantId");

                    b.ToTable("FrequentlyAskedQuestion", (string)null);
                });

            modelBuilder.Entity("AllYourGoods.Api.Models.ImageFile", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AltText")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("FileSize")
                        .HasColumnType("float");

                    b.Property<string>("MimeType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ImageFile", (string)null);
                });

            modelBuilder.Entity("AllYourGoods.Api.Models.Menu", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<Guid>("RestaurantId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("RestaurantId");

                    b.ToTable("Menu", (string)null);
                });

            modelBuilder.Entity("AllYourGoods.Api.Models.OpeningsTime", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<TimeOnly?>("Closing")
                        .HasColumnType("time");

                    b.Property<int?>("Day")
                        .HasColumnType("int");

                    b.Property<TimeOnly?>("Opening")
                        .HasColumnType("time");

                    b.Property<Guid>("RestaurantId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("RestaurantId");

                    b.ToTable("OpeningsTime", (string)null);
                });

            modelBuilder.Entity("AllYourGoods.Api.Models.Order", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AddressId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CustomerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("DeliveryPersonId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("ETA")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Note")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PaymentMethod")
                        .HasColumnType("int");

                    b.Property<Guid>("RestaurantId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ShiftId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<decimal>("TotalPrice")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("AddressId");

                    b.HasIndex("CustomerId");

                    b.HasIndex("DeliveryPersonId");

                    b.HasIndex("RestaurantId");

                    b.HasIndex("ShiftId");

                    b.ToTable("Order", (string)null);
                });

            modelBuilder.Entity("AllYourGoods.Api.Models.OrderHasProduct", b =>
                {
                    b.Property<Guid>("OrderId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Amount")
                        .HasColumnType("int");

                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("OrderId", "ProductId");

                    b.HasIndex("ProductId");

                    b.ToTable("OrderHasProduct", (string)null);
                });

            modelBuilder.Entity("AllYourGoods.Api.Models.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("ImageFileId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("NotAvailable")
                        .HasColumnType("bit");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("ImageFileId");

                    b.ToTable("Product", (string)null);
                });

            modelBuilder.Entity("AllYourGoods.Api.Models.ProductHasTag", b =>
                {
                    b.Property<Guid>("ProductId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("TagId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ProductId", "TagId");

                    b.HasIndex("TagId");

                    b.ToTable("ProductHasTag", (string)null);
                });

            modelBuilder.Entity("AllYourGoods.Api.Models.Restaurant", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AboutUs")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("AddressId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("BannerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("LogoId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<Guid?>("OwnerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<double?>("Radius")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("AddressId");

                    b.HasIndex("BannerId");

                    b.HasIndex("LogoId");

                    b.HasIndex("OwnerId")
                        .IsUnique()
                        .HasFilter("[OwnerId] IS NOT NULL");

                    b.ToTable("Restaurant", (string)null);
                });

            modelBuilder.Entity("AllYourGoods.Api.Models.RestaurantHasTags", b =>
                {
                    b.Property<Guid>("RestaurantId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("TagId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("RestaurantId", "TagId");

                    b.HasIndex("TagId");

                    b.ToTable("RestaurantHasTags", (string)null);
                });

            modelBuilder.Entity("AllYourGoods.Api.Models.Roles", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ConcurrencyStamp")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("NormalizedName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id");

                    b.ToTable("Role", (string)null);
                });

            modelBuilder.Entity("AllYourGoods.Api.Models.Shift", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<TimeSpan>("EndTime")
                        .HasColumnType("time");

                    b.Property<double>("MaxDistance")
                        .HasColumnType("float");

                    b.Property<TimeSpan>("StartTime")
                        .HasColumnType("time");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("WayOfTransport")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Shift", (string)null);
                });

            modelBuilder.Entity("AllYourGoods.Api.Models.Tag", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id");

                    b.ToTable("Tag", (string)null);
                });

            modelBuilder.Entity("AllYourGoods.Api.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Name")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("PasswordSalt")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<Guid?>("RestaurantId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int?>("Role")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("User", (string)null);
                });

            modelBuilder.Entity("AllYourGoods.Api.Models.UserRoles", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("UserRoles", (string)null);
                });

            modelBuilder.Entity("AllYourGoods.Api.Models.Category", b =>
                {
                    b.HasOne("AllYourGoods.Api.Models.Menu", "Menu")
                        .WithMany("Categories")
                        .HasForeignKey("MenuId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Menu");
                });

            modelBuilder.Entity("AllYourGoods.Api.Models.CategoryHasProduct", b =>
                {
                    b.HasOne("AllYourGoods.Api.Models.Category", "Category")
                        .WithMany("CategoryProducts")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AllYourGoods.Api.Models.Product", "Product")
                        .WithMany("CategoryProducts")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("AllYourGoods.Api.Models.DeliveryPerson", b =>
                {
                    b.HasOne("AllYourGoods.Api.Models.User", "User")
                        .WithOne("DeliveryPerson")
                        .HasForeignKey("AllYourGoods.Api.Models.DeliveryPerson", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("AllYourGoods.Api.Models.FrequentlyAskedQuestion", b =>
                {
                    b.HasOne("AllYourGoods.Api.Models.Restaurant", "Restaurant")
                        .WithMany("FAQs")
                        .HasForeignKey("RestaurantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Restaurant");
                });

            modelBuilder.Entity("AllYourGoods.Api.Models.Menu", b =>
                {
                    b.HasOne("AllYourGoods.Api.Models.Restaurant", "Restaurant")
                        .WithMany("Menus")
                        .HasForeignKey("RestaurantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Restaurant");
                });

            modelBuilder.Entity("AllYourGoods.Api.Models.OpeningsTime", b =>
                {
                    b.HasOne("AllYourGoods.Api.Models.Restaurant", "Restaurant")
                        .WithMany("OpeningsTimes")
                        .HasForeignKey("RestaurantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Restaurant");
                });

            modelBuilder.Entity("AllYourGoods.Api.Models.Order", b =>
                {
                    b.HasOne("AllYourGoods.Api.Models.Address", "Address")
                        .WithMany()
                        .HasForeignKey("AddressId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("AllYourGoods.Api.Models.User", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("AllYourGoods.Api.Models.DeliveryPerson", "DeliveryPerson")
                        .WithMany()
                        .HasForeignKey("DeliveryPersonId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("AllYourGoods.Api.Models.Restaurant", "Restaurant")
                        .WithMany()
                        .HasForeignKey("RestaurantId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("AllYourGoods.Api.Models.Shift", "Shift")
                        .WithMany("Orders")
                        .HasForeignKey("ShiftId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("Address");

                    b.Navigation("Customer");

                    b.Navigation("DeliveryPerson");

                    b.Navigation("Restaurant");

                    b.Navigation("Shift");
                });

            modelBuilder.Entity("AllYourGoods.Api.Models.OrderHasProduct", b =>
                {
                    b.HasOne("AllYourGoods.Api.Models.Order", "Order")
                        .WithMany("OrderHasProductList")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AllYourGoods.Api.Models.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("AllYourGoods.Api.Models.Product", b =>
                {
                    b.HasOne("AllYourGoods.Api.Models.ImageFile", "ImageFile")
                        .WithMany()
                        .HasForeignKey("ImageFileId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ImageFile");
                });

            modelBuilder.Entity("AllYourGoods.Api.Models.ProductHasTag", b =>
                {
                    b.HasOne("AllYourGoods.Api.Models.Product", "Product")
                        .WithMany("ProductTags")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AllYourGoods.Api.Models.Tag", "Tag")
                        .WithMany("ProductTags")
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");

                    b.Navigation("Tag");
                });

            modelBuilder.Entity("AllYourGoods.Api.Models.Restaurant", b =>
                {
                    b.HasOne("AllYourGoods.Api.Models.Address", "Address")
                        .WithMany()
                        .HasForeignKey("AddressId");

                    b.HasOne("AllYourGoods.Api.Models.ImageFile", "Banner")
                        .WithMany()
                        .HasForeignKey("BannerId");

                    b.HasOne("AllYourGoods.Api.Models.ImageFile", "Logo")
                        .WithMany()
                        .HasForeignKey("LogoId");

                    b.HasOne("AllYourGoods.Api.Models.User", "Owner")
                        .WithOne("Restaurant")
                        .HasForeignKey("AllYourGoods.Api.Models.Restaurant", "OwnerId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("Address");

                    b.Navigation("Banner");

                    b.Navigation("Logo");

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("AllYourGoods.Api.Models.RestaurantHasTags", b =>
                {
                    b.HasOne("AllYourGoods.Api.Models.Restaurant", "Restaurant")
                        .WithMany("RestaurantTags")
                        .HasForeignKey("RestaurantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AllYourGoods.Api.Models.Tag", "Tag")
                        .WithMany("RestaurantTags")
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Restaurant");

                    b.Navigation("Tag");
                });

            modelBuilder.Entity("AllYourGoods.Api.Models.Shift", b =>
                {
                    b.HasOne("AllYourGoods.Api.Models.User", "User")
                        .WithMany("Shifts")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("AllYourGoods.Api.Models.UserRoles", b =>
                {
                    b.HasOne("AllYourGoods.Api.Models.Roles", "Role")
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AllYourGoods.Api.Models.User", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("AllYourGoods.Api.Models.Category", b =>
                {
                    b.Navigation("CategoryProducts");
                });

            modelBuilder.Entity("AllYourGoods.Api.Models.Menu", b =>
                {
                    b.Navigation("Categories");
                });

            modelBuilder.Entity("AllYourGoods.Api.Models.Order", b =>
                {
                    b.Navigation("OrderHasProductList");
                });

            modelBuilder.Entity("AllYourGoods.Api.Models.Product", b =>
                {
                    b.Navigation("CategoryProducts");

                    b.Navigation("ProductTags");
                });

            modelBuilder.Entity("AllYourGoods.Api.Models.Restaurant", b =>
                {
                    b.Navigation("FAQs");

                    b.Navigation("Menus");

                    b.Navigation("OpeningsTimes");

                    b.Navigation("RestaurantTags");
                });

            modelBuilder.Entity("AllYourGoods.Api.Models.Roles", b =>
                {
                    b.Navigation("UserRoles");
                });

            modelBuilder.Entity("AllYourGoods.Api.Models.Shift", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("AllYourGoods.Api.Models.Tag", b =>
                {
                    b.Navigation("ProductTags");

                    b.Navigation("RestaurantTags");
                });

            modelBuilder.Entity("AllYourGoods.Api.Models.User", b =>
                {
                    b.Navigation("DeliveryPerson");

                    b.Navigation("Restaurant");

                    b.Navigation("Shifts");

                    b.Navigation("UserRoles");
                });
#pragma warning restore 612, 618
        }
    }
}
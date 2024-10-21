using AllYourGoods.Api.Models;
using AllYourGoods.Api.Models.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AllYourGoods.Api.Data
{
    public class DbInitializer
    {
        public static void Initialize(ApplicationContext context)
        {
            // Ensure the database is created and migrations are applied
            context.Database.Migrate();

            // Check if there are any existing roles
            if (context.Roles.Any())
            {
                return; // DB has been seeded
            }

            // Seed data for roles
            var seedRoles = new Roles[]
            {
                new() { Name = "Admin", NormalizedName = "ADMIN", ConcurrencyStamp = Guid.NewGuid().ToString() },
                new() { Name = "User", NormalizedName = "USER", ConcurrencyStamp = Guid.NewGuid().ToString() }
            };

            context.Roles.AddRange(seedRoles);
            context.SaveChanges();

            // Seed data for users
            var seedUsers = new User[]
            {
                new()
                {
                    Id = Guid.NewGuid(),
                    Name = "John Doe",
                    Email = "john.doe@example.com",
                    PasswordHash = "HashedPassword1",
                    PasswordSalt = "Salt1",
                },
                new()
                {
                    Id = Guid.NewGuid(),
                    Name = "Jane Smith",
                    Email = "jane.smith@example.com",
                    PasswordHash = "HashedPassword2",
                    PasswordSalt = "Salt2",
                },
            };

            context.Users.AddRange(seedUsers);
            context.SaveChanges();

            var delperson = new DeliveryPerson{
                Region = "north-pole",
                EstimatedTime = TimeSpan.FromHours(2),
                UserId = seedUsers[0].Id
            };

            context.DeliveryPersons.AddRange(delperson);
            context.SaveChanges();

            // Seed data for UserRoles
            var userRoles = new UserRoles[]
            {
                new() { UserId = seedUsers[0].Id, RoleId = seedRoles[0].Id },
                new() { UserId = seedUsers[1].Id, RoleId = seedRoles[1].Id },
            };

            context.UserRoles.AddRange(userRoles);
            context.SaveChanges();

            // Seed data for restaurants
            if (!context.Restaurants.Any())
            {
                var seedRestaurants = new Restaurant[]
                {
                    new()
                    {
                        Name = "McDonald's",
                        PhoneNumber = "123456789",
                        AboutUs = "The best fast food in the world",
                        Radius = 5,
                        Logo = new ImageFile
                        {
                            Url = "https://www.mcdonalds.com/content/dam/sites/usa/nfl/icons/arches-logo_108x108.jpg",
                            AltText = "McDonald's logo",
                            MimeType = "image/jpeg",
                            FileSize = 0.1
                        },
                        Address = new Address
                        {
                            City = "Rotterdam",
                            HouseNumber = "12",
                            StreetName = "Coolsingel",
                            ZipCode = "3011AD",
                            Latitude = 51.9225,
                            Longitude = 4.47917
                        },
                        Banner = new ImageFile
                        {
                            Url = "https://banner2.cleanpng.com/20180714/buk/kisspng-india-medplus-business-retail-pharmacy-logo-mcdonald-5b4a5d7662d712.2249596115316002464049.jpg",
                            AltText = "McDonald's banner",
                            MimeType = "image/jpeg",
                            FileSize = 0.1
                        },
                        Owner = seedUsers[0],
                        OpeningsTimes = new List<OpeningsTime>
                        {
                            new () { Opening = new TimeOnly(8, 30), Closing = new TimeOnly(22, 30), Day = Day.Monday },
                            new () { Opening = new TimeOnly(8, 30), Closing = new TimeOnly(22, 30), Day = Day.Tuesday },
                            new () { Opening = new TimeOnly(8, 30), Closing = new TimeOnly(22, 30), Day = Day.Wednesday },
                            new () { Opening = new TimeOnly(8, 30), Closing = new TimeOnly(22, 30), Day = Day.Thursday },
                            new () { Opening = new TimeOnly(8, 30), Closing = new TimeOnly(21, 30), Day = Day.Friday },
                        }
                    },
                    new()
                    {
                        Name = "Burger King",
                        PhoneNumber = "987654321",
                        AboutUs = "Home of the Whopper",
                        Radius = 5,
                        Logo = new ImageFile
                        {
                            Url = "https://upload.wikimedia.org/wikipedia/commons/c/cc/Burger_King_2020.svg",
                            AltText = "Burger King logo",
                            MimeType = "image/png",
                            FileSize = 0.15
                        },
                        Address = new Address
                        {
                            City = "Amsterdam",
                            HouseNumber = "45",
                            StreetName = "Damrak",
                            ZipCode = "1012LP",
                            Latitude = 52.3738,
                            Longitude = 4.8910
                        },
                        Banner = new ImageFile
                        {
                            Url = "https://upload.wikimedia.org/wikipedia/commons/c/cc/Burger_King_2020.svg",
                            AltText = "Burger King banner",
                            MimeType = "image/jpeg",
                            FileSize = 0.2
                        },
                        Owner = seedUsers[1],
                        OpeningsTimes = new List<OpeningsTime>
                        {
                            new () { Opening = new TimeOnly(9, 00), Closing = new TimeOnly(23, 00), Day = Day.Monday },
                            new () { Opening = new TimeOnly(9, 00), Closing = new TimeOnly(23, 00), Day = Day.Tuesday },
                            new () { Opening = new TimeOnly(9, 00), Closing = new TimeOnly(23, 00), Day = Day.Wednesday },
                            new () { Opening = new TimeOnly(9, 00), Closing = new TimeOnly(23, 00), Day = Day.Thursday },
                            new () { Opening = new TimeOnly(9, 00), Closing = new TimeOnly(22, 30), Day = Day.Friday },
                        }
                    },
                };

                context.Restaurants.AddRange(seedRestaurants);
                context.SaveChanges();
            }
        }
    }
}

using AllYourGoods.Api.Models;
using AllYourGoods.Api.Models.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace AllYourGoods.Api.Data;

public class DbInitializer
{
    public static async Task Initialize(ApplicationContext context, RoleManager<IdentityRole> roleManager, UserManager<User> userManager)
    {
        context.Database.Migrate();

        if (context.Restaurants.Any())
        {
            return;
        }

        foreach (Roles role in Enum.GetValues(typeof(Roles))) 
        {
            var exists = await roleManager.RoleExistsAsync(role.ToString());
            
            if (!exists)
            {
                await roleManager.CreateAsync(new IdentityRole(role.ToString()));
            }
        }

        var user = new User
        {
            UserName = "JamesLame",
            Email = "LameyJamey@email.com"
        };

        await userManager.CreateAsync(user, "LamePassword1!");
        await userManager.AddToRoleAsync(user, Roles.teamhr.ToString());


        var restaurants = new Restaurant[]
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
                Owner = context.Users.FirstOrDefault<User>(u => u.UserName == user.UserName),
                OpeningsTimes = new List<OpeningsTime>()
                {
                    new ()
                    {
                        Opening = new TimeOnly(8, 30),
                        Closing = new TimeOnly(22, 30),
                        Day = Day.Monday
                    },
                    new ()
                    {
                        Opening = new TimeOnly(8, 30),
                        Closing = new TimeOnly(22, 30),
                        Day = Day.Tuesday
                    },
                    new ()
                    {
                        Opening = new TimeOnly(8, 30),
                        Closing = new TimeOnly(22, 30),
                        Day = Day.Wednesday
                    },
                    new ()
                    {
                        Opening = new TimeOnly(8, 30),
                        Closing = new TimeOnly(22, 30),
                        Day = Day.Thursday
                    },
                    new ()
                    {
                        Opening = new TimeOnly(8, 30),
                        Closing = new TimeOnly(21, 30),
                        Day = Day.Friday
                    },
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
                Owner = context.Users.FirstOrDefault<User>(u => u.UserName == user.UserName),
                OpeningsTimes = new List<OpeningsTime>()
                {
                    new () { Opening = new TimeOnly(9, 00), Closing = new TimeOnly(23, 00), Day = Day.Monday },
                    new () { Opening = new TimeOnly(9, 00), Closing = new TimeOnly(23, 00), Day = Day.Tuesday },
                    new () { Opening = new TimeOnly(9, 00), Closing = new TimeOnly(23, 00), Day = Day.Wednesday },
                    new () { Opening = new TimeOnly(9, 00), Closing = new TimeOnly(23, 00), Day = Day.Thursday },
                    new () { Opening = new TimeOnly(9, 00), Closing = new TimeOnly(23, 00), Day = Day.Friday },
                }
            },
            new()
            {
                Name = "KFC",
                PhoneNumber = "456789123",
                AboutUs = "Finger Lickin' Good",
                Radius = 5,
                Logo = new ImageFile
                {
                    Url = "https://commons.wikimedia.org/wiki/File:Kentucky_Fried_Chicken_201x_logo.svg",
                    AltText = "KFC logo",
                    MimeType = "image/svg+xml",
                    FileSize = 0.05
                },
                Address = new Address
                {
                    City = "The Hague",
                    HouseNumber = "88",
                    StreetName = "Grote Markt",
                    ZipCode = "2511BJ",
                    Latitude = 52.0800,
                    Longitude = 4.3100
                },
                Banner = new ImageFile
                {
                    Url = "https://commons.wikimedia.org/wiki/File:Kentucky_Fried_Chicken_201x_logo.svg",
                    AltText = "KFC banner",
                    MimeType = "image/jpeg",
                    FileSize = 0.3
                },
                Owner = user,
                OpeningsTimes = new List<OpeningsTime>()
                {
                    new () { Opening = new TimeOnly(10, 00), Closing = new TimeOnly(22, 00), Day = Day.Monday },
                    new () { Opening = new TimeOnly(10, 00), Closing = new TimeOnly(22, 00), Day = Day.Tuesday },
                    new () { Opening = new TimeOnly(10, 00), Closing = new TimeOnly(22, 00), Day = Day.Wednesday },
                    new () { Opening = new TimeOnly(10, 00), Closing = new TimeOnly(22, 00), Day = Day.Thursday },
                    new () { Opening = new TimeOnly(10, 00), Closing = new TimeOnly(22, 00), Day = Day.Friday },
                }
            }
        };

        context.Restaurants.AddRange(restaurants);
        context.SaveChanges();
    }
}

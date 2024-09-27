using AllYourGoods.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace AllYourGoods.Api.Data;

public class DbInitializer
{
    public static void Initialize(ApplicationContext context)
    {
        context.Database.Migrate();

        if (context.Restaurants.Any())
        {
            return;
        }

        var restaurants = new Restaurant[]
        {
            new ()
            {
                Id = Guid.NewGuid(),
                OpeningTime = new TimeOnly(8, 0),
                ClosingTime = new TimeOnly(22, 0),
                StreetName = "Kerkstraat",
                HouseNumber = "1",
                Name = "De Klok",
                Description = "Gezellig café",
                Tags = new List<Tag> {new ("Cafe"), new ("Lokaal")},
                Radius = 1,
                ImageLink = "https://www.google.com"
            },
            new ()
            {
                Id = Guid.NewGuid(),
                OpeningTime = new TimeOnly(9, 0),
                ClosingTime = new TimeOnly(23, 0),
                StreetName = "Kerkstraat",
                HouseNumber = "2",
                Name = "De Kroeg",
                Description = "Gezellig café",
                Tags = new List<Tag> {new ("Cafe")},
                Radius = 1,
                ImageLink = "https://www.google.com"
            },
            new ()
            {
                Id = Guid.NewGuid(),
                OpeningTime = new TimeOnly(10, 0),
                ClosingTime = new TimeOnly(0, 0),
                StreetName = "Kerkstraat",
                HouseNumber = "3",
                Name = "De Pub",
                Description = "Gezellig café",
                Tags = new List<Tag> {new ("Cafe")},
                Radius = 1,
                ImageLink = "https://www.google.com"
            }
        };

        context.Restaurants.AddRange(restaurants);
        context.SaveChanges();
    }
}

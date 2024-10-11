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
        };

        context.Restaurants.AddRange(restaurants);
        context.SaveChanges();
    }
}

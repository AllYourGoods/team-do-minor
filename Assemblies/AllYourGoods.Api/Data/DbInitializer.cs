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

        // create user with id 3fa85f64-5717-4562-b3fc-2c963f66afa6

        context.Restaurants.AddRange(restaurants);
        context.SaveChanges();
    }
}

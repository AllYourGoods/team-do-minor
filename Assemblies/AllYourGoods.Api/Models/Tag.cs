namespace AllYourGoods.Api.Models;

public class Tag
{ 
    public Tag()
    {
    }
    public Tag(string name)
    {
        Name = name;
    }

    public Guid Id { get; set; }
    public string Name { get; set; } = null!;

    // Navigation property to establish many-to-many relationship with Restaurant
    public ICollection<Restaurant> Restaurants { get; set; } = new List<Restaurant>();
}


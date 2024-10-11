using System.ComponentModel.DataAnnotations;
using AllYourGoods.Api.Models.Enums;

namespace AllYourGoods.Api.Models;

public class User: BaseEntity
{
    [StringLength(255)]
    public string? Name { get; set; }
    public Role? Role { get; set; }

    [StringLength(255)] 
    public string Email { get; set; } = null!;

    [StringLength(255)] 
    public string PasswordHash { get; set; } = null!;

    [StringLength(255)] 
    public string PasswordSalt { get; set; } = null!;

    public Guid RestaurantId { get; set; }

    // Navigation properties
    public virtual ICollection<Restaurant> Restaurant { get; set; } = null!;
}

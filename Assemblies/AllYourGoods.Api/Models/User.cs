using System.ComponentModel.DataAnnotations;
using AllYourGoods.Api.Models.Enums;

namespace AllYourGoods.Api.Models;

public class User: BaseEntity
{
    public Guid? RestaurantId { get; set; }

    [StringLength(255)]
    public string Name { get; set; }
    public Role Role { get; set; }
    [StringLength(255)] 
    public string Email { get; set; } = null!;

    [StringLength(255)] 
    public string PasswordHash { get; set; } = null!;

    [StringLength(255)] 
    public string PasswordSalt { get; set; } = null!;

    public virtual Restaurant Restaurant { get; set; }
    public virtual DeliveryPerson DeliveryPerson { get; set; }

    // Many-to-many relationship with Roles
    public virtual ICollection<UserRoles> UserRoles { get; set; }
}

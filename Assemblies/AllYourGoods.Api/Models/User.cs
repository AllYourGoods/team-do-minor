using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace AllYourGoods.Api.Models;

public class User : IdentityUser {
    public Guid? RestaurantId { get; set; }
    public virtual Restaurant? Restaurant { get; set; }
    public virtual ICollection<Shift>? Shifts { get; set; }  // Ensure this property exists

    public virtual DeliveryPerson? DeliveryPerson { get; set; }

}

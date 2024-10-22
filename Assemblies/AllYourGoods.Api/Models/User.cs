using System.ComponentModel.DataAnnotations;
using AllYourGoods.Api.Models.Enums;

namespace AllYourGoods.Api.Models
{
    public class User : BaseEntity
    {
        public Guid? RestaurantId { get; set; }

        [StringLength(255)]
        public string? Name { get; set; }

        public Role? Role { get; set; }

        [StringLength(255)]
        public string Email { get; set; } = null!;

        [StringLength(255)]
        public string PasswordHash { get; set; } = null!;

        [StringLength(255)]
        public string PasswordSalt { get; set; } = null!;

        public virtual Restaurant? Restaurant { get; set; }
        public virtual ICollection<Shift> Shifts { get; set; }  // Ensure this property exists

        public virtual DeliveryPerson? DeliveryPerson { get; set; }

        public virtual ICollection<UserRoles> UserRoles { get; set; } = new List<UserRoles>();
    }
}
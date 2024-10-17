using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AllYourGoods.Api.Models
{
    public class Roles : BaseEntity
    {
        [StringLength(255)]
        public string Name { get; set; } = null!;

        [StringLength(255)]
        public string NormalizedName { get; set; } = null!;

        public string ConcurrencyStamp { get; set; } = null!;

        public virtual ICollection<UserRoles> UserRoles { get; set; } = new List<UserRoles>();
    }
}
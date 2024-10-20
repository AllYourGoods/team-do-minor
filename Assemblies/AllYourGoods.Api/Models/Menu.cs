using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AllYourGoods.Api.Models
{
    public class Menu : BaseEntity
    {
        [StringLength(255)]
        public string Name { get; set; } = null!;

        public bool Active { get; set; }
        public Guid RestaurantId { get; set; }
        public virtual Restaurant Restaurant { get; set; } = null!;

        public virtual ICollection<Category> Categories { get; set; } = new List<Category>();
    }
}
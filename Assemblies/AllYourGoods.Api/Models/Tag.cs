using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AllYourGoods.Api.Models
{
    public class Tag : BaseEntity
    {
        [StringLength(255)]
        public string Name { get; set; } = null!;

        public virtual ICollection<ProductHasTag> ProductTags { get; set; } = new List<ProductHasTag>();
        public virtual ICollection<RestaurantHasTags> RestaurantTags { get; set; } = new List<RestaurantHasTags>();
    }
}
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AllYourGoods.Api.Models
{
    public class Category : BaseEntity
    {
        [Range(0, double.MaxValue)]
        public double Weight { get; set; }

        [StringLength(255)] 
        public string Name { get; set; } = null!;

        public Guid MenuId { get; set; }
        public virtual Menu Menu { get; set; } = null!; 

        public virtual ICollection<CategoryHasProduct> CategoryProducts { get; set; } = new List<CategoryHasProduct>();
    }
}
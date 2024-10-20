using System.ComponentModel.DataAnnotations;

namespace AllYourGoods.Api.Models
{
    public class FrequentlyAskedQuestion : BaseEntity
    {
        [Required]
        public string Question { get; set; } = null!;

        [Required]
        public string Answer { get; set; } = null!;

        public Guid RestaurantId { get; set; }
        public virtual Restaurant Restaurant { get; set; } = null!;
    }
}
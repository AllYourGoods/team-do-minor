using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AllYourGoods.Api.Models
{
    public class Shift : BaseEntity
    {
        public TimeSpan? StartTime { get; set; }
        public TimeSpan? EndTime { get; set; }
        public double? MaxDistance { get; set; }

        [Required]
        public string WayOfTransport { get; set; } = null!;

        public string UserId { get; set; } = null!;
        [Required]
        public virtual User User { get; set; } = null!;
        public virtual ICollection<Order>? Orders { get; set; }  // Optional: One-to-many with Orders
    }
}

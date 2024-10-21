using System.ComponentModel.DataAnnotations;

namespace AllYourGoods.Api.Models
{
    public class Shift
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }

        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public double MaxDistance { get; set; }
        
        [Required]
        public string WayOfTransport { get; set; } = null!;

        [Required]
        public virtual User User { get; set; } = null!;
        
        public virtual ICollection<Order>? Orders { get; set; }  // Optional: One-to-many with Orders
    }
}

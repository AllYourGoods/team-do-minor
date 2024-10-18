using System.ComponentModel.DataAnnotations;

namespace AllYourGoods.Api.Models
{
    public class DeliveryPerson : BaseEntity
    {
        public Guid UserId { get; set; }

        [StringLength(255)]
        public string Region { get; set; } = null!;

        public double MaxDistance { get; set; }

        [StringLength(50)] 
        public string WayOfTransport { get; set; } = null!;

        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public TimeSpan EstimatedTime { get; set; }

        public virtual User User { get; set; } = null!;
    }
}
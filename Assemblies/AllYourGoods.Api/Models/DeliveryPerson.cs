using System.ComponentModel.DataAnnotations;

namespace AllYourGoods.Api.Models
{
    public class DeliveryPerson : BaseEntity
    {
        // [Key]
        // public new string Id { get; set; } = null!;

        [StringLength(255)]
        public string Region { get; set; } = null!;

        public TimeSpan EstimatedTime { get; set; }

        [Required]
        public string UserId { get; set; } = null!; 
        public virtual User User { get; set; } = null!;
    }
}
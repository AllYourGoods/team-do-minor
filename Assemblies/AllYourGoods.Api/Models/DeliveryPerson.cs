namespace AllYourGoods.Api.Models
{
    public class DeliveryPerson : BaseEntity
    {
        public Guid UserId { get; set; }
        public string Region { get; set; }
        public double MaxDistance { get; set; }
        public string WayOfTransport { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public TimeSpan EstimatedTime { get; set; }

        public virtual User User { get; set; }
    }

}

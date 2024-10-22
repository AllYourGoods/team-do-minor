namespace AllYourGoods.Api.Models
{
    public class Shift
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }

        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public double MaxDistance { get; set; }
        public string WayOfTransport { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<Order> Orders { get; set; }  // Optional: One-to-many with Orders
    }
}

namespace AllYourGoods.Api.Models
{
    public class Roles
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;
        public string NormalizedName { get; set; } = string.Empty;
        public string ConcurrencyStamp { get; set; } = Guid.NewGuid().ToString();

        // Initialize collection
        public virtual ICollection<UserRoles> UserRoles { get; set; } = new List<UserRoles>();
    }
}
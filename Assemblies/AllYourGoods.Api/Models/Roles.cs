namespace AllYourGoods.Api.Models
{
    public class Roles
    {
        public string Id { get; set; } 
        public string Name { get; set; } 
        public string NormalizedName { get; set; } 
        public string ConcurrencyStamp { get; set; } 

        public virtual ICollection<UserRoles> UserRoles { get; set; }
    }

}

namespace AllYourGoods.Api.Models
{
    public class Roles
    {
        public string Id { get; set; } // NVARCHAR(450)
        public string Name { get; set; } // NVARCHAR(256)
        public string NormalizedName { get; set; } // NVARCHAR(256)
        public string ConcurrencyStamp { get; set; } // NVARCHAR(MAX)

        public virtual ICollection<UserRoles> UserRoles { get; set; } // Many-to-many relationship
    }

}

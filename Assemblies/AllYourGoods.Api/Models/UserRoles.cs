using AllYourGoods.Api.Models.Enums;

namespace AllYourGoods.Api.Models
{
    public class UserRoles : BaseEntity
    {
        public Guid UserId { get; set; }
        public virtual User User { get; set; } = null!;

        public Guid RoleId { get; set; }
        public virtual Roles Role { get; set; } = null!;
    }

}
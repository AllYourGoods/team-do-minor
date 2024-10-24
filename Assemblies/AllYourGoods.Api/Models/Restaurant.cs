using System.ComponentModel.DataAnnotations;

namespace AllYourGoods.Api.Models
{
    public class Restaurant : BaseEntity
    {
        [StringLength(255)]
        public string Name { get; set; } = null!;

        [StringLength(15)]
        public string PhoneNumber { get; set; } = null!;
        public string? AboutUs { get; set; }
        public double? Radius { get; set; }

        public Guid? LogoId { get; set; }
        public Guid? AddressId { get; set; }
        public Guid? BannerId { get; set; }
        public string? OwnerId { get; set; }

        public virtual ImageFile Logo { get; set; } = null!;
        public virtual Address Address { get; set; } = null!;
        public virtual ImageFile Banner { get; set; } = null!;
        public virtual User? Owner { get; set; }

        public virtual ICollection<OpeningsTime> OpeningsTimes { get; set; } = new List<OpeningsTime>();

        public virtual ICollection<Menu> Menus { get; set; } = new List<Menu>();
        public virtual ICollection<RestaurantHasTags> RestaurantTags { get; set; } = new List<RestaurantHasTags>();
        public virtual ICollection<FrequentlyAskedQuestion> FAQs { get; set; } = new List<FrequentlyAskedQuestion>();
    }
}
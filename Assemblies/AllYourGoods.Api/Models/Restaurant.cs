using System.ComponentModel.DataAnnotations;

namespace AllYourGoods.Api.Models;

public class Restaurant : BaseEntity
{
    [StringLength(255)]
    public string? Name { get; set; }
    [StringLength(15)]
    public string? PhoneNumber { get; set; }
    public string? AboutUs { get; set; }
    public double? Radius { get; set; }

    public Guid? LogoId { get; set; }
    public Guid? AddressId { get; set; }
    public Guid? BannerId { get; set; }
    public Guid? OwnerId { get; set; }

    // Navigation properties
    public virtual ImageFile? Logo { get; set; }
    public virtual Address? Address { get; set; }
    public virtual ImageFile? Banner { get; set; }
    public virtual User? Owner { get; set; }
    public virtual ICollection<OpeningsTime>? OpeningsTimes { get; set; }
}

using AllYourGoods.Api.Interfaces.Model;

namespace AllYourGoods.Api.Models;

public abstract class BaseEntity : IEntity
{
    public Guid Id { get; set; }
}
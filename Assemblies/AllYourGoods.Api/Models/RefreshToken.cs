using System.ComponentModel.DataAnnotations.Schema;

namespace AllYourGoods.Api.Models;

public class RefreshToken
{
    public string Id { get; set; }
    public string Token { get; set; }
    public DateTime ExpirationDate { get; set; }
    [ForeignKey("User")]
    public string UserFK { get; set; }
    public User User { get; set; }
}
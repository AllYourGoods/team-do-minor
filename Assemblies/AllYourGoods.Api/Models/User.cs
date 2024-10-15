using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AllYourGoods.Api.Models.Enums;

namespace AllYourGoods.Api.Models
{
    public class User : BaseEntity
    {
        public Guid? RestaurantId { get; set; }  

        [StringLength(255)]
        public string Name { get; set; } = string.Empty; 

        public Role? Role { get; set; }  

        [StringLength(255)]
        public string Email { get; set; } = string.Empty; 
        [StringLength(255)]
        public string PasswordHash { get; set; } = string.Empty; 

        [StringLength(255)]
        public string PasswordSalt { get; set; } = string.Empty;  

        public virtual Restaurant? Restaurant { get; set; } 
        public virtual DeliveryPerson? DeliveryPerson { get; set; }  

        public virtual ICollection<UserRoles>? UserRoles { get; set; } = new List<UserRoles>();  
    }
}
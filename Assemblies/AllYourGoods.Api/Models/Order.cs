using AllYourGoods.Api.Interfaces.Model;
using System;
using System.Collections.Generic;
using AllYourGoods.Api.Models.Enums;

namespace AllYourGoods.Api.Models
{
    public class Order : BaseEntity
    {
        public Guid RestaurantId { get; set; }
        public string CustomerId { get; set; }
        public Guid AddressId { get; set; }
        public Guid DeliveryPersonId { get; set; }

        public decimal TotalPrice { get; set; }
        public string Note { get; set; } = null!;
        public decimal ETA { get; set; }
        public Guid? ShiftId { get; set; }  // Foreign key to Shift table

        public PaymentMethod PaymentMethod { get; set; }
        public OrderStatus Status { get; set; }

        public Address Address { get; set; } = null!;
        public Restaurant Restaurant { get; set; } = null!;
        public User Customer { get; set; } = null!;
        public DeliveryPerson DeliveryPerson { get; set; } = null!;
        public virtual Shift Shift { get; set; } = null!;// Navigation to Shift


        public List<OrderHasProduct> OrderHasProductList { get; set; } = null!;
    }
}

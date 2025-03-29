using System;
using System.Collections.Generic;

namespace Infrastructure.DataModels;

public partial class Basket
{
    public Guid Id { get; set; }

    public string UserId { get; set; } = null!;

    public virtual ICollection<TourBasketItem> TourBasketItem { get; set; } = new List<TourBasketItem>();

    public virtual Aspnetuser User { get; set; } = null!;
}

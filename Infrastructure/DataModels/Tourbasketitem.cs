using System;
using System.Collections.Generic;

namespace Infrastructure.DataModels;

public partial class TourBasketItem
{
    public Guid BasketId { get; set; }

    public Guid TourScheduleId { get; set; }

    public Guid TicketTypeId { get; set; }

    public int Quantity { get; set; }

    public virtual Basket Basket { get; set; } = null!;

    public virtual TourSchedule TourSchedule { get; set; } = null!;
}

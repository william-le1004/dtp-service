using System;
using System.Collections.Generic;

namespace Infrastructure.DataModels;

public partial class Ticket
{
    public Guid Id { get; set; }

    public string Code { get; set; } = null!;

    public Guid TicketTypeId { get; set; }

    public int Quantity { get; set; }

    public decimal GrossCost { get; set; }

    public Guid TourBookingId { get; set; }

    public virtual TicketType TicketType { get; set; } = null!;

    public virtual TourBooking TourBooking { get; set; } = null!;
}

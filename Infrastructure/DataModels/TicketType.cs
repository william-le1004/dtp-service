using System;
using System.Collections.Generic;

namespace Infrastructure.DataModels;

public partial class TicketType
{
    public Guid Id { get; set; }

    public decimal DefaultNetCost { get; set; }

    public int MinimumPurchaseQuantity { get; set; }

    public int TicketKind { get; set; }

    public Guid TourId { get; set; }

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();

    public virtual Tour Tour { get; set; } = null!;

    public virtual ICollection<TourScheduleTicket> TourScheduleTickets { get; set; } = new List<TourScheduleTicket>();
}

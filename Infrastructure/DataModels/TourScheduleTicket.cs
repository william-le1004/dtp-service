using System;
using System.Collections.Generic;

namespace Infrastructure.DataModels;

public partial class TourScheduleTicket
{
    public Guid TicketTypeId { get; set; }

    public Guid TourScheduleId { get; set; }

    public decimal NetCost { get; set; }

    public int Capacity { get; set; }

    public bool IsDeleted { get; set; }

    public virtual TicketType TicketType { get; set; } = null!;

    public virtual TourSchedule TourSchedule { get; set; } = null!;
}

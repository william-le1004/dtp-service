using System;
using System.Collections.Generic;

namespace Infrastructure.DataModels;

public partial class TourSchedule
{
    public Guid Id { get; set; }

    public Guid TourId { get; set; }

    public DateTime OpenDate { get; set; }

    public DateTime CloseDate { get; set; }

    public double PriceChangeRate { get; set; }

    public string? Remark { get; set; }

    public bool IsDeleted { get; set; }

    public DateTime CreatedAt { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? LastModified { get; set; }

    public string? LastModifiedBy { get; set; }

    public virtual Tour Tour { get; set; } = null!;

    public virtual ICollection<TourBasketItem> TourBasketItem { get; set; } = new List<TourBasketItem>();

    public virtual ICollection<TourBooking> TourBookings { get; set; } = new List<TourBooking>();

    public virtual ICollection<TourScheduleTicket> TourScheduleTickets { get; set; } = new List<TourScheduleTicket>();
}

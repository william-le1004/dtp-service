using System;
using System.Collections.Generic;

namespace Infrastructure.DataModels;

public partial class Payment
{
    public Guid Id { get; set; }

    public Guid BookingId { get; set; }

    public int Method { get; set; }

    public int Status { get; set; }

    public string? RefTransactionCode { get; set; }

    public string? PaymentLinkId { get; set; }

    public bool IsDeleted { get; set; }

    public DateTime CreatedAt { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? LastModified { get; set; }

    public string? LastModifiedBy { get; set; }

    public virtual TourBooking Booking { get; set; } = null!;
}

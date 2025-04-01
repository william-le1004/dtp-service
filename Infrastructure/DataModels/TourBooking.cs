using System;
using System.Collections.Generic;

namespace Infrastructure.DataModels;

public partial class TourBooking
{
    public Guid Id { get; set; }

    public string UserId { get; set; } = null!;

    public string Code { get; set; } = null!;

    public long RefCode { get; set; }

    public string Name { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public string Email { get; set; } = null!;

    public Guid TourScheduleId { get; set; }

    public string? VoucherCode { get; set; }

    public decimal DiscountAmount { get; set; }

    public int Status { get; set; }

    public string? Remark { get; set; }

    public bool IsDeleted { get; set; }

    public DateTime CreatedAt { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? LastModified { get; set; }

    public string? LastModifiedBy { get; set; }

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();

    public virtual TourSchedule TourSchedule { get; set; } = null!;

    public virtual Voucher? VoucherCodeNavigation { get; set; }
}

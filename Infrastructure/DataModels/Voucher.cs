using System;
using System.Collections.Generic;

namespace Infrastructure.DataModels;

public partial class Voucher
{
    public string Code { get; set; } = null!;

    public decimal MaxDiscountAmount { get; set; }

    public double Percent { get; set; }

    public DateTime ExpiryDate { get; set; }

    public virtual ICollection<TourBooking> Tourbookings { get; set; } = new List<TourBooking>();
}

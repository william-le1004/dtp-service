using System;
using System.Collections.Generic;

namespace Infrastructure.DataModels;

public partial class ExternalTransaction
{
    public Guid Id { get; set; }

    public string UserId { get; set; } = null!;

    public string ExternalTransactionCode { get; set; } = null!;

    public string TransactionCode { get; set; } = null!;

    public string? Description { get; set; }

    public decimal Amount { get; set; }

    public int Type { get; set; }

    public int Status { get; set; }

    public bool IsDeleted { get; set; }

    public DateTime CreatedAt { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? LastModified { get; set; }

    public string? LastModifiedBy { get; set; }

    public virtual Aspnetuser User { get; set; } = null!;
}

using System;
using System.Collections.Generic;

namespace Infrastructure.DataModels;

public partial class Transaction
{
    public Guid Id { get; set; }

    public Guid WalletId { get; set; }

    public string TransactionCode { get; set; } = null!;

    public string? Description { get; set; }

    public string RefTransactionCode { get; set; } = null!;

    public decimal AfterTransactionBalance { get; set; }

    public decimal Amount { get; set; }

    public int Type { get; set; }

    public int Status { get; set; }

    public bool IsDeleted { get; set; }

    public DateTime CreatedAt { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? LastModified { get; set; }

    public string? LastModifiedBy { get; set; }

    public virtual Wallet Wallet { get; set; } = null!;
}

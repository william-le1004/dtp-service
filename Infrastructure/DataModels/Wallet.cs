using System;
using System.Collections.Generic;

namespace Infrastructure.DataModels;

public partial class Wallet
{
    public Guid Id { get; set; }

    public string UserId { get; set; } = null!;

    public decimal Balance { get; set; }

    public bool IsDeleted { get; set; }

    public DateTime CreatedAt { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? LastModified { get; set; }

    public string? LastModifiedBy { get; set; }

    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();

    public virtual Aspnetuser User { get; set; } = null!;
}

using System;
using System.Collections.Generic;

namespace Infrastructure.DataModels;

public partial class Rating
{
    public Guid Id { get; set; }

    public Guid TourId { get; set; }

    public string UserId { get; set; } = null!;

    public int Star { get; set; }

    public string Comment { get; set; } = null!;

    public bool IsDeleted { get; set; }

    public DateTime CreatedAt { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? LastModified { get; set; }

    public string? LastModifiedBy { get; set; }

    public virtual Tour Tour { get; set; } = null!;

    public virtual Aspnetuser User { get; set; } = null!;
}

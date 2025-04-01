using System;
using System.Collections.Generic;

namespace Infrastructure.DataModels;

public partial class Category
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public bool IsDeleted { get; set; }

    public DateTime CreatedAt { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? LastModified { get; set; }

    public string? LastModifiedBy { get; set; }

    public virtual ICollection<Tour> Tours { get; set; } = new List<Tour>();
}

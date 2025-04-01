using System;
using System.Collections.Generic;

namespace Infrastructure.DataModels;

public partial class Company
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public string TaxCode { get; set; } = null!;

    public bool Licensed { get; set; }

    public bool IsDeleted { get; set; }

    public DateTime CreatedAt { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? LastModified { get; set; }

    public string? LastModifiedBy { get; set; }

    public virtual ICollection<Aspnetuser> Aspnetusers { get; set; } = new List<Aspnetuser>();

    public virtual ICollection<Tour> Tours { get; set; } = new List<Tour>();
}

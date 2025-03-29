using System;
using System.Collections.Generic;

namespace Infrastructure.DataModels;

public partial class Destination
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string Latitude { get; set; } = null!;

    public string Longitude { get; set; } = null!;

    public bool IsDeleted { get; set; }

    public DateTime CreatedAt { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? LastModified { get; set; }

    public string? LastModifiedBy { get; set; }

    public virtual ICollection<TourDestination> Tourdestinations { get; set; } = new List<TourDestination>();
}

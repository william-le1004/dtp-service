using System;
using System.Collections.Generic;

namespace Infrastructure.DataModels;

public partial class DestinationActivity
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public Guid TourDestinationId { get; set; }

    public TimeSpan StartTime { get; set; }

    public TimeSpan EndTime { get; set; }

    public int? SortOrder { get; set; }

    public virtual TourDestination TourDestination { get; set; } = null!;
}

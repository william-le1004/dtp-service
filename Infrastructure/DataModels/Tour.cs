using System;
using System.Collections.Generic;

namespace Infrastructure.DataModels;

public partial class Tour
{
    public Guid Id { get; set; }

    public string Title { get; set; } = null!;

    public Guid? CompanyId { get; set; }

    public Guid? CategoryId { get; set; }

    public string? Description { get; set; }

    public string? Code { get; set; }

    public bool IsDeleted { get; set; }

    public DateTime CreatedAt { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? LastModified { get; set; }

    public string? LastModifiedBy { get; set; }

    public virtual Category? Category { get; set; }

    public virtual Company? Company { get; set; }

    public virtual ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();

    public virtual ICollection<Rating> Ratings { get; set; } = new List<Rating>();

    public virtual ICollection<TicketType> Tickettypes { get; set; } = new List<TicketType>();

    public virtual ICollection<TourDestination> Tourdestinations { get; set; } = new List<TourDestination>();

    public virtual ICollection<TourSchedule> Tourschedules { get; set; } = new List<TourSchedule>();
}

using System;
using System.Collections.Generic;

namespace Infrastructure.DataModels;

public partial class Imageurl
{
    public Guid Id { get; set; }

    public Guid RefId { get; set; }

    public string Url { get; set; } = null!;
}

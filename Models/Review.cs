﻿using System;
using System.Collections.Generic;

namespace RoadReady.Models;

public partial class Review
{
    public int ReviewId { get; set; }

    public int UserId { get; set; }

    public int CarId { get; set; }

    public int? Rating { get; set; }

    public string? Comment { get; set; }

    public DateOnly ReviewDate { get; set; }

    public virtual Car Car { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}

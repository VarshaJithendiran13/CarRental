﻿using System;
using System.Collections.Generic;

namespace RoadReady.Models;

public partial class AdminReport
{
    public int ReportId { get; set; }

    public DateOnly ReportDate { get; set; }

    public int? TotalReservations { get; set; }

    public decimal? TotalRevenue { get; set; }

    public string? TopCars { get; set; }

    public string? MostActiveUser { get; set; }
}
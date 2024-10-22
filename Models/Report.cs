using System;
using System.Collections.Generic;

namespace ProjectLibrary_Back.Models;

public partial class Report
{
    public int Id { get; set; }

    public int? UserId { get; set; }

    public string ReportType { get; set; } = null!;

    public DateOnly? GeneratedDate { get; set; }

    public string ReportData { get; set; } = null!;

    public string Description { get; set; } = null!;

    public virtual User? User { get; set; }
}

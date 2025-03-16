using System;
using System.Collections.Generic;

namespace FinalPRN221.Models;

public partial class AbsentReason
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Attendance> Attendances { get; set; } = new List<Attendance>();
}

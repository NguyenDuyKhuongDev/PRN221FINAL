using System;
using System.Collections.Generic;

namespace FinalPRN221.Models;

public partial class AttendanceStatus
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Attendance> Attendances { get; set; } = new List<Attendance>();
}

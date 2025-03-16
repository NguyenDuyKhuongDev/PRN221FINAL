using System;
using System.Collections.Generic;

namespace FinalPRN221.Models;

public partial class Attendance
{
    public int AttendanceId { get; set; }

    public string EmployeeId { get; set; } = null!;

    public DateOnly AttendanceDate { get; set; }

    public int AbsentReasonId { get; set; }

    public int StatusId { get; set; }

    public TimeOnly CheckInTime { get; set; }

    public TimeOnly CheckOutTime { get; set; }

    public decimal WorkHours { get; set; }

    public decimal? OverTimeHours { get; set; }

    public string? Notes { get; set; }

    public int? ApprovedBy { get; set; }

    public bool? IsApproved { get; set; }

    public virtual AbsentReason AbsentReason { get; set; } = null!;

    public virtual AttendanceStatus Status { get; set; } = null!;
}

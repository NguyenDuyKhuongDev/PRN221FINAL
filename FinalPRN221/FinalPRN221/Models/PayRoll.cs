using System;
using System.Collections.Generic;

namespace FinalPRN221.Models;

public partial class PayRoll
{
    public int PayrollId { get; set; }

    public string EmployeeId { get; set; } = null!;

    public int StatusId { get; set; }

    public decimal? BasicSalary { get; set; }

    public decimal? Allowance { get; set; }

    public decimal? Bonus { get; set; }

    public decimal? FineSalary { get; set; }

    public decimal? OverTimePay { get; set; }

    public DateOnly? PayrollDate { get; set; }

    public int? Month { get; set; }

    public int? Year { get; set; }

    public string? Note { get; set; }

    public string UpdateBy { get; set; } = null!;

    public virtual PayrollStatus Status { get; set; } = null!;
}

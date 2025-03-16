using System;
using System.Collections.Generic;

namespace FinalPRN221.Models;

public partial class PayrollStatus
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<PayRoll> PayRolls { get; set; } = new List<PayRoll>();
}

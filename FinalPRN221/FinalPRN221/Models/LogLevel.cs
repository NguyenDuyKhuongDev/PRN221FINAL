using System;
using System.Collections.Generic;

namespace FinalPRN221.Models;

public partial class LogLevel
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Log> Logs { get; set; } = new List<Log>();
}

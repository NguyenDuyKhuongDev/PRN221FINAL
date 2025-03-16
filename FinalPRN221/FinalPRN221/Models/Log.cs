using System;
using System.Collections.Generic;

namespace FinalPRN221.Models;

public partial class Log
{
    public int LogId { get; set; }

    public int LogLevelId { get; set; }

    public string UserId { get; set; } = null!;

    public DateOnly TimeStamp { get; set; }

    public string? Message { get; set; }

    public int ActionId { get; set; }

    public string? Ipadress { get; set; }

    public virtual LogAction Action { get; set; } = null!;

    public virtual LogLevel LogLevel { get; set; } = null!;
}

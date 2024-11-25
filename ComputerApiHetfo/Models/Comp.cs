using System;
using System.Collections.Generic;

namespace ComputerApiHetfo.Models;

public partial class Comp
{
    public Guid id { get; set; }

    public string? brand { get; set; }

    public string? type { get; set; }

    public double? display { get; set; }

    public int? memory { get; set; }

    public DateTime? createdat { get; set; }

    public Guid? osid { get; set; }

    public virtual Osystem? Os { get; set; }
}

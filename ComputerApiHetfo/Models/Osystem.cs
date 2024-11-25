using System;
using System.Collections.Generic;

namespace ComputerApiHetfo.Models;

public partial class Osystem
{
    public int id { get; set; }

    public string? name { get; set; }

    public virtual ICollection<Comp> Comps { get; set; } = new List<Comp>();
}

using System;
using System.Collections.Generic;

namespace PoolGameAPI.modules;

public partial class ResultType
{
    public int IdresultTypes { get; set; }

    public string? ResultType1 { get; set; }

    public virtual ICollection<GameRecord> GameRecords { get; set; } = new List<GameRecord>();
}

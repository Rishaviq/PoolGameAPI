using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace PoolGameAPI.modules;

public partial class GameRecord
{
    public int IdgameRecords { get; set; }

    public string GameRecordsGameId { get; set; } = null!;

    public int GameRecordsPlayer { get; set; }

    public int GameRecordsResult { get; set; }

    public int? GameRecordsShotsMade { get; set; }

    public int? GameRecordsShotAttempted { get; set; }

    public int? GameRecordsHandball { get; set; }

    public int? GameRecordsFouls { get; set; }

    public int? GameRecordsBestStreak { get; set; }
    
    public virtual UserAccout GameRecordsPlayerNavigation { get; set; } = null!;
    
    public virtual ResultType GameRecordsResultNavigation { get; set; } = null!;
}

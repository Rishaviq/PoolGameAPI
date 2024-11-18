using System;
using System.Collections.Generic;

namespace PoolGameAPI.modules;

public partial class UserAccout
{
    public int IduserAccouts { get; set; }

    public string UserAccoutsUsername { get; set; } = null!;

    public string UserAccoutsPassword { get; set; } = null!;

    public virtual ICollection<GameRecord> GameRecords { get; set; } = new List<GameRecord>();

    public virtual ICollection<UserProfile> UserProfiles { get; set; } = new List<UserProfile>();
}

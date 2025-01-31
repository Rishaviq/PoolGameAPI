using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace PoolGameAPI.modules;

public partial class UserAccount
{
    public int IduserAccounts { get; set; }

    public string UserAccountsUsername { get; set; } = null!;

    public string UserAccountsPassword { get; set; } = null!;
    [JsonIgnore]
    public virtual ICollection<GameRecord> GameRecords { get; set; } = new List<GameRecord>();
    [JsonIgnore]
    public virtual ICollection<UserProfile> UserProfiles { get; set; } = new List<UserProfile>();
}

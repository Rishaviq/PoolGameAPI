using System;
using System.Collections.Generic;

namespace PoolGameAPI.modules;

public partial class UserProfile
{
    public int IdUserProfile { get; set; }

    public string UserProfileDisplayname { get; set; } = null!;

    public int? UserProfileIduser { get; set; }

    public virtual UserAccount? UserProfileIduserNavigation { get; set; }
}

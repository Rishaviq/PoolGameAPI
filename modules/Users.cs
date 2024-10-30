using System.Text.Json.Serialization;

namespace PoolGameAPI.modules
{
    public class Users
    {
        [JsonInclude]
        public string username { get; set; }
        [JsonInclude]
        public string password { get; set; }

        

    }
}

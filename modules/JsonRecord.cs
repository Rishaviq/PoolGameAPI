using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Text.Json.Serialization;

namespace PoolGameAPI.modules
{
    public class JsonRecord
    {
        [JsonIgnore]
        private IConfiguration _configuration;
        
        [JsonPropertyName("gameId")]
        public string gameId { get; set; } = null!;
       
        
        public string player { get; set; }
        
        [JsonPropertyName("result")]
        public string result { get; set; }
        
        [JsonPropertyName("shotsMade")]
        public int? shotsMade { get; set; }
        
        [JsonPropertyName("shotAttempted")]
        public int? shotAttempted { get; set; }
      
        [JsonPropertyName("handball")]
        public int? handball { get; set; }
       
        [JsonPropertyName("fouls")]
        public int? fouls { get; set; }

        public int? BestStreak { get; set; }

        public JsonRecord(GameRecord gR) {
            
            
                gameId = gR.GameRecordsGameId;
                player = gR.GameRecordsPlayerNavigation.UserAccountsUsername;
                result = gR.GameRecordsResultNavigation.ResultType1;
                shotsMade = gR.GameRecordsShotsMade;
                shotAttempted = gR.GameRecordsShotAttempted;
                handball = gR.GameRecordsHandball;
                fouls = gR.GameRecordsFouls;
            BestStreak = gR.GameRecordsBestStreak;
            
        }

        public GameRecord createRecord(GameRecord gR,IConfiguration configuration) {
            _configuration=configuration;
            gR.GameRecordsGameId = gameId;
            gR.GameRecordsShotsMade = shotsMade;
            gR.GameRecordsShotAttempted = shotAttempted;
            gR.GameRecordsHandball = handball;
            gR.GameRecordsFouls = fouls;
            gR.GameRecordsBestStreak=BestStreak;
            using (var Db = new PoolAppDbContext(configuration))
            {

                var result = Db.ResultTypes

                        .Where(gr => gr.ResultType1.Equals(this.result))
                        .Single();
                gR.GameRecordsResult = result.IdresultTypes;

                var player = Db.UserAccounts

                       .Where(gr => gr.UserAccountsUsername.Equals(this.player))
                       .Single();
                gR.GameRecordsPlayer = player.IduserAccounts;

            }



            return gR;
        }

        // [JsonConstructor]
        // public JsonRecord(string _gameId, string _player, string _result, int _shotsMade, int _shotAttempted, int _handball, int _fouls) =>
        //  (gameId, player, result, shotsMade, shotAttempted, handball, fouls) = (_gameId, _player, _result, _shotsMade, _shotAttempted, _handball, _fouls);
        
        public JsonRecord() {
            gameId = string.Empty;
            player = string.Empty;
            result = string.Empty;
            shotsMade = null;
            shotAttempted = null;
            handball = null;
            fouls = null;
            BestStreak = null;
        }







    }
}

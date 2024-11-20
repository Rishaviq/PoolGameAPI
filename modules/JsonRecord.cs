namespace PoolGameAPI.modules
{
    public class JsonRecord
    {

        public string GameId { get; set; } = null!;

        public string Player { get; set; }

        public string Result { get; set; }

        public int? ShotsMade { get; set; }

        public int? ShotAttempted { get; set; }

        public int? Handball { get; set; }

        public int? Fouls { get; set; }


      public  JsonRecord(GameRecord gR) {
            
            
                GameId = gR.GameRecordsGameId;
                Player = gR.GameRecordsPlayerNavigation.UserAccoutsUsername;
                Result = gR.GameRecordsResultNavigation.ResultType1;
                ShotsMade = gR.GameRecordsShotsMade;
                ShotAttempted = gR.GameRecordsShotAttempted;
                Handball = gR.GameRecordsHandball;
                Fouls = gR.GameRecordsFouls;
            
        }
        




    }
}

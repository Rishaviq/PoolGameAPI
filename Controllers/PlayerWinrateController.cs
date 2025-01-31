using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PoolGameAPI.modules;

namespace PoolGameAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PlayerWinrateController(IConfiguration configuration) : Controller
    {
        
    
        [HttpGet]

        [Authorize]
        public ActionResult Get()
        {
            
            try {
                using (var Db = new PoolAppDbContext(configuration))
                {

                    var playerStats = Db.GameRecords
                        .Join(Db.UserAccounts,
                              gr => gr.GameRecordsPlayer,
                              user => user.IduserAccounts,

                              (gr, user) => new
                              {
                                  playerUsername = user.UserAccountsUsername,

                                  gr.GameRecordsResult,
                                  gr.GameRecordsShotsMade,
                                  gr.GameRecordsShotAttempted
                              })
                        .GroupBy(p => p.playerUsername)
                        .Select(g => new
                        {
                                PlayerName = g.Key,

                                WinRate =Math.Round( 
                                    ((double)g.Count(p => p.GameRecordsResult == 1) / g.Count())*100,2), 
                               
                                ShotSuccessRate = Math.Round(
                                    (g.Average(p => p.GameRecordsShotAttempted > 0 ? (double)p.GameRecordsShotsMade / p.GameRecordsShotAttempted : 0) ?? 0)*100,2) 
                        })
                        .OrderByDescending(p => p.WinRate)
                        .ToList();




                    return Ok(playerStats);
                }


                    
            }
            catch (Exception ex)
            {
                Console.WriteLine("get");
                Console.WriteLine(ex.Message);
                return null;

            }            
        }
    }
    
    
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PoolGameAPI.modules;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using System.IdentityModel.Tokens.Jwt;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PoolGameAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameRecordsController(IConfiguration configuration, TokenProvider tokenProvider) : Controller
    {


        // GET api/<GameRecordsController>/5
        [Authorize]
        [HttpGet("{username}")]
        public IActionResult Get(string username)
        {
            try
            {

                using (var Db = new PoolAppDbContext(configuration))
                {

                    var game = Db.GameRecords
                        .Include(gr => gr.GameRecordsPlayerNavigation)
                        .Include(gr => gr.GameRecordsResultNavigation)
                            .Where(gr => gr.GameRecordsPlayerNavigation.UserAccountsUsername == username)
                            .ToList();


                    JsonRecord[] jsonRecords = new JsonRecord[game.Count];
                    for (int i = 0; i < game.Count; i++)
                    {
                        jsonRecords[i] = new JsonRecord(game[i]);
                    }
                    return Ok(jsonRecords);



                }
            }
            catch (Exception ex) { Console.WriteLine(ex.ToString()); return Ok(); }

        }

        // POST api/<GameRecordsController>
        [Authorize]
        [HttpPost]
        public IActionResult Post([FromBody] JsonRecord gamerecord)
        {
            try
            {
                using (var Db = new PoolAppDbContext(configuration))
                {
                    gamerecord.player = tokenProvider.getUsernameFromToken(Request.Headers["Authorization"].ToString());
                    GameRecord newRecord = new GameRecord();
                    newRecord = gamerecord.createRecord(newRecord, configuration);
                    Db.GameRecords.Add(newRecord);
                    Db.SaveChanges();
                }
            }
            catch (Exception ex) { Console.WriteLine(ex.ToString()); }
            return Ok();
        }

        [Authorize]
        [HttpGet("All")]
        
        public IActionResult Get() {
            try
            {
                using (var Db = new PoolAppDbContext(configuration))
                {
                    var record=Db.GameRecords
                        .Include(gr => gr.GameRecordsPlayerNavigation)
                        .Include(gr => gr.GameRecordsResultNavigation)
                        .ToList();


                    JsonRecord[] jsonRecords = new JsonRecord[record.Count];
                    for (int i = 0; i < record.Count; i++)
                    {
                        jsonRecords[i] = new JsonRecord(record[i]);
                    }
                    return Ok(jsonRecords);
                }

               
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); return Ok(); }
        }





    }
}

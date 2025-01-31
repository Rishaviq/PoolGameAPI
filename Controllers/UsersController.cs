using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using PoolGameAPI.modules;
using System.Text.Json;
using MySql.Data.MySqlClient;
using Google.Protobuf.WellKnownTypes;
using Microsoft.AspNetCore.Authorization;

namespace PoolGameAPI.Controllers
{
    [ApiController]
   
    [Route("[controller]")]
    public class UsersController(TokenProvider tokenProvider, IConfiguration configuration,IPasswordHasher passwordHasher) : Controller
    {

        [HttpPost]
        public IActionResult Post([FromBody] Users value) {


            try
            {
                value.password = passwordHasher.Hash(value.password);

                //  IConfiguration configuration;
                MySqlConnection connection = new MySqlConnection(configuration["SQL:connection"].ToString());

                connection.Open();


                if (connection.State == System.Data.ConnectionState.Open)
                {
                    
                    string query = $"INSERT INTO user_accouts ( user_accouts_username,user_accouts_password) " +
                                   $"VALUES (@AccUsername, @AccPassword)";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {

                        command.Parameters.AddWithValue("@AccUsername", value.username);
                        command.Parameters.AddWithValue("@AccPassword", value.password);

                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {

                            Console.WriteLine("Registration successful.");
                        }
                        else
                        {

                            Console.WriteLine("Registration failed.");
                        }
                    }
                }
                else
                {

                    Console.WriteLine("Database connection failed.");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to load {ex.Message}");

                return Ok();
            }


            return Ok();
        }

        [HttpGet]
        public string Get([FromQuery] Users credentials) {
            try
            {
                string token = tokenProvider.Create(credentials);


                using (var dBcontext = new PoolAppDbContext(configuration))
                {
                    
                    var user = dBcontext.UserAccounts
                            
                            .Where(b=>b.UserAccountsUsername.Equals( credentials.username))
                            .ToList();

                    if (passwordHasher.Verify(credentials.password, user[0].UserAccountsPassword) && user != null)
                    {

                       
                        return token;
                    }
                    else { return "wrong username/password"; }


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

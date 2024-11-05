using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using PoolGameAPI.modules;
using System.Text.Json;
using MySql.Data.MySqlClient;
using Google.Protobuf.WellKnownTypes;

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
                string token = null;
                string passwordHash=null;


                MySqlConnection connection = new MySqlConnection(configuration["SQL:connection"].ToString());
                connection.Open();

                if (connection.State == System.Data.ConnectionState.Open)
                {

                    string query = "SELECT user_accouts_password FROM user_accouts WHERE user_accouts_username =@Username";
                    
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Username", credentials.username);
                    var temp=command.ExecuteScalar();
                    if (temp != null)
                    {
                        passwordHash = command.ExecuteScalar().ToString();

                        if (passwordHasher.Verify(credentials.password, passwordHash) && temp != null)
                        {
                            token = tokenProvider.Create(credentials);
                            return token;
                        }
                        else { return "Invalid password or username"; }


                    }
                    return "Invalid username";
                    
                }
                else
                {

                    Console.WriteLine("Database connection failed.");
                    return "error";
                }




               

            }



            catch (Exception ex) {
                Console.WriteLine(ex.Message);
                return null;
            
            }
        } 
    }
}

﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using PoolGameAPI.modules;
using System.Text.Json;
using MySql.Data.MySqlClient;

namespace PoolGameAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController(TokenProvider tokenProvider, IConfiguration configuration) : Controller
    {

        [HttpPost]
        public IActionResult Post([FromBody] Users value) {


            try
            {

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
                MySqlConnection connection = new MySqlConnection(configuration["SQL:connection"].ToString());
                connection.Open();
                 MySqlCommand command = new MySqlCommand();
                token = tokenProvider.Create(credentials);
                return token;
            }
            catch (Exception ex) {
                Console.WriteLine(ex.Message);
                return null;
            
            }
        } 
    }
}
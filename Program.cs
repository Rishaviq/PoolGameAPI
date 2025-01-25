
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using PoolGameAPI.Controllers;
using System.Text;

namespace PoolGameAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
           
            Environment.SetEnvironmentVariable("Jwt__Secret", builder.Configuration["Jwt:Secret"]);
            Environment.SetEnvironmentVariable("Jwt__Issuer", builder.Configuration["Jwt:Issuer"]);
            Environment.SetEnvironmentVariable("Jwt__Audience", builder.Configuration["Jwt:Audience"]);
            Environment.SetEnvironmentVariable("Jwt__ExpirationInMinutes", "1440");
            Environment.SetEnvironmentVariable("SQL__connection", builder.Configuration["SQL:connectiont"]);
            Environment.SetEnvironmentVariable("PoolGameAPIContext", builder.Configuration["PoolGameAPIContext"]);

            builder.Configuration.AddEnvironmentVariables();
           

            /*   builder.WebHost.ConfigureKestrel(options =>                                  //temp 
                {
                    options.Listen(System.Net.IPAddress.Any , 5001, listenOptions =>
                    {
                        listenOptions.UseHttps("192.168.1.102.pem", "192.168.1.102-key.pem");
                    });

                });*/
            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGenWithAuth();
            builder.Services.AddSingleton<TokenProvider>();
            builder.Services.AddAuthorization();
            builder.Services.AddSingleton<IPasswordHasher, PasswordHasher>();
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(o =>
                {
                    o.RequireHttpsMetadata = false;
                    o.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                    {

                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Secret"]!)),
                        ValidIssuer = builder.Configuration["Jwt:Issuer"],
                        ValidAudience = builder.Configuration["Jwt:Audience"],
                        ClockSkew = TimeSpan.Zero

                    };

                });

            var app = builder.Build();
            

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

          //  app.UseAuthorization();


            app.MapControllers();

            app.UseAuthentication();

            app.UseAuthorization();

            app.Run();
        }
    }
}

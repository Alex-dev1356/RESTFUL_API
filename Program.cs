using Microsoft.EntityFrameworkCore;
using Restful_API.Data;

namespace Restful_API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            //Declaring our DbContext as a service in the Dependency Injection container
            builder.Services.AddDbContext<RestAPIContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))); //Installing the Microsoft.EntityFrameworkCore.SqlServer via NuGet Package Manager to use 'options.UseSqlServer'

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}

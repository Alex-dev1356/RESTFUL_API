using Microsoft.EntityFrameworkCore;
using Restful_API.Models;

namespace Restful_API.Data
{
    //First we need to install the Entity Framework Core package via NuGet Package Manager, so that we can use the DbContext class
    //This class will serve as a bridge between our database and our application
    public class RestAPIContext : DbContext
    {
        //Creating the Constructor which have some Parameter options of type DbContextOptions of type RestAPIContext
        //This will allow us to configure the context to connect to the database
        public RestAPIContext(DbContextOptions<RestAPIContext> options)
            : base(options)
        {

        }

        //We want to migrate our Book model to the database, so we need to create a DbSet property of type Book
        //This will represent the Books table in the database
        public DbSet<Book> Books { get; set; } = null!;//We gave ut a Null value to avoid warnings
    }
}

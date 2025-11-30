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


        //Inserting Data to our Database
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //Seeding initial data to the Books table
            modelBuilder.Entity<Book>().HasData(
                new Book { ID = 1, Title = "To Kill a Mockingbird", Author = "Harper Lee", YearPubished = 1960 },
                new Book { ID = 2, Title = "1984", Author = "George Orwell", YearPubished = 1949 },
                new Book { ID = 3, Title = "The Great Gatsby", Author = "F. Scott Fitzgerald", YearPubished = 1925 }
            );

            //We can then run the migrations to create the database and the Books table with the seeded data
            //On the Package Manager Console, run the following commands:
            //Add-Migration "MigrationName(ex. book data added)"
            //Update-Database
        }
    }
}

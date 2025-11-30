using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restful_API.Models;

//For Creating a new controller named BooksController => Right Click on Controllers folder => Add => Controller => API Controller - Empty => Name it BooksController
namespace Restful_API.Controllers
{
    [Route("api/[controller]")] //This is the Route for the Controller, we can access the controller via api/books
    [ApiController]
    public class BooksController : ControllerBase
    {
        //Creating some data for the Books
        static List<Book> books = new List<Book>//Making the List STATIC, this way we can access it without creating an instance of the class
                                                //meaning it is only created once and shared across all instances of the class
                                                //If we don't make it static, every time we create an instance of the class, a new list will be created and the data will be lost
        {
            new Book 
            { 
                ID = 1, 
                Title = "To Kill a Mocking bird", 
                Author = "Harper Lee", 
                YearPubished = 1960 
            },
            new Book
            {
                ID = 2,
                Title = "1984",
                Author = "George Orwell",
                YearPubished = 1949
            },
            new Book
            {
                ID = 3,
                Title = "The Great Gatsby",
                Author = "F. Scott Fitzgerald",
                YearPubished = 1925
            }
        };
    }
}

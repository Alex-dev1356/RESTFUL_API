using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restful_API.Models;
using System.Reflection.Metadata.Ecma335;

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

        //Creating an HTTP GET method to get all the books
        [HttpGet]
        public ActionResult<Book> GetBooks()
        {

            return Ok(books); //Returning the list of books with a 200 OK status code if successfull
        }

        //Creating an HTTP GET method to get ONE SPECIFIC book
        [HttpGet("{id}")]
        public ActionResult<Book> GetBookById(int id)
        {
            var book = books.FirstOrDefault(b => b.ID == id);//Looping through the books list using the FirstOrDefault LINQ method
                                                             //then finding the ID that matches the id parameter (b => b.ID == id)
                                                             //Finding the book with the specific ID

            if (book == null) 
                return NotFound(); //Returning a 404 Not Found status code if the book is not found

            return Ok(book); //Returning the books with a 200 OK status code if successfull

            //Another way to write it with error handling:
            //foreach (var book in books)
            //{
            //    if (book.ID == id)
            //    {
            //        return Ok(book); //Returning the book with a 200 OK status code if successfull
            //    }
            //}

            //return NotFound(); //Returning a 404 Not Found status code if the book is not found
        }
    }
}

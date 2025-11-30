using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Restful_API.Data;
using Restful_API.Models;
using System;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.Intrinsics.X86;
using static System.Runtime.InteropServices.JavaScript.JSType;

//For Creating a new controller named BooksController => Right Click on Controllers folder => Add => Controller => API Controller - Empty => Name it BooksController
namespace Restful_API.Controllers
{
    //Code BEFORE there is a Data Base 
    #region
    //[Route("api/[controller]")] //This is the Route for the Controller, we can access the controller via api/books
    //[ApiController]
    //public class BooksController : ControllerBase
    //{
    //    //Creating some data for the Books
    //    static readonly List<Book> books = new List<Book>//Making the List STATIC, this way we can access it without creating an instance of the class
    //                                            //meaning it is only created once and shared across all instances of the class
    //                                            //If we don't make it static, every time we create an instance of the class, a new list will be created and the data will be lost
    //    {
    //        new Book
    //        {
    //            ID = 1,
    //            Title = "To Kill a Mocking bird",
    //            Author = "Harper Lee",
    //            YearPubished = 1960
    //        },
    //        new Book
    //        {
    //            ID = 2,
    //            Title = "1984",
    //            Author = "George Orwell",
    //            YearPubished = 1949
    //        },
    //        new Book
    //        {
    //            ID = 3,
    //            Title = "The Great Gatsby",
    //            Author = "F. Scott Fitzgerald",
    //            YearPubished = 1925
    //        }
    //    };

    //    //Creating an HTTP GET method to get all the books
    //    [HttpGet]
    //    public ActionResult<Book> GetBooks()
    //    {

    //        return Ok(books); //Returning the list of books with a 200 OK status code if successfull
    //    }

    //    //Creating an HTTP GET method to get ONE SPECIFIC book
    //    [HttpGet("{id}")]
    //    public ActionResult<Book> GetBookById(int id)
    //    {
    //        var book = books.FirstOrDefault(b => b.ID == id);//Looping through the books list using the FirstOrDefault LINQ method
    //                                                         //then finding the ID that matches the id parameter (b => b.ID == id)
    //                                                         //Finding the book with the specific ID

    //        if (book == null)
    //            return NotFound(); //Returning a 404 Not Found status code if the book is not found

    //        return Ok(book); //Returning the books with a 200 OK status code if successfull

    //        //Another way to write it with error handling:
    //        //foreach (var book in books)
    //        //{
    //        //    if (book.ID == id)
    //        //    {
    //        //        return Ok(book); //Returning the book with a 200 OK status code if successfull
    //        //    }
    //        //}

    //        //return NotFound(); //Returning a 404 Not Found status code if the book is not found
    //    }

    //    //Creating an HTTP POST method to add a new book
    //    [HttpPost]
    //    public ActionResult<Book> AddBook(Book newBook)
    //    {
    //        if (newBook == null)
    //            return BadRequest(); //Returning a 400 Bad Request status code if the book is null

    //        books.Add(newBook); //Adding the new book to the list
    //        return CreatedAtAction(nameof(GetBookById), new { id = newBook.ID }, newBook); //Returning a 201 Created status code if successfull
    //                                                                                       //1st Parameter: The action TO GET the newly created book (GetBookById)
    //                                                                                       //2nd Parameter: The route values (the id of the newly created book)
    //                                                                                       //3rd Parameter: Returning the newly created book
    //    }

    //    //Creating an HTTP PUT method to update an existing book
    //    [HttpPut("{id}")]
    //    public IActionResult UpdateBook(int id, Book updatedBook) //We used IActionResult becuase we're not returning any object, were returning STATUS CODE
    //    {
    //        var bookId = books.FirstOrDefault(b => b.ID == id);

    //        if (bookId == null)
    //            return NotFound();

    //        //If the book is existing based on the ID passed, we're updating the value of the existing book to the value of the updated book.
    //        bookId.ID = updatedBook.ID;
    //        bookId.Author = updatedBook.Author;
    //        bookId.Title = updatedBook.Title;
    //        bookId.YearPubished = updatedBook.YearPubished;

    //        return NoContent(); //This returns a Status Code of 204 which means there was no content returned, but rather ONLY UPDATED
    //    }

    //    //Creating an HTTP DELETE method to delete an existing book
    //    [HttpDelete("{id}")]
    //    public IActionResult DeleteBook(int id)
    //    {
    //        var bookId = books.FirstOrDefault(b => b.ID == id);

    //        if (bookId == null)
    //            return NotFound();

    //        books.Remove(bookId);

    //        return NoContent();
    //    }
    //}
    #endregion



    //Accessing the Data on our Data Base using the Dependency Injection
    [Route("api/[controller]")] //This is the Route for the Controller, we can access the controller via api/books
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly RestAPIContext _context; //Using the Dependency Injection to use the Data from our Data Base using the DB Context From EF
        public BooksController(RestAPIContext context)
        {
            _context = context;
        }

        [HttpGet]
        //We Use async when your method performs I/O-bound work (database, file, network).
        //We Use Task<T> for async methods that return a value. Use Task (without <T>) for async methods that don’t return anything.
        public async Task<ActionResult<Book>> GetBooks()
        {
            //await waits for the database query to finish, then passes the result into Ok()
            //Use await whenever you call an async method and need its result.
            //Without await, you’d just get a Task object, not the actual data.
            return Ok(await _context.Books.ToListAsync()); //We use ToListAsync() instead of ToList() whenever you’re inside an
                                                           //async method. It’s best practice in ASP.NET Core because it scales
                                                           //better under heavy load.
                                                           //This runs SELECT * FROM Books asynchronously and returns a List<Book>.

            //📊 Analogy
            // Think of async/ await like ordering food at a restaurant:
            // -async = You tell the waiter you’ll wait for food but don’t block the table.
            // - Task < T > = The order slip(promise) that says "food will come later."
            // - await = You pause eating until the food arrives, but you can chat or do other things.
            // -ToListAsync() = The kitchen preparing your meal(database query).
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBookById(int id)
        {
            var bookById = await _context.Books.FirstOrDefaultAsync(b => b.ID == id);
            //var bookById = await _context.Books.FindAsync(id); //Another way to find the book for specific ID.

            if (bookById == null)
                return NotFound();

            return Ok(bookById);
        }

        [HttpPost]
        public async Task<ActionResult<Book>> AddBook(Book newBook)
        {
            if (newBook == null)
                return BadRequest();

            _context.Books.Add(newBook);

            //After adding the Book, we need to SAVE THE CHANGES 
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetBookById), new { id = newBook.ID }, newBook);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Book>> UpdateBook(int id, Book updatedBook)
        {
            var bookById = await _context.Books.FindAsync(id);

            if (bookById == null)
                return NotFound();

            bookById.ID = updatedBook.ID;
            bookById.Title = updatedBook.Title;
            bookById.Author = updatedBook.Author;
            bookById.YearPubished = updatedBook.YearPubished;

            //After updating the Book, we need to SAVE THE CHANGES 
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var bookById = await _context.Books.FindAsync(id);

            if (bookById == null)
                return NotFound();

            _context.Books.Remove(bookById);

            //After deleting the Book, we need to SAVE THE CHANGES 
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
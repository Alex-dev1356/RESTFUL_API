# Restful API
---

# 📘 Notes on Building REST APIs in .NET 9

## 1. What is a REST API?
- **Definition:** REST (Representational State Transfer) is a set of rules for communication between systems using HTTP methods (GET, POST, PUT, DELETE).
- **Why use it:** It allows different applications (web, mobile, desktop) to interact with the same backend data.
- **Example:**  
  - A weather app requests today’s forecast → API sends JSON data with temperature and conditions.  
  - A banking app requests account balance → API responds with JSON `{ "balance": 5000 }`.

---

## 2. Project Setup in ASP.NET Core (.NET 9)
- **Files to know:**
  - `Program.cs` → Entry point, configures middleware and services.
  - `appsettings.json` → Stores configuration (logging, database connection strings).
  - `launchSettings.json` → Defines URLs for development.
- **Example:**  
  - `appsettings.json` might contain:  
    ```json
    "ConnectionStrings": {
      "DefaultConnection": "Server=.;Database=LibraryDB;Trusted_Connection=True;"
    }
    ```

---

## 3. Models (Data Structure)
- **Definition:** A model is a C# class that represents data (like a database table).
- **Example:** Book model  
  ```csharp
  public class Book {
      public int Id { get; set; }
      public string Title { get; set; } = string.Empty;
      public string Author { get; set; } = string.Empty;
      public int YearPublished { get; set; }
  }
  ```
- **Other Example:** User model  
  ```csharp
  public class User {
      public int Id { get; set; }
      public string Username { get; set; } = string.Empty;
      public string Email { get; set; } = string.Empty;
      public DateTime DateJoined { get; set; }
  }
  ```

---

## 4. Controllers (Logic for Endpoints)
- **Definition:** Controllers define how the API responds to requests.
- **Example:** BooksController route `/api/books`
  - GET → `/api/books` → returns all books
  - GET → `/api/books/1` → returns book with ID=1
  - POST → `/api/books` → adds a new book
- **Other Example:** UsersController route `/api/users`
  - GET → `/api/users` → returns all users
  - DELETE → `/api/users/5` → deletes user with ID=5

---

## 5. CRUD Operations with HTTP Methods
- **GET:** Retrieve data  
  Example: `/api/books` → returns list of books  
- **POST:** Add new data  
  Example: `/api/books` with body `{ "title": "1984", "author": "Orwell" }`  
- **PUT:** Update existing data  
  Example: `/api/books/1` with body `{ "title": "Animal Farm" }`  
- **DELETE:** Remove data  
  Example: `/api/books/1` → deletes book with ID=1  

💡 **Other Example:** For a `UsersController`  
- GET `/api/users/2` → returns user with ID=2  
- POST `/api/users` → adds new user  
- PUT `/api/users/2` → updates user info  
- DELETE `/api/users/2` → removes user  

---

## 6. Connecting to SQL Server
- **Steps:**
  1. Install SQL Server + Management Studio.
  2. Add connection string in `appsettings.json`.
  3. Create `DbContext` class to bridge C# models with database tables.
  4. Run migrations to create tables.
- **Example:**  
  ```csharp
  public class LibraryContext : DbContext {
      public LibraryContext(DbContextOptions<LibraryContext> options) : base(options) {}
      public DbSet<Book> Books { get; set; }
      public DbSet<User> Users { get; set; }
  }
  ```

---

## 7. Entity Framework Core (EF Core)
- **Definition:** ORM (Object Relational Mapper) that lets you interact with the database using C# instead of SQL.
- **Example:**  
  - Add a book:  
    ```csharp
    var newBook = new Book { Title = "Clean Code", Author = "Robert C. Martin", YearPublished = 2008 };
    context.Books.Add(newBook);
    await context.SaveChangesAsync();
    ```
  - Query books:  
    ```csharp
    var books = await context.Books.ToListAsync();
    ```

---

## 8. Testing APIs
- **Options:**
  - Built-in HTTP file in Visual Studio.
  - Postman (popular external tool).
- **Example:**  
  - GET request in HTTP file:  
    ```
    GET https://localhost:5001/api/books
    Accept: application/json
    ```

---

# ✅ How to Use These Notes
- Treat each section as a **reference card**.  
- Add your own **examples** (e.g., Movies API, Students API) to reinforce learning.  
- Keep a **library of models + controllers** so you can reuse them in future projects.  

---


📝 REST API Cheat Sheet – .NET 9 (ASP.NET Core)
1. REST API Basics
REST = Representational State Transfer

Uses HTTP methods: GET, POST, PUT, DELETE

Data format: JSON (most common)

Example JSON Response:

json
{
  "id": 1,
  "title": "Clean Code",
  "author": "Robert C. Martin",
  "yearPublished": 2008
}
2. Project Setup
Create new project → ASP.NET Core Web API

Key files:

Program.cs → app startup

appsettings.json → configs (logging, DB connection)

launchSettings.json → dev URLs

Example Connection String:

json
"ConnectionStrings": {
  "DefaultConnection": "Server=.;Database=LibraryDB;Trusted_Connection=True;"
}
3. Models (Data Classes)
Represent data structure (like DB tables)

Book Model:

csharp
public class Book {
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public int YearPublished { get; set; }
}
Other Example – User Model:

csharp
public class User {
    public int Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public DateTime DateJoined { get; set; }
}
4. Controllers (Endpoints)
Define routes and logic for API requests

BooksController Example:

csharp
[ApiController]
[Route("api/[controller]")]
public class BooksController : ControllerBase {
    [HttpGet]
    public IActionResult GetBooks() => Ok(books);
}
Other Example – UsersController:

csharp
[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase {
    [HttpDelete("{id}")]
    public IActionResult DeleteUser(int id) {
        var user = users.FirstOrDefault(u => u.Id == id);
        if (user == null) return NotFound();
        users.Remove(user);
        return NoContent();
    }
}
5. CRUD Operations
GET – Retrieve Data
csharp
[HttpGet]
public IActionResult GetBooks() => Ok(books);
➡ Example: /api/books

POST – Add Data
csharp
[HttpPost]
public IActionResult AddBook(Book newBook) {
    books.Add(newBook);
    return CreatedAtAction(nameof(GetBooks), new { id = newBook.Id }, newBook);
}
➡ Example: /api/books

PUT – Update Data
csharp
[HttpPut("{id}")]
public IActionResult UpdateBook(int id, Book updatedBook) {
    var book = books.FirstOrDefault(b => b.Id == id);
    if (book == null) return NotFound();
    book.Title = updatedBook.Title;
    book.Author = updatedBook.Author;
    book.YearPublished = updatedBook.YearPublished;
    return NoContent();
}
➡ Example: /api/books/1

DELETE – Remove Data
csharp
[HttpDelete("{id}")]
public IActionResult DeleteBook(int id) {
    var book = books.FirstOrDefault(b => b.Id == id);
    if (book == null) return NotFound();
    books.Remove(book);
    return NoContent();
}
➡ Example: /api/books/1

6. Database Integration (SQL Server + EF Core)
DbContext Setup
csharp
public class LibraryContext : DbContext {
    public LibraryContext(DbContextOptions<LibraryContext> options) : base(options) {}
    public DbSet<Book> Books { get; set; }
    public DbSet<User> Users { get; set; }
}
Program.cs Configuration
csharp
builder.Services.AddDbContext<LibraryContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
7. Entity Framework Core Usage
Add Data
csharp
var newBook = new Book { Title = "Domain-Driven Design", Author = "Eric Evans", YearPublished = 2003 };
context.Books.Add(newBook);
await context.SaveChangesAsync();
Query Data
csharp
var books = await context.Books.ToListAsync();
Update Data
csharp
var book = await context.Books.FindAsync(1);
book.Title = "Refactoring";
await context.SaveChangesAsync();
Delete Data
csharp
var book = await context.Books.FindAsync(1);
context.Books.Remove(book);
await context.SaveChangesAsync();
8. Testing APIs
Built-in HTTP file in Visual Studio

Postman (external tool)

Example HTTP Request:

Code
GET https://localhost:5001/api/books
Accept: application/json
✅ Quick Reference Flow
Create project → ASP.NET Core Web API

Define Models → represent data

Create Controllers → handle routes

Implement CRUD methods → GET, POST, PUT, DELETE

Connect to SQL Server via EF Core

Test endpoints with HTTP file or Postman


🔄 Flow Explanation
Client (Web/Mobile App) → Sends HTTP request (GET, POST, PUT, DELETE).

Controller (ASP.NET Core) → Receives request, applies routing, validation, and business logic.

Service Layer (Optional) → Encapsulates reusable business rules.

DbContext (Entity Framework Core) → Translates C# operations into SQL queries.

SQL Server Database → Executes queries, stores/retrieves data.

DbContext Maps Results → Converts database rows back into C# objects.

Controller Sends JSON Response → Returns structured data with status codes.

Client Receives Response → Displays results in app UI.

This diagram is a visual quick reference for how every request flows end-to-end. It’s especially useful when debugging or explaining architecture to teammates.



-------------------
![REST API Connection to Database]
Installing these packages via NuGet Package Manager Console:
```bash
Install-Package Microsoft.EntityFrameworkCore => to use the DbContext and EF Core features
Install-Package Microsoft.EntityFrameworkCore.SqlServer => to connect to SQL Server databases
Install-Package Microsoft.EntityFrameworkCore.Tools => to enable EF Core command-line tools for migrations and scaffolding
```

To do the Migration Go to: 
Tools -> NuGet Package Manager -> Package Manager Console
Then Run these commands:
Add-Migration "NameOfMigration(ex.book model added)" then hit 'Enter'

Then to update our Database run:
Update-Database then hit 'Enter' //This will apply the migration to the database
BokController
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnetapp.Models;
using dotnetapp.Services;
using Microsoft.AspNetCore.Mvc;
using dotnetapp.Repository;
using Microsoft.Extensions.Logging;
 
namespace dotnetapp.Controllers
{
    [ApiController]
    [Route("api/books")]
    public class BookController : ControllerBase
    {
        public IBookService ser;
 
        public BookController( IBookService ser1)
        {
            ser =ser1;
        }
 
        [HttpGet]
        public ActionResult<List<Book>> GetBooks()
        {
            return Ok(ser.GetBooks());
        }
 
        [HttpGet("{id}")]
        public ActionResult<Book> GetBook(int id)
        {
            var book = ser.GetBook(id);
            if(book==null)
              return NotFound();
           
            return Ok(book);
        }
 
        [HttpPost]
        public ActionResult<Book> SaveBook([FromBody]Book obj)
        {
            var createdBook = ser.SaveBook(obj);
            return CreatedAtAction(nameof(GetBook), new {id = createdBook.BookId},
            createdBook);
        }
 
        [HttpPut("{id}")]
        public ActionResult<Book> UpdateBook(int id, Book obj)
        {
            var res = ser.UpdateBook(id, obj);
            if(res == null)
              return NotFound();
 
            return NoContent();
           
        }
 
        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            ser.DeleteBook(id);
 
            return NoContent();
        }
    }
}
Abhishek Vasant Khomane
24-06-2026 20:59
ordercontroller
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnetapp.Models;
using dotnetapp.Services;
using Microsoft.AspNetCore.Mvc;
using dotnetapp.Repository;
using Microsoft.Extensions.Logging;
 
namespace dotnetapp.Controllers
{
    [ApiController]
    [Route("api/orders")]
    public class OrderController : ControllerBase
    {
        public IOrderService ser;
 
        public OrderController( IOrderService ser1)
        {
            ser =ser1;
        }
 
        [HttpGet]
        public ActionResult<List<Order>> GetOrders()
        {
            return Ok(ser.GetOrders());
        }
 
        [HttpGet("{id}")]
        public ActionResult<Book> GetOrder(int id)
        {
            var order = ser.GetOrder(id);
            if(order==null)
              return NotFound();
           
            return Ok(order);
        }
 
        [HttpPost]
        public ActionResult<Book> SaveOrder([FromBody]Order obj)
        {
            var createdOrder = ser.SaveOrder(obj);
            return CreatedAtAction(nameof(GetOrder), new {id = createdOrder.OrderId},
            createdOrder);
        }
 
        [HttpPut("{id}")]
        public ActionResult<Book> UpdateOrder(int id, Order obj)
        {
            var res = ser.UpdateOrder(id, obj);
            if(res == null)
              return NotFound();
 
            return NoContent();
           
        }
 
        [HttpDelete("{id}")]
        public IActionResult DeleteOrder(int id)
        {
            ser.DeleteOrder(id);
 
            return NoContent();
        }
    }
}
Abhishek Vasant Khomane
24-06-2026 20:59
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
 
namespace dotnetapp.Models
{
    public class Book
    {
        public int BookId{get;set;}
        public string BookName{get;set;}
        public string Category{get;set;}
        public decimal Price{get; set;}
    }
}
Abhishek Vasant Khomane
24-06-2026 21:00
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
 
namespace dotnetapp.Models
{
    public class Order
    {
        public int OrderId{get;set;}
        public string CustomerName{get;set;}
        public decimal TotalAmount{get; set;}
    }
}
Abhishek Vasant Khomane
24-06-2026 21:00
 book Repo
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnetapp.Models;
using dotnetapp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
 
namespace dotnetapp.Repository
{
    public class BookRepository
    {
        public static List<Book> books = new List<Book>();
 
        public List<Book> GetBooks()
        {
            return books.ToList();
        }
 
        public Book GetBook(int id)
        {
            return books.Find(p=>p.BookId==id);
        }
 
        public Book SaveBook(Book obj)
        {
            obj.BookId = books.Count+1;
            books.Add(obj);
            return obj;
        }
 
        public Book UpdateBook(int id, Book obj)
        {
            var res = books.Find(p=>p.BookId == id);
            res.BookName = obj.BookName;
            res.Category = obj.Category;
            res.Price = obj.Price;
 
            return obj;
        }
 
        public bool DeleteBook(int id)
        {
            var res = books.Find(p=>p.BookId == id);
 
            if(res==null)
                return false;
 
            books.Remove(res);
            return true;
        }
    }
}
Abhishek Vasant Khomane
24-06-2026 21:00
Order repo
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnetapp.Models;
using dotnetapp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
 
namespace dotnetapp.Repository
{
    public class OrderRepository
    {
        public static List<Order> orders = new List<Order>();
 
        public List<Order> GetOrders()
        {
            return orders.ToList();
        }
 
        public Order GetOrder(int id)
        {
            return orders.Find(p=>p.OrderId==id);
        }
 
        public Order SaveOrder(Order obj)
        {
            obj.OrderId = orders.Count+1;
            orders.Add(obj);
            return obj;
        }
 
        public Order UpdateOrder(int id, Order obj)
        {
            var res = orders.Find(p=>p.OrderId == id);
 
            if(res == null)
              return null;
 
            res.CustomerName = obj.CustomerName;
            res.TotalAmount = obj.TotalAmount;
 
            return res;
        }
 
        public bool DeleteOrder(int id)
        {
            var res = orders.Find(p=>p.OrderId == id);
 
            if(res==null)
                return false;
 
            orders.Remove(res);
            return true;
        }
    }
}
Abhishek Vasant Khomane
24-06-2026 21:00
Book service
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnetapp.Models;
using dotnetapp.Repository;
using dotnetapp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
 
 
namespace dotnetapp.Services
{
    public class BookService : IBookService
    {
        private readonly BookRepository ser;
 
        public BookService()
        {
            ser = new BookRepository();
        }
        public BookService( BookRepository ser1)
        {
            ser = ser1;
           
        }
     
        public List<Book> GetBooks()
        {
            return ser.GetBooks();
        }
 
        public Book GetBook(int id)
        {
            return ser.GetBook(id);
        }
 
        public Book SaveBook(Book obj)
        {
           return ser.SaveBook(obj);
        }
 
        public Book UpdateBook(int id, Book obj)
        {
            return ser.UpdateBook(id,obj);
        }
 
         public bool DeleteBook(int id){
            return ser.DeleteBook(id);
        }
    }
}
Abhishek Vasant Khomane
24-06-2026 21:01
Ibookservice
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnetapp.Models;
 
namespace dotnetapp.Services
{
    public interface IBookService
    {
        List<Book> GetBooks();
 
        Book GetBook(int id);
        Book SaveBook(Book obj);
        Book UpdateBook(int id, Book obj);
        bool DeleteBook(int id);
    }
}
Abhishek Vasant Khomane
24-06-2026 21:01
Iordrservice
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnetapp.Models;
 
namespace dotnetapp.Services
{
    public interface IOrderService
    {
        List<Order> GetOrders();
        Order GetOrder(int id);
        Order SaveOrder(Order obj);
        Order UpdateOrder(int id, Order obj);
        bool DeleteOrder(int id);
    }
}
Abhishek Vasant Khomane
24-06-2026 21:01
OrderSevice
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnetapp.Models;
using dotnetapp.Repository;
using dotnetapp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
 
namespace dotnetapp.Services
{
    public class OrderService : IOrderService
    {
        private readonly OrderRepository ser;
 
        public OrderService()
        {
            ser = new OrderRepository();
        }
 
        public OrderService(OrderRepository ser1)
        {
            ser=ser1;
        }
   
        public List<Order> GetOrders()
        {
            return ser.GetOrders();
        }
 
        public Order GetOrder(int id)
        {
            return ser.GetOrder(id);
        }
 
        public Order SaveOrder(Order obj)
        {
            return ser.SaveOrder(obj);
        }
 
        public Order UpdateOrder(int id, Order obj)
        {
           return ser.UpdateOrder(id,obj);
        }
 
        public bool DeleteOrder(int id){
            return ser.DeleteOrder(id);
        }
    }
}
Abhishek Vasant Khomane
24-06-2026 21:01
Program cs
using dotnetapp.Repository;
using dotnetapp.Services;
 
var builder = WebApplication.CreateBuilder(args);
 
// Add Event services to the container.
 
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
 
builder.Services.AddScoped<BookRepository>();
builder.Services.AddScoped<OrderRepository>();
 
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IOrderService, OrderService>();
 
 
 
var app = builder.Build();
 
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
 
app.UseHttpsRedirection();
 
app.UseAuthorization();
 
app.MapControllers();
 
app.Run();
 

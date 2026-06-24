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
        public DateTime OrderDate{get;set;}
        public decimal TotalAmount{get;set;}
        public string Status{get;set;}
    }
}
 
-----------------------
 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnetapp.Services;
using Microsoft.AspNetCore.Mvc;
using dotnetapp.Models;
 
namespace dotnetapp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly OrderService db;
 
        public OrderController(OrderService db1)
        {
            db=db1;
        }
 
        [HttpGet]
        public IActionResult GetAllOrders()
        {
            var res=db.GetAllOrders();
            return Ok(res);
        }
 
        [HttpGet("{orderId}")]
        public IActionResult GetOrdersById(int orderId)
        {
            var res=db.GetOrderById(orderId);
            if(res==null)
            {
                return NotFound();
            }
            return Ok(res);
        }
 
 
        [HttpPost]
        public async Task<ActionResult> AddOrder(Order obj)
        {
            if(obj==null)
            {
                return BadRequest();
            }
            db.AddOrder(obj);
            return CreatedAtAction("AddOrder",obj);
        }
    }
}
 
------------------------------------------
 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnetapp.Models;
 
namespace dotnetapp.Services
{
    public class OrderService
    {
        public static List<Order> orders=new List<Order>();
 
        public List<Order> GetAllOrders()
        {
            return orders.ToList();
        }
 
 
        public Order GetOrderById(int id)
        {
            return orders.Find(r=>r.OrderId==id);
       
        }
 
        public void AddOrder(Order obj)
        {
            orders.Add(obj);
        }
    }
}
 
-------------------------------------
using dotnetapp.Services;
 
 
var builder = WebApplication.CreateBuilder(args);
 
// Add Event services to the container.
 
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<OrderService>();
 
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

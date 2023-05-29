using AuthorizationRestaurant.Models;
using AuthorizationRestaurant.Services;
using BisnessLogic.Models;
using BisnessLogic.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections;
using System.Linq;

namespace BisnessLogic.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly UsersContext _context;
        private readonly ILogger<OrderController> _logger;
        public OrderController(UsersContext context, ILogger<OrderController> logger)
        {
            _logger = logger;
            _context = context;
        }

        [HttpPost]
        [Route("MakeOrder"), Authorize]
        public async Task<ActionResult<Order>> MakeOrder(OrderRequest request)
        {
            var userInDb = _context.Users.FirstOrDefault(u => u.UserName == User.Identity.Name);
            var order = new Order
            {
                User = userInDb,
                CreatedDate = DateTime.UtcNow,
                SpecialRequests = request.SpecialRequests,
                Status = "Wait",
                Dish = request.Dish
            };

            var obj = _context.Order.Add(order);
            _context.SaveChanges();

            return Ok(new OrderResponse()
            {
                UserId = userInDb.Id,
                Dish = order.Dish,
                SpecialRequests = order.SpecialRequests,
                Id = obj.Entity.Id
            });
        }

        [HttpGet]
        [Route("GetOrder"), Authorize]
        public async Task<ActionResult<OrderResponse>> GetOrder(int requestId)
        {
            var orderInDb = _context.Order.ToList().FirstOrDefault((ord) => ord.Id == requestId);
            if(orderInDb == null)
            {
                return BadRequest("Order does not exist");
            }

            return Ok(new OrderResponse()
            {
                UserId = orderInDb.UserId,
                Dish = orderInDb.Dish,
                SpecialRequests = orderInDb.SpecialRequests,
                Status= orderInDb.Status,
                Id = orderInDb.Id
            });
        }


        [HttpGet]
        [Route("GetMenu")]
        public async Task<ActionResult<IEnumerable>> GetMenu()
        {
            var menu = _context.Menu.ToList();

            return Ok(menu);
        }


        [HttpPost]
        [Route("RunOrderProcessor"), Authorize]
        public async Task<IActionResult> RunOrderProcessor(OrderProcessorRequest request)
        {
            var starttime = DateTime.Now;
            var context = new UsersContext();
            Task task = new Task(() =>
            {
                while (true)
                {
                    var orderInDb = context.Order.FirstOrDefault(u => u.Status == "Wait");
                    if (orderInDb != null)
                    {
                        Thread.Sleep(20000);
                        orderInDb.Status = "Done";
                        context.Order.Update(orderInDb);
                        context.SaveChanges();
                        _logger.LogInformation($"Order about {orderInDb.Dish} is done");
                    }

                    if ((DateTime.Now - starttime).TotalMinutes > request.DurationMinutes)
                    {
                        return;
                    }
                }
            });
            task.Start();

            return Ok();
        }
    }
}

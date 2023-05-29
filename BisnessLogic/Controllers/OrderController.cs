using AuthorizationRestaurant.Models;
using AuthorizationRestaurant.Services;
using BisnessLogic.Models;
using BisnessLogic.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BisnessLogic.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly UsersContext _context;
        private readonly ILogger<WeatherForecastController> _logger;
        public OrderController(UsersContext context, ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
            _context = context;
        }

        [HttpPost]
        [Route("MakeOrder"), Authorize]
        public async Task<IActionResult> MakeOrder(OrderRequest request)
        {
            var userInDb = _context.Users.FirstOrDefault(u => u.UserName == User.Identity.Name);
            var order = new Order {
                User = userInDb,
                CreatedDate = DateTime.UtcNow,
                SpecialRequests = request.SpecialRequests,
                Status = "Wait",
                Dish = request.Dish
            };

                _context.Order.Add(order);
                _context.SaveChanges();
           
            return Ok(_context.Order.Count().ToString());
        }

        [HttpPost]
        [Route("RunOrderProcessor"), Authorize]
        public async Task<IActionResult> RunOrderProcessor(OrderProcessorRequest request)
        {
            var starttime = DateTime.Now;
            var context = new UsersContext();
            Task task = new Task(() =>
            {
                while (true) {
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

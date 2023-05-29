using AuthorizationRestaurant.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace BisnessLogic.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string UserId { get; set; } 
        public User? User { get; set; }
        public string Status { get; set; }
        public string Dish { get; set; }
        public string SpecialRequests { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}

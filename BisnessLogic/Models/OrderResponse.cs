using AuthorizationRestaurant.Models;

namespace BisnessLogic.Models
{
    public class OrderResponse
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Status { get; set; }
        public string Dish { get; set; }
        public string SpecialRequests { get; set; }
    }
}

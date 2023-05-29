using AuthorizationRestaurant.Models;

namespace BisnessLogic.Models
{
    public class Order
    {
        public int Id { get; set; }
        public User User { get; set; }
        public string Status { get; set; }
        public string Dish { get; set; }
        public string SpecialRequests { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}

using AuthorizationRestaurant.Models;

namespace BisnessLogic.Models
{
    public class Menu
    {
        public int Id { get; set; }
        public string Dish { get; set; }
        public int Price { get; set; }
        public bool Access { get; set; }

    }
}

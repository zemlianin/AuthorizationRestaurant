using Microsoft.AspNetCore.Identity;
using System.Data;

namespace AuthorizationRestaurant.Models
{
    public class User : IdentityUser
    {
        public int Age { get; set; }
        public string Role { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}

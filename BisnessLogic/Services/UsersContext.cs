namespace AuthorizationRestaurant.Services
{
    using AuthorizationRestaurant.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    public class UsersContext : IdentityUserContext<User>
    {
        public UsersContext()
        {
        }

        public UsersContext(DbContextOptions<UsersContext> options)
            : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // It would be a good idea to move the connection string to user secrets
            options.UseNpgsql("Server=localhost;Port=8080;User Id=app;Password=app;Database=mydbname2;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}

using Database.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Database.DAL
{
    public class UsersdbContext:IdentityDbContext<UserModel>
    {
        public UsersdbContext(DbContextOptions<UsersdbContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
          => optionsBuilder.UseNpgsql("host='127.0.0.1'; port=5432 ;DataBase='archival'; user id=postgres;");
        public DbSet<UserModel> Staff { get; set; }
    }
}

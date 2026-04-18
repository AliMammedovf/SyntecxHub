using Microsoft.EntityFrameworkCore;
using SyntecxhubUserApi.Models;

namespace SyntecxhubUserApi.Data
{
    public class AppDbContext: DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }


        public DbSet<User> Users { get; set; }
    }
}

using Microsoft.EntityFrameworkCore;
using PeopleDatabase.Models;

namespace PeopleDatabase.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Person> Persons { get; set; }
    }
}

using apitest.Models;
using Microsoft.EntityFrameworkCore;

namespace apitest.Data
{
    public class ContactsApiDbContext : DbContext
    {

        public ContactsApiDbContext(DbContextOptions<ContactsApiDbContext> options) : base(options)
        {

        }

        public DbSet<Contacts> contacts { get; set; }
    }


}


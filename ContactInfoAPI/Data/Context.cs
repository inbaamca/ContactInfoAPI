using ContactInfoAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactInfoAPI.Data
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> option) : base(option)
        {

        }
        public DbSet<Contact> Contact { get; set; }
    }
}

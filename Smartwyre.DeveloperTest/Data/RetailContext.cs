using Microsoft.EntityFrameworkCore;
using Smartwyre.DeveloperTest.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smartwyre.DeveloperTest.Data
{
    public class RetailContext : DbContext
    {
        public RetailContext(DbContextOptions<RetailContext> options) : base(options)
        {
        }

        public DbSet<Rebate> Rebates { get; set; }
        
        public DbSet<Product> Products { get; set; }
    }
}

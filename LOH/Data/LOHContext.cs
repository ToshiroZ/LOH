using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using LOH;

namespace LOH.Data
{
    public class LOHContext : DbContext
    {
        public LOHContext (DbContextOptions<LOHContext> options)
            : base(options)
        {
        }

        public DbSet<LOH.User> User { get; set; }
    }
}

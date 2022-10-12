using DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndWebAPI.DataContext
{
    public class ApplicationContext:DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options):base(options)
        {

        }
        public DbSet<Duser> Dusers { get; set; }

        protected override void OnModelCreating(ModelBuilder buidler)
        {
            base.OnModelCreating(buidler);
        }
    }
}

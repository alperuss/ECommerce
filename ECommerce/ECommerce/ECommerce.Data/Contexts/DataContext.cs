using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Data.Contexts
{
    public class DataContext:DbContext
    {
        public DataContext(DbContextOptions<DataContext>dbContextoptions):base(dbContextoptions)
        {

        }
        public DbSet<Entities.User> Users { get; set; }
    }
}

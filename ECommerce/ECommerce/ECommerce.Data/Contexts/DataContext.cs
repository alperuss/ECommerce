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
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Entities.User>().HasData(new Entities.User()
            {
                Id=1,
                Active = true,
                Admin = true,
                CreateDate = DateTime.UtcNow,
                Deleted = false,
                Email = "admin@admin.com",
                Name = "Admin",
                Surname = "Admin",
                Password = Helper.CryptoHelper.Sha1("12345678")
            });
        }
    }
}

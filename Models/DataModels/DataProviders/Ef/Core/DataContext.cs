using DataModel.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataModel.DataProviders.Ef.Core;

public class DataContext : DbContext
{
    public DbSet<Order> Orders { get; set; } = null!;
    public DbSet<OrdersProducts> OrderProducts { get; set; } = null!;
    public DbSet<PickupPoint> PickupPoints { get; set; } = null!;
    public DbSet<Product> Products { get; set; } = null!;
    public DbSet<User> Users { get; set; } = null!;
    protected override void OnModelCreating(ModelBuilder mb)
    {
        //var admin = new User()
        //{
        //    Id = 1,
        //    Login = "root",
        //    FirstName = "N/A",
        //    MiddleName = "N/A",
        //    LastName = "N/A",
        //    Password = User.ToHashString("root"),
        //    Administrator = true
        //};
        //var guest = new User()
        //{
        //    Id = 2,
        //    Login = "guest",
        //    FirstName = "N/A",
        //    MiddleName = "N/A",
        //    LastName = "N/A",
        //    Password = User.ToHashString("12345"),
        //    Administrator = false
        //};
        //mb.Entity<User>().HasData(admin);
        //mb.Entity<User>().HasData(guest);
    }
}

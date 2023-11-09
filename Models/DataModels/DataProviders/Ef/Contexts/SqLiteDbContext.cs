using DataModel.DataProviders.Ef.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Sqlite;

using System;
using System.Collections.Generic;
using System.Text;

namespace DataModel.DataProviders.Ef.Contexts;

public class SqLiteDbContext : DataContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        builder.UseSqlite(@"Data Source = C:\Data\db.db");
    }
}

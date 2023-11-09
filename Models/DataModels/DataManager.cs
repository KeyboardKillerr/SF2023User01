using DataModel.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using DataModel.DataProviders.Ef.Core.Repositories;
using System.IO;

namespace DataModel;

public class DataManager
{
    public IOrderProductsRep OrderProducts { get; }
    public IOrderRep Orders { get; }
    public IPickupPointRep PickupPoints { get; }
    public IProductRep Products { get; }
    public IUserRep Users { get; }

    private DataManager(
        IOrderProductsRep orderProducts,
        IOrderRep order,
        IPickupPointRep pickupPoint,
        IProductRep product,
        IUserRep user)
    {
        OrderProducts = orderProducts;
        Orders = order;
        PickupPoints = pickupPoint;
        Products = product;
        Users = user;
    }

    public static DataManager Get(DataProvidersList dataProviders)
    {
        switch (dataProviders)
        {
            case DataProvidersList.Json:
            case DataProvidersList.Txt:
            case DataProvidersList.Oracle:
            case DataProvidersList.SqLite:
                var sqliteContext = new DataProviders.Ef.Contexts.SqlServerDbContext();
                if (!Directory.Exists(@"C:\Data")) Directory.CreateDirectory(@"C:\Data");
                if (!sqliteContext.Database.EnsureCreated()) throw new Exception();
                throw new NotSupportedException("Поставщики данных находятся в стадии разработки");
            case DataProvidersList.MySql:
                throw new NotSupportedException("Поставщики данных находятся в стадии разработки");
            case DataProvidersList.SqlServer:
                var sqlserverContext = new DataProviders.Ef.Contexts.SqlServerDbContext();
                sqlserverContext.Database.EnsureCreated();
                return new DataManager
                (
                    new EfOrderProducts(sqlserverContext),
                    new EfOrder(sqlserverContext),
                    new EfPickupPoint(sqlserverContext),
                    new EfProduct(sqlserverContext),
                    new EfUser(sqlserverContext)
                );
            default:
                throw new NotSupportedException("Поставщики данных неизвестен");
        }
    }
}

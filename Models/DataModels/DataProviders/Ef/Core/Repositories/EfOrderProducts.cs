using DataModel.Entities;
using DataModel.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel.DataProviders.Ef.Core.Repositories;

public class EfOrderProducts : IOrderProductsRep
{
    protected readonly DataContext Context;
    public EfOrderProducts(DataContext context) => Context = context;
    public IQueryable<OrdersProducts> Items => Context.OrderProducts;

    public async Task<int> DeleteAsync(int orderId, string productId)
    {
        var item = await Items.FirstOrDefaultAsync(x => x.OrderId == orderId && x.ProductId == productId);
        if (item != default)
        {
            Context.Remove(item);
            return await Context.SaveChangesAsync();
        }
        return 0;
    }

    public async Task<OrdersProducts?> GetItemByIdAsync(int orderId, string productId)
    {
        return await Items.FirstOrDefaultAsync(x => x.OrderId == orderId && x.ProductId == productId);
    }

    public async Task<int> UpdateAsync(OrdersProducts table)
    {
        var item = await Items.FirstOrDefaultAsync(x => x.OrderId == table.OrderId && x.ProductId == table.ProductId);
        if (item != default) Context.Update(table);
        else await Context.AddAsync(table);
        return await Context.SaveChangesAsync();
    }


    public void Insert(OrdersProducts table)
    {
        Context.Database.ExecuteSqlRaw(
            "INSERT INTO [dbo].[OrderProducts] (" +
            "OrderId," +
            "ProductId," +
            "Quantity)" +
            "VALUES (" +
            $"'{table.OrderId}'," +
            $"'{table.ProductId}'," +
            $"'{table.Quantity}');"
            );
    }
}

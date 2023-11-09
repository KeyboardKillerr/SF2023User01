using DataModel.Entities;
using DataModel.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel.DataProviders.Ef.Core.Repositories;

public class EfOrder : IOrderRep
{
    protected readonly DataContext Context;
    public EfOrder(DataContext context) => Context = context;
    public IQueryable<Order> Items => Context.Orders
        .Include(x => x.PickupPointKey)
        .Include(x => x.UserKey)
        .Include(x => x.OrdersProducts);

    public async Task<int> DeleteAsync(int id)
    {
        var item = await Items.FirstOrDefaultAsync(x => x.Id == id);
        if (item != default)
        {
            Context.Remove(item);
            return await Context.SaveChangesAsync();
        }
        return 0;
    }

    public async Task<Order?> GetItemByIdAsync(int id)
    {
        return await Items.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<int> UpdateAsync(Order table)
    {
        var item = await Items.FirstOrDefaultAsync(x => x.Id == table.Id);
        if (item != default) Context.Update(table);
        else await Context.AddAsync(table);
        return await Context.SaveChangesAsync();
    }

    public void Insert(Order table)
    {
        Context.Database.ExecuteSqlRaw(
            "SET IDENTITY_INSERT [dbo].[Orders] ON;" +
            "INSERT INTO [dbo].[Orders] (" +
            "Id," +
            "OrderDate," +
            "DeliveryDate," +
            "ConfirmationCode," +
            "OrderStatus," +
            "UserKey," +
            "PickupPointKey)" +
            "VALUES (" +
            $"'{table.Id}'," +
            $"'{table.OrderDate:MM.dd.yyyy}'," +
            $"'{table.DeliveryDate:MM.dd.yyyy}'," +
            $"'{table.ConfirmationCode}'," +
            $"'{table.OrderStatus}'," +
            $"'{table.UserKey}'," +
            $"'{table.PickupPointKey}');" +
            "SET IDENTITY_INSERT [dbo].[Orders] OFF;"
            );
    }
}

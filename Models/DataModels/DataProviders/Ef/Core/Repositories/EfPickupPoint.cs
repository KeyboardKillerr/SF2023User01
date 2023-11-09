using DataModel.Entities;
using DataModel.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel.DataProviders.Ef.Core.Repositories;

public class EfPickupPoint : IPickupPointRep
{
    protected readonly DataContext Context;
    public EfPickupPoint(DataContext context) => Context = context;
    public IQueryable<PickupPoint> Items => Context.PickupPoints;

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

    public async Task<PickupPoint?> GetItemByIdAsync(int id)
    {
        return await Items.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<int> UpdateAsync(PickupPoint table)
    {
        var item = await Items.FirstOrDefaultAsync(x => x.Id == table.Id);
        if (item != default) Context.Update(table);
        else await Context.AddAsync(table);
        return await Context.SaveChangesAsync();
    }


    public void Insert(PickupPoint table)
    {
        Context.Database.ExecuteSqlRaw(
            "SET IDENTITY_INSERT [dbo].[PickupPoints] ON;" +
            "INSERT INTO [dbo].[PickupPoints] (" +
            "Id," +
            "PostalCode," +
            "City," +
            "Street," +
            "Building)" +
            "VALUES (" +
            $"'{table.Id}'," +
            $"'{table.PostalCode}'," +
            $"'{table.City}'," +
            $"'{table.Street}'," +
            $"'{table.Building}');" +
            "SET IDENTITY_INSERT [dbo].[PickupPoints] OFF;"
            );
    }
}

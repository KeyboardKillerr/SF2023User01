using DataModel.Entities;
using DataModel.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel.DataProviders.Ef.Core.Repositories;

public class EfProduct : IProductRep
{
    protected readonly DataContext Context;
    public EfProduct(DataContext context) => Context = context;
    public IQueryable<Product> Items => Context.Products
        .Include(x => x.OrdersProducts);

    public async Task<int> DeleteAsync(string id)
    {
        var item = await Items.FirstOrDefaultAsync(x => x.Id == id);
        if (item != default)
        {
            Context.Remove(item);
            return await Context.SaveChangesAsync();
        }
        return 0;
    }

    public async Task<Product?> GetItemByIdAsync(string id)
    {
        return await Items.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<int> UpdateAsync(Product table)
    {
        var item = await Items.FirstOrDefaultAsync(x => x.Id == table.Id);
        if (item != default) Context.Update(table);
        else await Context.AddAsync(table);
        return await Context.SaveChangesAsync();
    }


    public void Insert(Product table)
    {
        Context.Database.ExecuteSqlRaw(
            "INSERT INTO [dbo].[Products] (" +
            "Id," +
            "Price," +
            "MaximumDiscount," +
            "CurrentDiscount," +
            "StorageQuantity," +
            "Type," +
            "MeasurmentUnit," +
            "Manufacturer," +
            "Dealer," +
            "Category," +
            "Description," +
            "ImagePath)" +
            "VALUES (" +
            $"'{table.Id}'," +
            $"'{table.Price}'," +
            $"'{table.MaximumDiscount}'," +
            $"'{table.CurrentDiscount}'," +
            $"'{table.StorageQuantity}'," +
            $"'{table.Type}'," +
            $"'{table.MeasurmentUnit}'," +
            $"'{table.Manufacturer}'," +
            $"'{table.Dealer}'," +
            $"'{table.Category}'," +
            $"'{table.Description}'," +
            $"'{table.ImagePath}');"
            );
    }
}

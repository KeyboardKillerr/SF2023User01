using DataModel.Entities;
using DataModel.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DataModel.DataProviders.Ef.Core.Repositories;

public class EfUser : IUserRep
{
    protected readonly DataContext Context;
    public EfUser(DataContext context) => Context = context;
    public IQueryable<User> Items => Context.Users;

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

    public async Task<User?> GetItemByIdAsync(int id)
    {
        return await Items.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<int> UpdateAsync(User table)
    {
        var item = await Items.FirstOrDefaultAsync(x => x.Id == table.Id);
        if (item != default) Context.Update(table);
        else await Context.AddAsync(table);
        return await Context.SaveChangesAsync();
    }

    public void Insert(User table)
    {
        Context.Database.ExecuteSqlRaw(
            "SET IDENTITY_INSERT [dbo].[Users] ON;" +
            "INSERT INTO [dbo].[Users] (" +
            "Id," +
            "Login," +
            "FirstName," +
            "MiddleName," +
            "LastName," +
            "Password," +
            "Role)" +
            "VALUES (" +
            $"'{table.Id}'," +
            $"'{table.Login}'," +
            $"'{table.FirstName}'," +
            $"'{table.MiddleName}'," +
            $"'{table.LastName}'," +
            $"'{table.Password}'," +
            $"'{table.Role}');" +
            "SET IDENTITY_INSERT [dbo].[Users] OFF;"
            );
    }
}

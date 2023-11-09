using DataModel.Entities;
using DataModel.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel.DataProviders.Ef.Core.Repositories
{
    public class EfUsersA : IUsersARep
    {
        protected readonly DataContext Context;
        public EfUsersA(DataContext context) => Context = context;
        public IQueryable<UsersA> Items => Context.UsersAs;

        public async Task<int> DeleteAsync(int id)
        {
            var item = await Items.FirstOrDefaultAsync(x => x.UserId == id);
            if (item != default)
            {
                Context.Remove(item);
                return await Context.SaveChangesAsync();
            }
            return 0;
        }

        public async Task<UsersA?> GetItemByIdAsync(int id)
        {
            return await Items.FirstOrDefaultAsync(x => x.UserId == id);
        }

        public async Task<int> UpdateAsync(UsersA table)
        {
            var item = await Items.FirstOrDefaultAsync(x => x.UserId == table.UserId);
            if (item != default) Context.Update(table);
            else await Context.AddAsync(table);
            return await Context.SaveChangesAsync();
        }
    }
}

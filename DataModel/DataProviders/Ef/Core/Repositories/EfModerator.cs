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
    public class EfModerator : IModeratorRep
    {
        protected readonly DataContext Context;
        public EfModerator(DataContext context) => Context = context;
        public IQueryable<Moderator> Items => Context.Moderators;

        public async Task<int> DeleteAsync(int id)
        {
            var item = await Items.FirstOrDefaultAsync(x => x.ModeratorId == id);
            if (item != default)
            {
                Context.Remove(item);
                return await Context.SaveChangesAsync();
            }
            return 0;
        }

        public async Task<Moderator?> GetItemByIdAsync(int id)
        {
            return await Items.FirstOrDefaultAsync(x => x.ModeratorId == id);
        }

        public async Task<int> UpdateAsync(Moderator table)
        {
            var item = await Items.FirstOrDefaultAsync(x => x.ModeratorId == table.ModeratorId);
            if (item != default) Context.Update(table);
            else await Context.AddAsync(table);
            return await Context.SaveChangesAsync();
        }
    }
}

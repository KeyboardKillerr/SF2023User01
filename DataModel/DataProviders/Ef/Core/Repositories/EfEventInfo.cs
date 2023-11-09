using DataModel.Entities;
using DataModel.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DataModel.DataProviders.Ef.Core.Repositories
{
    public class EfEventInfo : IEventInfoRep
    {
        protected readonly DataContext Context;
        public EfEventInfo(DataContext context) => Context = context;
        public IQueryable<Entities.EventInfo> Items => Context.EventInfos;

        public async Task<int> DeleteAsync(int id)
        {
            var item = await Items.FirstOrDefaultAsync(x => x.EventInfoId == id);
            if (item != default)
            {
                Context.Remove(item);
                return await Context.SaveChangesAsync();
            }
            return 0;
        }

        public async Task<Entities.EventInfo?> GetItemByIdAsync(int id)
        {
            return await Items.FirstOrDefaultAsync(x => x.EventInfoId == id);
        }

        public async Task<int> UpdateAsync(Entities.EventInfo table)
        {
            var item = await Items.FirstOrDefaultAsync(x => x.EventInfoId == table.EventInfoId);
            if (item != default) Context.Update(table);
            else await Context.AddAsync(table);
            return await Context.SaveChangesAsync();
        }
    }
}

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
    public class EfEvent : IEventRep
    {
        protected readonly DataContext Context;
        public EfEvent(DataContext context) => Context = context;
        public IQueryable<Event> Items => Context.Events;

        public async Task<int> DeleteAsync(int id)
        {
            var item = await Items.FirstOrDefaultAsync(x => x.EventId == id);
            if (item != default)
            {
                Context.Remove(item);
                return await Context.SaveChangesAsync();
            }
            return 0;
        }

        public async Task<Event?> GetItemByIdAsync(int id)
        {
            return await Items.FirstOrDefaultAsync(x => x.EventId == id);
        }

        public async Task<int> UpdateAsync(Event table)
        {
            var item = await Items.FirstOrDefaultAsync(x => x.EventId == table.EventId);
            if (item != default) Context.Update(table);
            else await Context.AddAsync(table);
            return await Context.SaveChangesAsync();
        }
    }
}

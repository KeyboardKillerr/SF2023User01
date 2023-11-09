using DataModel.Entities;
using DataModel.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel.DataProviders.Ef.Core.Repositories
{
    public class EfEventsDirection : IEventsDirectionRep
    {
        protected readonly DataContext Context;
        public EfEventsDirection(DataContext context) => Context = context;
        public IQueryable<EventsDirection> Items => Context.EventsDirections;

        public async Task<int> DeleteAsync(int eventId, int directionId)
        {
            var item = await Items.FirstOrDefaultAsync(x => x.EventId == eventId && x.DirectionId == directionId);
            if (item != default)
            {
                Context.Remove(item);
                return await Context.SaveChangesAsync();
            }
            return 0;
        }

        public async Task<EventsDirection?> GetItemByIdAsync(int eventId, int directionId)
        {
            return await Items.FirstOrDefaultAsync(x => x.EventId == eventId && x.DirectionId == directionId);
        }

        public async Task<int> UpdateAsync(EventsDirection table)
        {
            var item = await Items.FirstOrDefaultAsync(x => x.EventId == table.EventId && x.DirectionId == table.DirectionId);
            if (item != default) Context.Update(table);
            else await Context.AddAsync(table);
            return await Context.SaveChangesAsync();
        }
    }
}

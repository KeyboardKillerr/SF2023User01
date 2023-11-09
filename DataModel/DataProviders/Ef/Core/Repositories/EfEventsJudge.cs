using DataModel.Entities;
using DataModel.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel.DataProviders.Ef.Core.Repositories
{
    public class EfEventsJudge : IEventsJudgeRep
    {
        protected readonly DataContext Context;
        public EfEventsJudge(DataContext context) => Context = context;
        public IQueryable<EventsJudge> Items => Context.EventsJudges;

        public async Task<int> DeleteAsync(int eventId, int judgeId)
        {
            var item = await Items.FirstOrDefaultAsync(x => x.EventId == eventId && x.JudgeId == judgeId);
            if (item != default)
            {
                Context.Remove(item);
                return await Context.SaveChangesAsync();
            }
            return 0;
        }

        public async Task<EventsJudge?> GetItemByIdAsync(int eventId, int judgeId)
        {
            return await Items.FirstOrDefaultAsync(x => x.EventId == eventId && x.JudgeId == judgeId);
        }

        public async Task<int> UpdateAsync(EventsJudge table)
        {
            var item = await Items.FirstOrDefaultAsync(x => x.EventId == table.EventId && x.JudgeId == table.JudgeId);
            if (item != default) Context.Update(table);
            else await Context.AddAsync(table);
            return await Context.SaveChangesAsync();
        }
    }
}

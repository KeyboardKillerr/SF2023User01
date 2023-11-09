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
    public class EfJudge : IJudgeRep
    {
        protected readonly DataContext Context;
        public EfJudge(DataContext context) => Context = context;
        public IQueryable<Judge> Items => Context.Judges;

        public async Task<int> DeleteAsync(int id)
        {
            var item = await Items.FirstOrDefaultAsync(x => x.JudgeId == id);
            if (item != default)
            {
                Context.Remove(item);
                return await Context.SaveChangesAsync();
            }
            return 0;
        }

        public async Task<Judge?> GetItemByIdAsync(int id)
        {
            return await Items.FirstOrDefaultAsync(x => x.JudgeId == id);
        }

        public async Task<int> UpdateAsync(Judge table)
        {
            var item = await Items.FirstOrDefaultAsync(x => x.JudgeId == table.JudgeId);
            if (item != default) Context.Update(table);
            else await Context.AddAsync(table);
            return await Context.SaveChangesAsync();
        }
    }
}

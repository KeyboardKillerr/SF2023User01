using DataModel.Entities;
using DataModel.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel.DataProviders.Ef.Core.Repositories
{
    public class EfDirection : IDirectionRep
    {
        protected readonly DataContext Context;
        public EfDirection(DataContext context) => Context = context;
        public IQueryable<Direction> Items => Context.Directions;

        public async Task<int> DeleteAsync(int id)
        {
            var item = await Items.FirstOrDefaultAsync(x => x.DirectionId == id);
            if (item != default)
            {
                Context.Remove(item);
                return await Context.SaveChangesAsync();
            }
            return 0;
        }

        public async Task<Direction?> GetItemByIdAsync(int id)
        {
            return await Items.FirstOrDefaultAsync(x => x.DirectionId == id);
        }

        public async Task<int> UpdateAsync(Direction table)
        {
            var item = await Items.FirstOrDefaultAsync(x => x.DirectionId == table.DirectionId);
            if (item != default) Context.Update(table);
            else await Context.AddAsync(table);
            return await Context.SaveChangesAsync();
        }
    }
}

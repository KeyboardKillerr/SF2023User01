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
    public class EfCity : ICityRep
    {
        protected readonly DataContext Context;
        public EfCity(DataContext context) => Context = context;
        public IQueryable<City> Items => Context.Cities;

        public async Task<int> DeleteAsync(int id)
        {
            var item = await Items.FirstOrDefaultAsync(x => x.CityId == id);
            if (item != default)
            {
                Context.Remove(item);
                return await Context.SaveChangesAsync();
            }
            return 0;
        }

        public async Task<City?> GetItemByIdAsync(int id)
        {
            return await Items.FirstOrDefaultAsync(x => x.CityId == id);
        }

        public async Task<int> UpdateAsync(City table)
        {
            var item = await Items.FirstOrDefaultAsync(x => x.CityId == table.CityId);
            if (item != default) Context.Update(table);
            else await Context.AddAsync(table);
            return await Context.SaveChangesAsync();
        }
    }
}

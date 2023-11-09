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
    public class EfCountry : ICountryRep
    {
        protected readonly DataContext Context;
        public EfCountry(DataContext context) => Context = context;
        public IQueryable<Country> Items => Context.Countries;

        public async Task<int> DeleteAsync(int id)
        {
            var item = await Items.FirstOrDefaultAsync(x => x.CountryId == id);
            if (item != default)
            {
                Context.Remove(item);
                return await Context.SaveChangesAsync();
            }
            return 0;
        }

        public async Task<Country?> GetItemByIdAsync(int id)
        {
            return await Items.FirstOrDefaultAsync(x => x.CountryId == id);
        }

        public async Task<int> UpdateAsync(Country table)
        {
            var item = await Items.FirstOrDefaultAsync(x => x.CountryId == table.CountryId);
            if (item != default) Context.Update(table);
            else await Context.AddAsync(table);
            return await Context.SaveChangesAsync();
        }
    }
}

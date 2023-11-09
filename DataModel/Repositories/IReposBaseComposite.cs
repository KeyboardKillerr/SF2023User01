using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel.Repositories
{
    public interface IReposBaseComposite<TTable> where TTable : class
    {
        IQueryable<TTable> Items { get; }
        Task<TTable?> GetItemByIdAsync(int id1, int id2);
        Task<int> UpdateAsync(TTable table);
        Task<int> DeleteAsync(int id1, int id2);
    }
}

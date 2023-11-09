using DataModel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel.Repositories;

public interface IProductRep : IInsertable<Product>
{
    IQueryable<Product> Items { get; }
    Task<Product?> GetItemByIdAsync(string id);
    Task<int> UpdateAsync(Product table);
    Task<int> DeleteAsync(string id);
}

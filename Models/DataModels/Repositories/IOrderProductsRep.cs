using DataModel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel.Repositories;

public interface IOrderProductsRep : IInsertable<OrdersProducts>
{
    IQueryable<OrdersProducts> Items { get; }
    Task<OrdersProducts?> GetItemByIdAsync(int orderKey, string productKey);
    Task<int> UpdateAsync(OrdersProducts table);
    Task<int> DeleteAsync(int orderKey, string productKey);
}

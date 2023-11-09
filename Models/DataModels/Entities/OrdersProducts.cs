﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Metrics;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel.Entities;

[PrimaryKey(nameof(OrderId), nameof(ProductId))]
public class OrdersProducts
{
    public int OrderId { get; set; }
    public string ProductId { get; set; } = Product.AutoGeneratedIdUnsafe();
    public virtual Order Order { get; set; } = null!;
    public virtual Product Product { get; set; } = null!;
    public int Quantity { get; set; } = 1;
}

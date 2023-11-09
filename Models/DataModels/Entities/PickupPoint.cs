using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel.Entities;

public class PickupPoint : EntityBase
{
    public int PostalCode { get; set; }
    public string City { get; set; } = null!;
    public string Street { get; set; } = null!;
    public int Building { get; set; }
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel.Entities;

public class Order : EntityBase
{
    public DateTime OrderDate { get; init; }
    public DateTime DeliveryDate { get; set; }
    public int ConfirmationCode { get; set; }
    public string OrderStatus { get; set; } = null!;
    [ForeignKey(nameof(Entities.User))]
    public int UserKey { get; set; }
    public User User { get; set; } = null!;

    [ForeignKey(nameof(Entities.PickupPoint))]
    public int PickupPointKey { get; set; }
    public PickupPoint PickupPoint { get; set; } = null!;
    public virtual ICollection<OrdersProducts> OrdersProducts { get; set; } = new Collection<OrdersProducts>();
}

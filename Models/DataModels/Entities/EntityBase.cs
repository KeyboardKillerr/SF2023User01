using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataModel.Entities;

public abstract class EntityBase
{
    //[DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int Id { get; init; }
}

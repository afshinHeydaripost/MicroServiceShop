using Helper.Base;
using System;
using System.Collections.Generic;

namespace Products.DataModel.Models;

public partial class ProductRate : BaseEntity
{

    public byte Rate { get; set; }

    public int ProductId { get; set; }

    public int UserId { get; set; }

    public string UserIp { get; set; }

    public string CreaateDateTime { get; set; }

    public virtual Product Product { get; set; }
}

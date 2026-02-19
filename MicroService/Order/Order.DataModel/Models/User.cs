using System;
using System.Collections.Generic;

namespace Order.DataModel.Models;

public partial class User
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public string UserCode { get; set; }

    public string UserName { get; set; }

    public string Email { get; set; }

    public string PhoneNumber { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public bool EmailConfirmed { get; set; }

    public bool PhoneNumberConfirmed { get; set; }

    public DateTime CreateDateTime { get; set; }

    public DateTime? UpdateDateTime { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}

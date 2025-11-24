using Helper.Base;
using System;
using System.Collections.Generic;

namespace AuthService.DataModel.Models;

public partial class RefreshToken : BaseEntity
{

    public int UserId { get; set; }

    public string Token { get; set; }

    public DateTime ExpiresDateTime { get; set; }

    public bool Revoked { get; set; }

    public DateTime CreateDateTime { get; set; }

    public string CreatedByIp { get; set; }

    public string ReplacedByToken { get; set; }

    public virtual User User { get; set; }
}

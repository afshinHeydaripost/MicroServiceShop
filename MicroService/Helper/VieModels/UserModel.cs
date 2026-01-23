using Helper.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helper.VieModels
{
    internal class UserModel
    {
    }
    public  class UserViewModel : BaseEntity
    {
        public string UserCode { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Password { get; set; }

        public bool EmailConfirmed { get; set; }

        public bool PhoneNumberConfirmed { get; set; }

        public string CreateDateTime { get; set; }

        public string UpdateDateTime { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public bool RememberMe { get; set; } = false;

    }
    public class LoginRequestViewModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
        public string ipAddress { get; set; }
        public string Token { get; set; }

    }
}

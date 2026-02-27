using Helper.Base;
using Helper.Resources;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helper.VieModels
{
    internal class UserModel
    {
    }
    public class UserViewModel : BaseEntity
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

        public DateTime CreateDateTime { get; set; }
        public string StrCreateDateTime => CreateDateTime.ToString("yyyy/MMM/dd hh:mm:ss");

        public DateTime? UpdateDateTime { get; set; }
        public string StrUpdateDateTime => (UpdateDateTime == null) ? "" : UpdateDateTime.Value.ToString("yyyy/MMM/dd hh:mm:ss");
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public bool RememberMe { get; set; } = false;

    }
    public partial class UserRoleViewModel: BaseIdObj
    {

        public int UserId { get; set; }
        public string RoleName { get; set; }

        public int RoleId { get; set; }
        public bool UserRoles { get; set; }
        public int[] RolesIds { get; set; }
    }
    public class LoginRequestViewModel
    {
        [Display(Name = "Username", ResourceType = typeof(Resource))]
        [Required(
            ErrorMessageResourceType = typeof(Resource),
            ErrorMessageResourceName = "RequiredField"
        )]
        public string Username { get; set; }

        [Display(Name = "Password", ResourceType = typeof(Resource))]
        [Required(
            ErrorMessageResourceType = typeof(Resource),
            ErrorMessageResourceName = "RequiredField"
        )]
        public string Password { get; set; }

        [Display(Name = "RememberMe", ResourceType = typeof(Resource))]
        public bool RememberMe { get; set; }
        public string ipAddress { get; set; }
        public string Token { get; set; }

    }
}

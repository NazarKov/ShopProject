using ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi.DtoModels.SignatureKey;
using ShopProjectDataBase.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi.DtoModels.User
{
    public class CreateUserDto
    {
        public string Login { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string TIN { get; set; } = string.Empty;
        public bool AutomaticLogin { get; set; }
        public int Status { get; set; }
        public int UserRoleID { get; set; }
        public SignatureKeyDto? SignatureKey { get; set; }
        public DateTimeOffset? CreatedAt { get; set; }
    }
}

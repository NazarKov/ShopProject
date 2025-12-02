using ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi.DtoModels.SignatureKey;
using ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi.DtoModels.User;
using ShopProject.UIModel.UserPage; 
using ShopProjectDataBase.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi.Mapping
{
    public static class UserMappingExtensions
    {
        public static User ToUser(this UserDto user)
        {
            var result = new User()
            {
                ID = Guid.Parse(user.ID),
                AutomaticLogin = user.AutomaticLogin,
                Status =  (TypeStatusUser)user.Status,
                CreatedAt = user.CreatedAt,
                Email = user.Email,
                FullName = user.FullName,
                Login = user.Login,
                Password = user.Password,
                TIN = user.TIN, 
            };
            if (user.SignatureKeyID != null)
            {
                result.SignatureKey = new SignatureKey() { ID = Guid.Parse(user.SignatureKeyID) };
            }

            result.Role = Session.Roles.Where(i => i.ID == user.UserRoleID).First();
            return result;
        }

        public static IEnumerable<User> ToUser(this IEnumerable<UserDto> users)
        {
            var result = new List<User>();
            foreach(var user in users)
            {
                result.Add(ToUser(user));
            }
            return result;
        }

        public static User ToUser(this AuthorizationUserDto user)
        {
            var result = new User()
            { 
                Login = user.Login,
                AutomaticLogin = user.AutomaticLogin,
                Email = user.Email,
                FullName = user.FullName,
                TIN = user.TIN,
                Token = user.Token, 
            };
            if(Session.Roles != null)
            {
                result.Role = Session.Roles.Where(i => i.ID == user.UserRoleID).First();
            }
            return result;
        }

        public static CreateUserDto ToCreateUserDto(this User user)
        {
            var item = new CreateUserDto()
            {
                AutomaticLogin = user.AutomaticLogin,
                Status = (int)user.Status,
                CreatedAt = user.CreatedAt,
                Email = user.Email,
                FullName = user.FullName,
                Login = user.Login,
                TIN = user.TIN,
                Password = user.Password, 
            };
            if (user.Role != null) 
            {
                item.UserRoleID = user.Role.ID;
            }

            if (user.SignatureKey != null) 
            {
                item.SignatureKey = new SignatureKeyDto();
                item.SignatureKey.CreateAt = user.SignatureKey.CreateAt;
                item.SignatureKey.EndAt = user.SignatureKey.EndAt;
                item.SignatureKey.SignaturePassword = user.SignatureKey.SignaturePassword;
                item.SignatureKey.Signature = user.SignatureKey.Signature;
            }
            return item;
        }
    }
}

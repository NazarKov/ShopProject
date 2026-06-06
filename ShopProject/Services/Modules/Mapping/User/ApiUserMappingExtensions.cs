using ShopProject.Model.Enum;
using ShopProject.Services.Integration.Network.ShopProjectWebServerApi.DtoModels.SignatureKey;
using ShopProject.Services.Integration.Network.ShopProjectWebServerApi.DtoModels.User;
using ShopProject.Services.Integration.Network.ShopProjectWebServerApi.DtoModels.UserRole;
using ShopProject.Services.Modules.Mapping.UserRole;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Services.Modules.Mapping.User
{
    public static class ApiUserMappingExtensions
    {
        public static ShopProject.Model.Domain.User.User ToUser(this UserDto user, IEnumerable<UserRoleDto> usersRoleDto)
        {
            var result = new ShopProject.Model.Domain.User.User()
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
                result.SignatureKey = new ShopProject.Model.Domain.SignatureKey.SignatureKey() { ID = Guid.Parse(user.SignatureKeyID) };
            }

            result.Role = usersRoleDto.Where(i => i.ID == user.UserRoleID).First().ToUserRole();
            return result;
        }

        public static IEnumerable<ShopProject.Model.Domain.User.User> ToUser(this IEnumerable<UserDto> users,IEnumerable<UserRoleDto> usersRoleDto)
        {
            var result = new List<ShopProject.Model.Domain.User.User>();
            foreach(var user in users)
            {
                result.Add(ToUser(user,usersRoleDto));
            }
            return result;
        }

        public static ShopProject.Model.Domain.User.User ToUser(this AuthorizationUserDto user, IEnumerable<UserRoleDto> usersRoleDto)
        {
            var result = new ShopProject.Model.Domain.User.User()
            { 
                Login = user.Login,
                AutomaticLogin = user.AutomaticLogin,
                Email = user.Email,
                FullName = user.FullName,
                TIN = user.TIN,
                Token = user.Token, 
            };
            result.Role = usersRoleDto.Where(i => i.ID == user.UserRoleID).First().ToUserRole();
            return result;
        }

        public static CreateUserDto ToCreateUserDto(this ShopProject.Model.Domain.User.User user)
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

        public static UpdateUserDto ToUpdateUserDto(this ShopProject.Model.Domain.User.User user)
        {
            var item = new UpdateUserDto()
            {
                AutomaticLogin = user.AutomaticLogin,
                Status = (int)user.Status,
                CreatedAt = user.CreatedAt,
                Email = user.Email,
                FullName = user.FullName,
                Login = user.Login,
                TIN = user.TIN,
                Password = user.Password,
                ID = user.ID.ToString(),
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

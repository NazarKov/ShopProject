using ShopProjectWebServer.Api.Common;
using ShopProjectWebServer.Api.DtoModels.User;
using ShopProjectWebServer.Models.Domain.Enum;
using ShopProjectWebServer.Models.Domain.Paginator;
using ShopProjectWebServer.Services.Modules.Mapping.SignatureKey;
using UserModel = ShopProjectWebServer.Models.Domain.User.User;

namespace ShopProjectWebServer.Services.Modules.Mapping.User
{
    public static class UserApiMappingExtensions
    {
        public static UserModel ToUser(this CreateUserDto item)
        {
            var userEntity = new UserModel()
            {
                AutomaticLogin = item.AutomaticLogin,
                CreatedAt = item.CreatedAt,
                Email = item.Email,
                FullName = item.FullName,
                Login = item.Login,
                Password = item.Password,
                TIN = item.TIN,
                UserRole = new ShopProjectWebServer.Models.Domain.UserRole.UserRole() { ID = item.UserRoleID }
            };

            if (item.SignatureKey != null)
            {
                userEntity.SignatureKey = new Models.Domain.SignatureKey.SignatureKey();
                userEntity.SignatureKey.CreateAt = item.SignatureKey.CreateAt;
                userEntity.SignatureKey.EndAt = item.SignatureKey.EndAt;
                userEntity.SignatureKey.SignaturePassword = item.SignatureKey.SignaturePassword;
                userEntity.SignatureKey.Signature = item.SignatureKey.Signature;
            }


            Enum.TryParse(item.Status.ToString(), out TypeStatusUser type);
            userEntity.Status = type;
            return userEntity;
        }
        public static UserModel ToUser(this UpdateUserDto item)
        {
            var userEntity = new UserModel()
            {
                ID = new Guid(item.ID),
                AutomaticLogin = item.AutomaticLogin,
                CreatedAt = item.CreatedAt,
                Email = item.Email,
                FullName = item.FullName,
                Login = item.Login,
                Password = item.Password,
                TIN = item.TIN,
                UserRole = new ShopProjectWebServer.Models.Domain.UserRole.UserRole() { ID = item.UserRoleID }
            };

            if (item.SignatureKey != null)
            {
                userEntity.SignatureKey = new Models.Domain.SignatureKey.SignatureKey();
                userEntity.SignatureKey.CreateAt = item.SignatureKey.CreateAt;
                userEntity.SignatureKey.EndAt = item.SignatureKey.EndAt;
                userEntity.SignatureKey.SignaturePassword = item.SignatureKey.SignaturePassword;
                userEntity.SignatureKey.Signature = item.SignatureKey.Signature;
            }


            Enum.TryParse(item.Status.ToString(), out TypeStatusUser type);
            userEntity.Status = type;
            return userEntity;
        }
        public static UserDto ToUserDto(this UserModel item)
        {
            var result = new UserDto()
            {
                ID = item.ID.ToString(),
                UserRoleID = item.UserRole.ID,
                Status = (int)item.Status,
                AutomaticLogin = item.AutomaticLogin,
                CreatedAt = item.CreatedAt,
                Email = item.Email,
                FullName = item.FullName,
                Login = item.Login,
                Password = item.Password,
                TIN = item.TIN,
            };
            if(item.SignatureKey != null)
            {
                result.SignatureKey = item.SignatureKey.ToSignatureKey();
            }
            return result;
        }
        public static IEnumerable<UserDto> ToUserDto(this IEnumerable<UserModel> item)
        {
            var result = new List<UserDto>();
            foreach (var itemEntity in item)
            {
                result.Add(ToUserDto(itemEntity));
            }
            return result;
        }
        public static AuthorizationUserDto ToAuthoUserDto(this UserModel item)
        {
            var result = new AuthorizationUserDto()
            {
                AutomaticLogin = item.AutomaticLogin,
                Email = item.Email,
                FullName = item.FullName,
                Login = item.Login,
                TIN = item.TIN,
                UserRoleID = item.UserRole.ID,
                Token = item.Token,
            };
            return result;
        }

        public static ShopProjectWebServer.Models.Domain.Paginator.Paginator<UserModel, int> ToPaginator(this PaginatorDto<UserDto, int> item)
        {
            return new Paginator<UserModel, int>()
            {
                CountItemPage = item.CountItemPage,
                DataType = item.DataType,
                Page = item.Page,
                Pages = item.Pages,
            };
        }
        public static PaginatorDto<UserDto, int> ToPaginatorDto(this ShopProjectWebServer.Models.Domain.Paginator.Paginator<UserModel, int> item)
        {
            var result = new PaginatorDto<UserDto, int>()
            {
                CountItemPage = item.CountItemPage,
                DataType = item.DataType,
                Page = item.Page,
                Pages = item.Pages,
            };
            if(item.Data!= null)
            {
                result.Data = item.Data.ToUserDto();
            }
            return result;
        }

        public static UserModel ToUser(this UserDto item)
        {
            var result = new UserModel()
            {
                ID = Guid.Parse(item.ID),
                UserRole = new Models.Domain.UserRole.UserRole() { ID = (int)item.UserRoleID },
                Status = (TypeStatusUser)item.Status,
                AutomaticLogin = item.AutomaticLogin,
                CreatedAt = item.CreatedAt,
                Email = item.Email,
                FullName = item.FullName,
                Login = item.Login,
                Password = item.Password,
                TIN = item.TIN, 
            };
            if (item.SignatureKey != null)
            {
                result.SignatureKey = item.SignatureKey.ToSignatureKey();
            }

            return result;
        }
        public static IEnumerable<UserModel> ToUser(this IEnumerable<UserDto> users)
        {
            var result = new List<UserModel>();
            foreach (var user in users) 
            {
                result.Add(ToUser(user));
            }
            return result;
        }
    }
}

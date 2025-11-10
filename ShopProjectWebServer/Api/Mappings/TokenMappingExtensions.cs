using ShopProjectDataBase.Entities;
using ShopProjectWebServer.Api.DtoModels.Token;

namespace ShopProjectWebServer.Api.Mappings
{
    public static class TokenMappingExtensions
    {
        public static TokenDto ToTokenDto(this TokenEntity item)
        {
            return new TokenDto()
            {
                CreateAt = item.CreateAt,
                Device = item.Device,
                Token = item.Token,
<<<<<<< HEAD
                UserID = item.User.ID.ToString()
=======
                User_ID = item.User.ID
>>>>>>> 306da6b87d87ea969d9567c60bf1dbf9a079baf4
            };
        }
    }
}

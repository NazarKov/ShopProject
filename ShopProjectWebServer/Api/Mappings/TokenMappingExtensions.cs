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
                UserID = item.User.ID.ToString() 
            };
        }
    }
}

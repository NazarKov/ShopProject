using ShopProjectWebServer.Api.DtoModels.MediaAccessControl;

namespace ShopProjectWebServer.Api.Interface.Services
{
    public interface IMediaAccessContolServise
    {
        public bool Add(string token , CreateMediaAccessControlDto createMediaAccessControlDto);
        public MediaAccessControlDto GetLastMediaAccessControl(string token,Guid id);
    }
}

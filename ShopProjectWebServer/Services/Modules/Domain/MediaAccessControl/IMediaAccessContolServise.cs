using ShopProjectWebServer.Api.DtoModels.MediaAccessControl;

namespace ShopProjectWebServer.Services.Modules.Domain.MediaAccessControl
{
    public interface IMediaAccessContolServise
    {
        public bool Add(string token , CreateMediaAccessControlDto createMediaAccessControlDto);
        public MediaAccessControlDto GetLastMediaAccessControl(string token,Guid id);
    }
}

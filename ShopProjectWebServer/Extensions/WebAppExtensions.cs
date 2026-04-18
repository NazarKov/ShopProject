using ShopProjectWebServer.Service.Integration.Directory.Interface;

namespace ShopProjectWebServer.Extensions
{
    public static class WebAppExtensions
    {
        public static void Init(this WebApplication app)
        {
            var service = app.Services.GetService(typeof(IDirectoryService));
            if (service is IDirectoryService directory) 
            {
                if (directory.IsCreateProgramFolders())
                {
                    return;
                }
                else
                {
                    directory.CreateProgramFolders();
                }
            }
        }
    }
}

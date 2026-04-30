namespace ShopProjectWebServer.Services.Infrastructure.ContolWebServer.Interface
{
    public interface IControlWebServerService
    {
        public Task<bool> CheckDataBaseHealth();
    }
}

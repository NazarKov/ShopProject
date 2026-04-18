namespace ShopProjectWebServer.Services.Modules.Authorization.Interface
{
    public interface IAuthorizationService
    {
        public bool LoginToken(string token);
        public void AddToken(string token);
    }
}

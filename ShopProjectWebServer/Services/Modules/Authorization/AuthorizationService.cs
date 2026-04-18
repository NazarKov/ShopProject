using ShopProjectWebServer.DataBase;
using ShopProjectWebServer.DataBase.Interface;
using ShopProjectWebServer.Services.Modules.Authorization.Interface;

namespace ShopProjectWebServer.Services.Modules.Authorization
{
    internal class AuthorizationService : IAuthorizationService
    {
        private List<string> _lastTokens = new List<string>();
        private IDataBaseService _dataBaseService;
        public AuthorizationService(IDataBaseService dataBaseService) 
        {
            _dataBaseService = dataBaseService;
        }

        public bool LoginToken(string token)
        {

            if (_lastTokens.Count > 0)
            {
                var result = _lastTokens.FirstOrDefault(item => item == token);
                if (result == token)
                {
                    return true;
                }
            } 

            var tokens = _dataBaseService.DataBaseAccess.TokenTable.GetAll();

            if (tokens != null)
            {
                var result = tokens.FirstOrDefault(t => t.Token == token);
                if (result != null && result.Token == token)
                {
                    _lastTokens.Add(token);
                    return true;
                }
            }

            throw new Exception("Невірний токен авторизації");
        } 
        public void AddToken(string token)
        {
            _lastTokens.Add(token);
        }
    }
}

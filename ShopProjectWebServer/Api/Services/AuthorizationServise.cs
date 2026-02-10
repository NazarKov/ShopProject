using ShopProjectWebServer.DataBase;

namespace ShopProjectWebServer.Api.Services
{
    public class AuthorizationServise
    {
        private static List<string> _lastTokens = new List<string>();
        private DataBaseMainController _controller;
        public AuthorizationServise(DataBaseMainController controller) 
        {
            _controller = controller;
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

            var tokens =  _controller.DataBaseAccess.TokenTable.GetAll();

            if (tokens != null)
            {
                var result = tokens.Where(t => t.Token == token).FirstOrDefault();
                if (result != null && result.Token == token)
                {
                    _lastTokens.Add(token);
                    return true;
                }
            }

            throw new Exception("Невірний токен авторизації");
        } 
        public static void AddToken(string token)
        {
            _lastTokens.Add(token);
        }
    }
}

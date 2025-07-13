using ShopProjectWebServer.DataBase;

namespace ShopProjectWebServer.Api.Helpers
{
    public class AuthorizationApi
    {
        private static List<string> _lastTokens = new List<string>();

        public AuthorizationApi() { }

        public static bool LoginToken(string token)
        {

            if (_lastTokens.Count > 0)
            {
                var result = _lastTokens.Where(item => item == token);
                if (result.ElementAt(0) == token)
                {
                    return true;
                }
            }


            var tokens = DataBaseMainController.DataBaseAccess.TokenTable.GetAll();

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
    }
}

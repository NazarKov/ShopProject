namespace ShopProjectWebServer.DataBase.Helpers
{
    public class ConnectionString
    {
        public string Server { get; set; } = string.Empty;
        public string DataBaseName { get; set; } = string.Empty;

        public string UserName { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public bool TrustServerCertificate { get; set; } = true; //true тест fasle потрібно створити ssl сертифікат і підключити його
 
        public TypeConnectDataBase TypeDataBase { get; set; }

        public override string ToString()
        {
            Server = "localhost";
            switch (TypeDataBase) 
            {
                case TypeConnectDataBase.None:
                    {
                        return string.Empty; 
                    }
                case TypeConnectDataBase.SQLEXPRESS:
                    { 
                        return $"Server = {Server}.\\SQLEXPRESS; Database = {DataBaseName}; User Id = {UserName}; Password = {Password}; TrustServerCertificate = {TrustServerCertificate};";
                    }
                case TypeConnectDataBase.DEVELEPER:
                    {
                        return $"Server = {Server}; Database = {DataBaseName}; User Id = {UserName}; Password = {Password}; TrustServerCertificate = {TrustServerCertificate};"; 
                    }
            }
            return string.Empty;
        }
    }
}

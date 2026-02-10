namespace ShopProjectWebServer.DataBase.Helpers
{
    public class ConnectionString
    {
        public string Server { get; set; } = string.Empty;
        public string DataBaseName { get; set; } = string.Empty;

        public string UserName { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public bool TrustServerCertificate { get; set; } = true; //true тест fasle потрібно створити ssl сертифікат і підключити його
 
        public TypeConnectDataBase TypeConnectDB { get; set; }

        public static ConnectionString CreateConnectionString(string login ,string password, TypeConnectDataBase typeConnect, string dataBaseName, bool trustServerCertificate = true)
        {
            return new ConnectionString()
            {
                UserName = login,
                Password = password,
                DataBaseName = dataBaseName,
                Server = Environment.MachineName,
                TrustServerCertificate = trustServerCertificate,
                TypeConnectDB = typeConnect,
            };
        }


        public override string ToString()
        {
            Server = "localhost";
            switch (TypeConnectDB) 
            {
                case TypeConnectDataBase.None:
                    {
                        return string.Empty; 
                    }
                case TypeConnectDataBase.SQLEXPRESS:
                    { 
                        return $"Server = {Server}\\SQLEXPRESS; Database = {DataBaseName}; User Id = {UserName}; Password = {Password}; TrustServerCertificate = {TrustServerCertificate};";
                    }
                case TypeConnectDataBase.DEVELEPER:
                    {
                        if (UserName == string.Empty && Password == string.Empty) 
                        {
                            return $"Server = {Server}; Database = {DataBaseName};TrustServerCertificate = {TrustServerCertificate};";
                        }
                        else
                        {
                            return $"Server = {Server}; Database = {DataBaseName}; User Id = {UserName}; Password = {Password}; TrustServerCertificate = {TrustServerCertificate};"; 
                        }
                    }
            }
            return string.Empty;
        }
    }
}

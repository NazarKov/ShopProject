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
            switch (TypeConnectDB) 
            {
                case TypeConnectDataBase.None:
                    {
                        return string.Empty; 
                    }
                case TypeConnectDataBase.SQLEXPRESS:
                    { 
                        if (UserName == string.Empty && Password == string.Empty)
                        {
                            if(DataBaseName != string.Empty)
                            {
                                return $"Data Source={Server}\\SQLEXPRESS;Database = {DataBaseName};Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate={TrustServerCertificate};Application Intent=ReadWrite;Multi Subnet Failover=False";
                            }
                            else
                            {
                                return $"Data Source={Server}\\SQLEXPRESS;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate={TrustServerCertificate};Application Intent=ReadWrite;Multi Subnet Failover=False";
                            }
                        }
                        else
                        {
                            return $"Data Source={Server}\\SQLEXPRESS;Database = {DataBaseName}; User Id = {UserName}; Password = {Password};Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate={TrustServerCertificate};Application Intent=ReadWrite;Multi Subnet Failover=False";

                        }
                    }
                case TypeConnectDataBase.DEVELEPER:
                    {
                        if (UserName == string.Empty && Password == string.Empty) 
                        {
                            if (DataBaseName != string.Empty) 
                            {
                                return $"Data Source={Server};Database = {DataBaseName};Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate={TrustServerCertificate};Application Intent=ReadWrite;Multi Subnet Failover=False;";
                            }
                            else
                            {
                                return $"Data Source={Server};Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate={TrustServerCertificate};Application Intent=ReadWrite;Multi Subnet Failover=False;";
                            } 
                        }
                        else
                        { 
                            return $"Data Source={Server};Database = {DataBaseName};User Id = {UserName}; Password = {Password};Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate={TrustServerCertificate};Application Intent=ReadWrite;Multi Subnet Failover=False;";
                   
                        }
                    }
            }
            return string.Empty;
        }
    }
}

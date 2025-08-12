namespace ShopProjectWebServer.DataBase.Helpers
{
    public class ConnectionString
    {
        public string DataSource { get; set; } = string.Empty;
        public bool IntegratedSecurity { get; set; } = false;
        public string InitialCatalog { get; set; } = string.Empty;
        public TypeConnectDataBase TypeDataBase { get; set; }

        public override string ToString()
        {
            switch(TypeDataBase) 
            {
                case TypeConnectDataBase.None:
                    {
                        return string.Empty; 
                    }
                case TypeConnectDataBase.SQLEXPRESS: 
                    {
                        return "Data Source=" + DataSource + ".\\SQLEXPRESS;Initial Catalog=" + InitialCatalog + ";Integrated Security=" + IntegratedSecurity + ";"; 
                    }
                case TypeConnectDataBase.DEVELEPER:
                    {
                        return "Data Source=" + DataSource + ";Initial Catalog=" + InitialCatalog + ";Integrated Security=" + IntegratedSecurity + ";"; 
                    }
            }
            return string.Empty;
        }
    }
}

namespace ShopProjectWebServer.DataBase.HelperModel
{
    public class ConnectionString
    {
        public string DataSource { get; set; }
        public bool IntegratedSecurity { get; set; }
        public string InitialCatalog { get; set; }
        public TypeConnectDataBase TypeDataBase { get; set; }

        public override string ToString()
        {
            switch(TypeDataBase) 
            {
                case TypeConnectDataBase.None:
                    {
                        return string.Empty;
                        break;
                    }
                case TypeConnectDataBase.SQLEXPRESS: 
                    {
                        return "Data Source=" + DataSource + ".\\SQLEXPRESS;Initial Catalog=" + InitialCatalog + ";Integrated Security=" + IntegratedSecurity + ";";
                        break;
                    }
                case TypeConnectDataBase.DEVELEPER:
                    {
                        return "Data Source=" + DataSource + ";Initial Catalog=" + InitialCatalog + ";Integrated Security=" + IntegratedSecurity + ";";
                        break;
                    }
            }
            return string.Empty;
        }
    }
}

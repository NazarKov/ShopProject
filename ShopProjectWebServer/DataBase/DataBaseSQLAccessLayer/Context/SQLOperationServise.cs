using Microsoft.Data.SqlClient;
using ShopProjectWebServer.DataBase.Interface.DataBaseInterface;

namespace ShopProjectWebServer.DataBase.DataBaseSQLAccessLayer.Context
{
    public class SqlOperationServise :ISqlOperationServise
    {
        public async Task<bool> Сonnection(string connectionString)
        {
            using (var connect = new SqlConnection(connectionString))
            {
                await connect.OpenAsync();
            }
            return true;
        }
    }
}

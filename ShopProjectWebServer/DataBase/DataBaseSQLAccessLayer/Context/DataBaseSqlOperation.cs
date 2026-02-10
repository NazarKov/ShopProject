using Microsoft.Data.SqlClient;
using ShopProjectWebServer.DataBase.Interface.DataBaseInterface;

namespace ShopProjectWebServer.DataBase.DataBaseSQLAccessLayer.Context
{
    public class DataBaseSqlOperation :IDataBaseOperation
    {
        public async Task<bool> Сonnection(string connectionString)
        {
            try
            {
                using (var connect = new SqlConnection(connectionString))
                {
                    await connect.OpenAsync();
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}

using Microsoft.Data.SqlClient;
using ShopProjectWebServer.DataBase.Helpers;
using ShopProjectWebServer.DataBase.Helpers.Enum;
using ShopProjectWebServer.DataBase.Interface.DataBaseInterface;
using System.ServiceProcess;

namespace ShopProjectWebServer.DataBase.DataBaseSQLAccessLayer.Context
{
    public class SqlOperationServise :IDataBaseOperationServise
    {
        public async Task<bool> Сonnection(string connectionString)
        {
            try
            {
                using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(3));
                using (var connect = new SqlConnection(connectionString))
                {
                    await connect.OpenAsync(cts.Token);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        } 
    }

}

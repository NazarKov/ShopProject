using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ShopProjectDataBase.Context;
using ShopProjectWebServer.DataBase.Interface.DataBaseInterface;
using System.Data;
using System.Text.RegularExpressions;

namespace ShopProjectWebServer.DataBase.DataBaseSQLAccessLayer.Context
{
    public class DatabaseSQLSecurityService : IDataBaseSecurityService
    { 
        private string _connectionString; 
        public DatabaseSQLSecurityService(string connectionString)
        {
            _connectionString = connectionString; 
        }

        private bool IsValidName(string value) => Regex.IsMatch(value, @"^[a-zA-Z0-9_]+$");

        public async Task<bool> CreateLogin(string login, string password, string nameDataBase)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                await connection.OpenAsync();

                string sql = @"
                        DECLARE @sql NVARCHAR(MAX);
                        
                        IF NOT EXISTS (SELECT 1 FROM sys.server_principals WHERE name = @login)
                        BEGIN
                            SET @sql =
                                N'CREATE LOGIN [' + REPLACE(@login, ']', ']]') +
                                N'] WITH PASSWORD = ''' + REPLACE(@password, '''', '''''') + N'''';
                        
                            EXEC (@sql);
                        END
                        
                        SET @sql =
                            N'ALTER LOGIN [' + REPLACE(@login, ']', ']]') +
                            N'] WITH DEFAULT_DATABASE = [' + REPLACE(@db, ']', ']]') + N']';
                        
                        EXEC (@sql);
                        ";

                using var cmdMaster = new SqlCommand(sql, connection);
                cmdMaster.Parameters.Add("@login", SqlDbType.NVarChar, 128).Value = login;
                cmdMaster.Parameters.Add("@password", SqlDbType.NVarChar, 128).Value = password;
                cmdMaster.Parameters.Add("@db", SqlDbType.NVarChar, 128).Value = nameDataBase;

                await cmdMaster.ExecuteNonQueryAsync(); 

                connection.ChangeDatabase(nameDataBase);

                string sqlDb = $@"
                        IF NOT EXISTS (SELECT 1 FROM sys.database_principals WHERE name = '{login}')
                        BEGIN
                            CREATE USER [{login}] FOR LOGIN [{login}];
                        END
                        
                        ALTER ROLE db_datareader ADD MEMBER [{login}];
                        ALTER ROLE db_datawriter ADD MEMBER [{login}];
                        ";

                using var cmdDb = new SqlCommand(sqlDb, connection);
                 await cmdDb.ExecuteNonQueryAsync(); 
                return true;
            }
            catch(SqlException ex)
            {
                foreach (SqlError error in ex.Errors)
                {
                    Console.WriteLine(error.Message);
                }
                return false;
            }
        }
    }
}

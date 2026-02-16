using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ShopProjectDataBase.Context;
using ShopProjectDataBase.Entities;
using ShopProjectWebServer.DataBase.Helpers;
using ShopProjectWebServer.DataBase.Interface.DataBaseInterface;
using System.Diagnostics;

namespace ShopProjectWebServer.DataBase.DataBaseSQLAccessLayer.Context
{
    public class DatabaseSqlInitializer : IDataBaseInitializer
    {
        private ContextDataBase _contextDataBase;
        public DatabaseSqlInitializer(ContextDataBase contextDataBase)
        {
            _contextDataBase = contextDataBase;
        }
        public DatabaseSqlInitializer() 
        {
            _contextDataBase = new ContextDataBase();
        }

        private async Task<bool> Create(string connectionString , string login , string password)
        {
            try
            {
                var optionsBuilder = new DbContextOptionsBuilder<ContextDataBase>();
                optionsBuilder.UseSqlServer(connectionString); 
                _contextDataBase = new ContextDataBase(optionsBuilder.Options);

                await _contextDataBase.Database.MigrateAsync();
                _contextDataBase.Initial(new UserEntity() { 
                    Login = login,
                    Password = password,  
                });
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> CreateDataBase(ISqlOperationServise dataBaseOperation, ISqlSecurityService dataBaseSecurityService,
          string login, string password, string nameDataBase, ConnectionString connectionString)
        {
            connectionString.DataBaseName = nameDataBase;
            if (await Create(connectionString.ToString(), login , password))
            {
                if (await dataBaseSecurityService.CreateLogin(login, password, nameDataBase))
                {
                    connectionString.UserName = login;
                    connectionString.Password = password;
                    return await Connection();
                }
            }
            return false;
        }

        public async Task<bool> IsCreate()
        {
            try
            {
                using (ContextDataBase context = _contextDataBase)
                {

                    if (!await CheckDbFastAsync())
                    {
                        return false;
                    }

                    context.Database.Migrate();

                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> CheckDbFastAsync()
        {
            try
            {
                using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(3));
                using var conn = new SqlConnection(_contextDataBase.Database.GetConnectionString());


                await conn.OpenAsync(cts.Token);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void Delete()
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Connection()
        {
            return  await _contextDataBase.Database.CanConnectAsync();
        }
    }
}

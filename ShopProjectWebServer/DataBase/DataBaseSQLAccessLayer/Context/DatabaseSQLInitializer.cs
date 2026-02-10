using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ShopProjectDataBase.Context;
using ShopProjectWebServer.DataBase.Interface.DataBaseInterface;
using System.Diagnostics;

namespace ShopProjectWebServer.DataBase.DataBaseSQLAccessLayer.Context
{
    public class DatabaseSqlInitializer : IDatabaseInitializer
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

        public async Task<bool> Create(string connectionString)
        {
            try
            {
                var optionsBuilder = new DbContextOptionsBuilder<ContextDataBase>();
                optionsBuilder.UseSqlServer(connectionString); 
                _contextDataBase = new ContextDataBase(optionsBuilder.Options);

                await _contextDataBase.Database.MigrateAsync();
                _contextDataBase.Initial();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> IsCreate()
        {
            try
            {
                
                using (ContextDataBase context = _contextDataBase)
                {

                    if (!await CheckDbFastAsync())
                    {
                        context.Database.Migrate();
                        context.Initial();
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

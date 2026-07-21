using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using ShopProjectDataBase.Context;
using ShopProjectWebServer.Models.Domain.Setting;
using ShopProjectWebServer.Service.Integration.Directory;
using ShopProjectWebServer.Service.Integration.File.BaseFile;
using ShopProjectWebServer.Service.Modules.Setting;
using ShopProjectWebServer.Service.Modules.Setting.Interface;

namespace ShopProjectWebServer.DataBase.DataBaseSQLAccessLayer.Context
{
    public class DataBaseSQLContextFactory : IDesignTimeDbContextFactory<ContextDataBase>
    {
        private ISettingService _settingService;  
        public DataBaseSQLContextFactory()
        {
            _settingService = new SettingService(new DirectoryService(), new FileService());
        }

        public ContextDataBase CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ContextDataBase>();

            ConfigurationBuilder builder = new ConfigurationBuilder(); 
            string connectionString = _settingService.GetSetting<SettingDataBaseConnection>().ConnectionString.ToString();


            optionsBuilder.UseSqlServer(connectionString, opts => opts.CommandTimeout((int)TimeSpan.FromMinutes(10).TotalSeconds));
            return new ContextDataBase(optionsBuilder.Options);
        }
    }
}

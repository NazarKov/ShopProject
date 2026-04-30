using ShopProjectWebServer.DataBase.Interface;
using ShopProjectWebServer.Services.Infrastructure.ContolWebServer.Interface;
using System.Threading.Tasks;

namespace ShopProjectWebServer.Services.Infrastructure.ContolWebServer
{
    public class ControlWebServerService : IControlWebServerService
    {
        private IDataBaseService _dataBaseService;

        public ControlWebServerService(IDataBaseService dataBaseService) 
        {
            _dataBaseService = dataBaseService;
        }

        public async Task<bool> CheckDataBaseHealth()
        {
            return await _dataBaseService.IsConnect();
        }

    }
}

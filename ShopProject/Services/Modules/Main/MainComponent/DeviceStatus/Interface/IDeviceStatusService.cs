using System.Threading.Tasks;
using DeviseStatusModel = ShopProject.Model.Domain.Monitoring.DeviceStatus;

namespace ShopProject.Services.Modules.Main.MainComponent.DeviceStatus.Interface
{
    internal interface IDeviceStatusService
    {
        public Task Check(); 
        public DeviseStatusModel GetStatus();

    }
}

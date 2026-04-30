using ShopProject.Infrastructure.CompositionRoot;
using ShopProject.Services.Infrastructure.Logging;
using ShopProject.Services.Infrastructure.Logging.Interface;
using ShopProject.Services.Infrastructure.Monitoring.WebServerStatus;
using ShopProject.Services.Infrastructure.Monitoring.WebServerStatus.Interface;
using ShopProject.Services.Integration.Directory;
using ShopProject.Services.Integration.Directory.Interface;
using ShopProject.Services.Integration.File.BaseFile;
using ShopProject.Services.Integration.File.BaseFile.Interface;
using ShopProject.Services.Integration.Network.WebServerApi;
using ShopProject.Services.Integration.Network.WebServerApi.Interface;
using ShopProject.Services.Integration.Printing;
using ShopProject.Services.Integration.Printing.Interface;
using ShopProject.Services.Integration.PrintingService;
using ShopProject.Services.Integration.Windows.WindowsService;
using ShopProject.Services.Integration.Windows.WindowsService.Interface;
using ShopProject.Services.Modules.Main;
using ShopProject.Services.Modules.Main.Interface;
using ShopProject.Services.Modules.Model.WorkingShift;
using ShopProject.Services.Modules.Model.WorkingShift.Interface;
using ShopProject.Services.Modules.ModelService.ObjectOwner;
using ShopProject.Services.Modules.ModelService.ObjectOwner.Interface;
using ShopProject.Services.Modules.ModelService.OperationRecorder;
using ShopProject.Services.Modules.ModelService.OperationRecorder.Interface;
using ShopProject.Services.Modules.ModelService.Product;
using ShopProject.Services.Modules.ModelService.Product.Interface;
using ShopProject.Services.Modules.ModelService.ProductCodeUKTZED;
using ShopProject.Services.Modules.ModelService.ProductCodeUKTZED.Interface;
using ShopProject.Services.Modules.ModelService.ProductUnit;
using ShopProject.Services.Modules.ModelService.ProductUnit.Interface;
using ShopProject.Services.Modules.ModelService.User;
using ShopProject.Services.Modules.ModelService.User.Interface;
using ShopProject.Services.Modules.NetworkUrlScanner;
using ShopProject.Services.Modules.NetworkUrlScanner.Interface;
using ShopProject.Services.Modules.Resourse;
using ShopProject.Services.Modules.Resourse.Interface;
using ShopProject.Services.Modules.Session;
using ShopProject.Services.Modules.Session.Interface;
using ShopProject.Services.Modules.Setting;
using ShopProject.Services.Modules.Setting.Interface;
using ShopProject.ViewModel.AdminPage.Dashboard;
using ShopProject.ViewModel.AdminPage.Storage.Product;
using ShopProject.ViewModel.AdminPage.Storage.ProductUnit;
using ShopProject.ViewModel.Authorization;
using ShopProject.ViewModel.Common.ConnectionLost;
using ShopProject.ViewModel.Common.Home;
using ShopProject.ViewModel.Common.Setting;
using ShopProject.ViewModel.Common.Start;
using ShopProject.ViewModel.HomePage.HomePageComponent;
using ShopProject.ViewModel.Integration.DeviceStatus;
using ShopProject.ViewModel.Integration.Printing;
using ShopProject.ViewModel.Integration.Windows.Service;
using ShopProject.ViewModel.SettingPage;
using ShopProject.ViewModel.StoragePage;
using ShopProject.ViewModel.StoragePage.ProductCodeUKTZEDPage;
using ShopProject.ViewModel.StoragePage.ProductUnitPage;
using ShopProject.ViewModel.UserPage.SaleMenu;

namespace ShopProject.Extensions.FactoryExtensions
{
    public static class ServiceProviderExtensions
    {
        public static void AddApplicationViewModel(this ServiceProvider factory)
        {
            factory.RegisterTransient<DeviceStatusViewModel,DeviceStatusViewModel>();
            factory.RegisterTransient<MainViewModel,MainViewModel>();
            factory.RegisterTransient<StartViewModel, StartViewModel>();
            factory.RegisterTransient<RegisterWindowsServiceViewModel, RegisterWindowsServiceViewModel>();

            factory.RegisterTransient<NotificationViewModel, NotificationViewModel>(); 
            factory.RegisterScoped<ServerSelectionViewModel, ServerSelectionViewModel>();
            factory.RegisterTransient<AuthorizationViewModel, AuthorizationViewModel>();

            factory.RegisterTransient<CreateProductViewModel, CreateProductViewModel>();
            factory.RegisterScoped<ProductsDataViewModel, ProductsDataViewModel>();
            factory.RegisterScoped<UpdateProductViewModel, UpdateProductViewModel>();
            factory.RegisterScoped<UpdateProductRangeViewModel, UpdateProductRangeViewModel>(); 
            
            factory.RegisterScoped<ProductUnitsDataViewModel, ProductUnitsDataViewModel>();
            factory.RegisterTransient<CreateProductUnitViewModel, CreateProductUnitViewModel>();
            factory.RegisterTransient<UpdateProductUnitViewModel, UpdateProductUnitViewModel>();

            factory.RegisterScoped<ProductCodeUKTZEDDataViewModel, ProductCodeUKTZEDDataViewModel>();
            factory.RegisterTransient<CreateProductCodeUKTZEDViewModel, CreateProductCodeUKTZEDViewModel>();
            factory.RegisterTransient<UpdateProductCodeUKTZEDViewModel, UpdateProductCodeUKTZEDViewModel>();

            factory.RegisterScoped<StorageViewModel, StorageViewModel>();

            factory.RegisterScoped<SettingProfileViewModel, SettingProfileViewModel>();
            factory.RegisterScoped<SettingStorageViewModel, SettingStorageViewModel>();
            factory.RegisterScoped<SettingPrintingStickerViewModel, SettingPrintingStickerViewModel>();
            factory.RegisterScoped<SettingPrintingCheckViewModel, SettingPrintingCheckViewModel>();
            factory.RegisterScoped<SettingOperationRecorderViewModel, SettingOperationRecorderViewModel>();

            factory.RegisterScoped<SettingViewModel, SettingViewModel>();

            factory.RegisterScoped<OperationRecorderViewModel,OperationRecorderViewModel>();
            factory.RegisterScoped<WorkShiftMenuViewModel, WorkShiftMenuViewModel>();
            factory.RegisterTransient<SaleProductMenuViewModel, SaleProductMenuViewModel>();

            factory.RegisterTransient<ConnectionLostViewModel, ConnectionLostViewModel>();
            factory.RegisterTransient<StickerPrintViewModel, StickerPrintViewModel>();
            factory.RegisterTransient<DashBoardViewModel, DashBoardViewModel>();
        }

        public static void AddApplicationService(this ServiceProvider factory) 
        { 
            factory.RegisterScoped<IFileService, FileService>();
            factory.RegisterScoped<ISettingService, SettingService>(); 
            factory.RegisterScoped<ISessionService , SessionService>();
            factory.RegisterTransient<ISettingWebServerService, SettingWebServerService>();
            factory.RegisterSingleton<IMainWebServerService,WebServerService>();
            
            factory.RegisterScoped<IMainAppServise, MainAppServise>();

            factory.RegisterTransient<IResourseService , ResourseService>();
            factory.RegisterTransient<IDirectoryService, DirectoryService>();
            factory.RegisterTransient<IUserServise, UserServise>();
            factory.RegisterTransient<INetworkUrlManagerService, NetworkUrlManagerService>();

            factory.RegisterScoped<IProductServiсe,ProductServiсe>();
            factory.RegisterScoped<IProductUnitServiсe, ProductUnitServiсe>();
            factory.RegisterScoped<IProductCodeUKTZEDServiсe, ProductCodeUKTZEDServiсe>();

            factory.RegisterTransient<IPrintingStikerService, PrintingSticker>();
            factory.RegisterTransient<IPrintingFiscalCheckService,PrintingFiscalCheckServise>();

            factory.RegisterScoped<IObjectOwnerService,ObjectOwnerService>();
            factory.RegisterScoped<IOperationRecorderServise, OperationRecorderServise>();
            factory.RegisterScoped<IWorkingShiftService,WorkingShiftService>();
            factory.RegisterScoped<ISaleMenuService,SaleMenuService>();
            factory.RegisterSingleton<ILoggerService, FileLoggerService>();
            factory.RegisterScoped<IWindowsServiceManager, WindowsServiceManager>();
            factory.RegisterScoped<IWebServerStatusService, WebServerStatusService>();
        }
    }
}

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
using ShopProject.Services.Modules.Control;
using ShopProject.Services.Modules.Control.Interface;
using ShopProject.Services.Modules.Domain.OperationRecorder;
using ShopProject.Services.Modules.Domain.OperationRecorder.Interface;
using ShopProject.Services.Modules.Domain.PoinOfSale.SaleMenu;
using ShopProject.Services.Modules.Domain.PoinOfSale.SaleMenu.Interface;
using ShopProject.Services.Modules.Domain.PoinOfSale.TaxObject;
using ShopProject.Services.Modules.Domain.PoinOfSale.TaxObject.Interface;
using ShopProject.Services.Modules.Domain.Product;
using ShopProject.Services.Modules.Domain.Product.Interface;
using ShopProject.Services.Modules.Domain.ProductCodeUKTZED;
using ShopProject.Services.Modules.Domain.ProductCodeUKTZED.Interface;
using ShopProject.Services.Modules.Domain.ProductUnit;
using ShopProject.Services.Modules.Domain.ProductUnit.Interface;
using ShopProject.Services.Modules.Domain.User;
using ShopProject.Services.Modules.Domain.User.Interface;
using ShopProject.Services.Modules.Domain.UserRole;
using ShopProject.Services.Modules.Domain.UserRole.Interface;
using ShopProject.Services.Modules.Main;
using ShopProject.Services.Modules.Main.Interface;
using ShopProject.Services.Modules.NetworkUrlScanner;
using ShopProject.Services.Modules.NetworkUrlScanner.Interface;
using ShopProject.Services.Modules.Resourse;
using ShopProject.Services.Modules.Resourse.Interface;
using ShopProject.Services.Modules.Session;
using ShopProject.Services.Modules.Session.Interface;
using ShopProject.Services.Modules.Setting;
using ShopProject.Services.Modules.Setting.Interface;
using ShopProject.ViewModel.AdminPage.Dashboard;
using ShopProject.ViewModel.AdminPage.PointOfSale;
using ShopProject.ViewModel.AdminPage.PointOfSale.OperationRecorder;
using ShopProject.ViewModel.AdminPage.PointOfSale.TaxObject;
using ShopProject.ViewModel.AdminPage.Storage.Product;
using ShopProject.ViewModel.AdminPage.Storage.ProductUnit;
using ShopProject.ViewModel.AdminPage.UserManagement;
using ShopProject.ViewModel.AdminPage.UserManagement.User;
using ShopProject.ViewModel.Authorization;
using ShopProject.ViewModel.Common.ConnectionLost;
using ShopProject.ViewModel.Common.Main;
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
using ShopProject.ViewModel.UserPage.PointOfSale;
using ShopProject.ViewModel.UserPage.PointOfSale.SaleMenu;

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

            factory.RegisterScoped<UsersDataViewModel, UsersDataViewModel>();
            factory.RegisterTransient<CreateUserViewModel, CreateUserViewModel>();
            factory.RegisterTransient<UpdateUserViewModel, UpdateUserViewModel>();

            factory.RegisterScoped<UserManagementViewModel, UserManagementViewModel>();

            factory.RegisterScoped<SettingProfileViewModel, SettingProfileViewModel>();
            factory.RegisterScoped<SettingStorageViewModel, SettingStorageViewModel>();
            factory.RegisterScoped<SettingPrintingStickerViewModel, SettingPrintingStickerViewModel>();
            factory.RegisterScoped<SettingPrintingCheckViewModel, SettingPrintingCheckViewModel>();
            factory.RegisterScoped<SettingOperationRecorderViewModel, SettingOperationRecorderViewModel>();

            factory.RegisterScoped<SettingViewModel, SettingViewModel>();

            factory.RegisterScoped<PointOfSaleViewModel, PointOfSaleViewModel>();
            factory.RegisterScoped<TaxObjectsDataViewModel, TaxObjectsDataViewModel>();

            factory.RegisterTransient<CreateTaxObjectViewModel, CreateTaxObjectViewModel>();
            factory.RegisterTransient<CreateTaxObjectFromKeyViewModel, CreateTaxObjectFromKeyViewModel>();
            factory.RegisterTransient<BindingOperationRecorderToTaxObjectViewModel, BindingOperationRecorderToTaxObjectViewModel>();
            factory.RegisterTransient<BindingUserToTaxObjectViewModel, BindingUserToTaxObjectViewModel>();

            factory.RegisterScoped<OperationRecordersDataViewModel, OperationRecordersDataViewModel>();
            factory.RegisterTransient<CreateOperationRecorederViewModel, CreateOperationRecorederViewModel>();
            factory.RegisterTransient<CreateOperationRecorderFromKeyViewModel, CreateOperationRecorderFromKeyViewModel>();
            factory.RegisterTransient<UpdateTaxObjectViewModel, UpdateTaxObjectViewModel>();

            factory.RegisterScoped<AssignedPointsOfSaleViewModel, AssignedPointsOfSaleViewModel>();
            factory.RegisterScoped<WorkShiftMenuViewModel, WorkShiftMenuViewModel>();
             
            factory.RegisterTransient<SaleMenuViewModel, SaleMenuViewModel>();

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
            factory.RegisterTransient<IUserService, UserService>();
            factory.RegisterTransient<INetworkUrlManagerService, NetworkUrlManagerService>();

            factory.RegisterScoped<IProductServiсe,ProductServiсe>();
            factory.RegisterScoped<IProductUnitServiсe, ProductUnitServiсe>();
            factory.RegisterScoped<IProductCodeUKTZEDServiсe, ProductCodeUKTZEDServiсe>();

            factory.RegisterTransient<IPrintingStikerService, PrintingSticker>();
            factory.RegisterTransient<IPrintingFiscalCheckService,PrintingFiscalCheckServise>();

            factory.RegisterScoped<ITaxObjectService,TaxObjectService>();
            factory.RegisterScoped<IOperationRecorderService, OperationRecorderService>();
            factory.RegisterScoped<IWorkingShiftService,WorkingShiftService>();
            factory.RegisterScoped<ISaleMenuService,SaleMenuService>();
            factory.RegisterSingleton<ILoggerService, FileLoggerService>();
            factory.RegisterScoped<IWindowsServiceManager, WindowsServiceManager>();
            factory.RegisterScoped<IWebServerStatusService, WebServerStatusService>();
            factory.RegisterScoped<IUserRoleService, UserRoleService>();
            factory.RegisterScoped<IWorkingShfitOperationService,WorkingShiftOperationService>();
            factory.RegisterScoped<IMessageBoxControlService, MessageBoxControlService>();
        }
    }
}

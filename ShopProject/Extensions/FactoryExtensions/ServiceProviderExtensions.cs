using ShopProject.Core.Mvvm.CompositionRoot;
using ShopProject.Services.Integration.File.Directory;
using ShopProject.Services.Integration.File.Directory.Interface;
using ShopProject.Services.Integration.Network.ShopProjectWebServerApi;
using ShopProject.Services.Integration.Network.ShopProjectWebServerApi.Interface;
using ShopProject.Services.Integration.Printing;
using ShopProject.Services.Integration.Printing.Interface;
using ShopProject.Services.Integration.PrintingService;
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
using ShopProject.View.Common.Setting;
using ShopProject.ViewModel.AdminPage.Storage.Product;
using ShopProject.ViewModel.AdminPage.Storage.ProductUnit;
using ShopProject.ViewModel.Authorization;
using ShopProject.ViewModel.Common.Home;
using ShopProject.ViewModel.Common.Setting;
using ShopProject.ViewModel.Common.Start;
using ShopProject.ViewModel.HomePage.HomePageComponent;
using ShopProject.ViewModel.SalePage;
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
            factory.RegisterTransient<MainViewModel,MainViewModel>();
            factory.RegisterTransient<StartViewModel, StartViewModel>();

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
            factory.RegisterScoped<SaleProductMenuViewModel, SaleProductMenuViewModel>();

        }

        public static void AddApplicationService(this ServiceProvider factory) 
        { 
            factory.RegisterScoped<IFileServise, FileServise>();
            factory.RegisterScoped<ISettingService, SettingService>(); 
            factory.RegisterScoped<ISessionService , SessionService>();
            factory.RegisterTransient<ISettingWebServerService, SettingWebServerService>();
            factory.RegisterSingleton<IMainWebServerService,MainWebServerService>();
            
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
        }
    }
}

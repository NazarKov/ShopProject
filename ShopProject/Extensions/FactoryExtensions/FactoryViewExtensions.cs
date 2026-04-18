using ShopProject.Infrastructure.CompositionRoot;
using ShopProject.View.AdminPage.Storage;
using ShopProject.View.AdminPage.Storage.Product;
using ShopProject.View.AdminPage.Storage.ProductCodeUKTZED;
using ShopProject.View.AdminPage.Storage.ProductUnit;
using ShopProject.View.Authorization;
using ShopProject.View.Common.Main;
using ShopProject.View.Common.Setting;
using ShopProject.View.Common.Start;
using ShopProject.View.HomePage.HomePageComponent;
using ShopProject.View.Integration.Printing;
using ShopProject.View.Integration.Windows.Service;
using ShopProject.View.StoragePage;
using ShopProject.View.UserPage.SaleMenu;
using ShopProject.ViewModel.StoragePage;

namespace ShopProject.Extensions.FactoryExtensions
{
    public static class FactoryViewExtensions
    {
        public static void AddApplicationView(this FactoryView factory)
        {
            factory.Register<MainView>(()=>new MainView());


            factory.Register<StartView>(()=>new StartView());
            factory.Register<ServerSelectionView>(()=>new ServerSelectionView());
            factory.Register<RegisterWindowsServiceView>(()=>new RegisterWindowsServiceView());

            factory.Register<NotificationView>(()=>new NotificationView()); 

            factory.Register<CreateProductView>(()=>new CreateProductView());
            factory.Register<UpdateProductView>(()=>new UpdateProductView());
            factory.Register<UpdateProductRangeView>(()=>new UpdateProductRangeView());
            factory.Register<ProductsDataView>(()=>new ProductsDataView());
            
            factory.Register<ProductUnitsDataView>(()=>new ProductUnitsDataView());
            factory.Register<CreateProductUnitView>(()=>new CreateProductUnitView());
            factory.Register<UpdateProductUnitView>(()=>new UpdateProductUnitView());

            factory.Register<ProductCodesUKTZEDDataView>(()=>new ProductCodesUKTZEDDataView());
            factory.Register<CreateProductCodeUKTZEDView>(()=>new CreateProductCodeUKTZEDView());
            factory.Register<UpdateProductCodeUKTZEDView>(() => new UpdateProductCodeUKTZEDView());
            
            factory.Register<StorageView>(()=>new StorageView());

            factory.Register<SettingProfileView>(()=>new SettingProfileView());
            factory.Register<SettingStorageView>(()=>new SettingStorageView());
            factory.Register<SettingPrintingCheckView>(()=>new SettingPrintingCheckView());
            factory.Register<SettingPrintingStickerView>(()=>new SettingPrintingStickerView());
            factory.Register<SettingOperationRecorderView>(()=>new SettingOperationRecorderView());

            factory.Register<SettingView>(()=>new SettingView());
            
            factory.Register<OperationRecorderView>(()=>new OperationRecorderView());
            factory.Register<WorkShiftMenuView>(()=>new WorkShiftMenuView());
            factory.Register<SaleProductMenuView>(()=>new SaleProductMenuView());

            factory.Register<StickerPrintView>(()=>new StickerPrintView());



            factory.Register<AuthorizationView>(() => new AuthorizationView());
        }
    }
}

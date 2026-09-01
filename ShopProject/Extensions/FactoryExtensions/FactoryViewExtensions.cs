using ShopProject.Controls.MessegeBox;
using ShopProject.Infrastructure.CompositionRoot;
using ShopProject.View.AdminPage.Dashboard;
using ShopProject.View.AdminPage.PointOfSale;
using ShopProject.View.AdminPage.PointOfSale.OperationRecorder;
using ShopProject.View.AdminPage.PointOfSale.TaxObject;
using ShopProject.View.AdminPage.Storage;
using ShopProject.View.AdminPage.Storage.Product;
using ShopProject.View.AdminPage.Storage.ProductCodeUKTZED;
using ShopProject.View.AdminPage.Storage.ProductUnit; 
using ShopProject.View.AdminPage.UserManagement;
using ShopProject.View.AdminPage.UserManagement.User;
using ShopProject.View.Authorization;
using ShopProject.View.Common.ConnectionLost;
using ShopProject.View.Common.Main;
using ShopProject.View.Common.Setting;
using ShopProject.View.Common.Start;
using ShopProject.View.HomePage.HomePageComponent;
using ShopProject.View.Integration.DeviceStatus;
using ShopProject.View.Integration.Printing;
using ShopProject.View.Integration.Windows.Service;
using ShopProject.View.UserPage.PointOfSale;
using ShopProject.View.UserPage.PointOfSale.SaleMenu; 

namespace ShopProject.Extensions.FactoryExtensions
{
    public static class FactoryViewExtensions
    {
        public static void AddApplicationView(this FactoryView factory)
        {
            factory.Register<DeviceStatusView>(() => new DeviceStatusView()); 
            factory.Register<MainView>(()=>new MainView());
            factory.Register<DashBoardView>(()=>new DashBoardView());

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


            factory.Register<UsersDataView>(()=>new UsersDataView());
            factory.Register<CreateUserView>(()=>new CreateUserView());
            factory.Register<UpdateUserView>(()=>new UpdateUserView());

            factory.Register<UserManagementView>(()=>new UserManagementView());

            factory.Register<SettingProfileView>(()=>new SettingProfileView());
            factory.Register<SettingStorageView>(()=>new SettingStorageView());
            factory.Register<SettingPrintingCheckView>(()=>new SettingPrintingCheckView());
            factory.Register<SettingPrintingStickerView>(()=>new SettingPrintingStickerView());
            factory.Register<SettingOperationRecorderView>(()=>new SettingOperationRecorderView());

            factory.Register<SettingView>(()=>new SettingView());
            
            factory.Register<PointOfSaleView>(()=>new PointOfSaleView());
            factory.Register<TaxObjectsDataView>(() => new TaxObjectsDataView());
            factory.Register<OperationRecordersDataView>(()=>new OperationRecordersDataView());

            factory.Register<CreateTaxObjectView>(()=>new CreateTaxObjectView());
            factory.Register<CreateTaxObjectFromKeyView>(()=>new CreateTaxObjectFromKeyView());
            factory.Register<UpdateTaxObjectView>(()=>new UpdateTaxObjectView());

            factory.Register<CreateOperationRecorederView>(()=>new CreateOperationRecorederView());
            factory.Register<CreateOperationRecorderFromKeyView>(()=>new CreateOperationRecorderFromKeyView());
            factory.Register<BindingOperationRecorderToTaxObjectView>(()=> new BindingOperationRecorderToTaxObjectView());
            factory.Register<BindingUserToTaxObjectView>(() => new BindingUserToTaxObjectView());

            factory.Register<AssignedPointsOfSaleView>(() => new AssignedPointsOfSaleView());
            factory.Register<WorkShiftMenuView>(()=>new WorkShiftMenuView());
             
            factory.Register<SaleMenuView>(()=>new SaleMenuView());

            factory.Register<StickerPrintView>(()=>new StickerPrintView());


            factory.Register<ConnectionLostView>(()=>new ConnectionLostView());
            factory.Register<AuthorizationView>(() => new AuthorizationView());

            factory.Register<MessegeBoxView>(() => new MessegeBoxView());
        }
    }
}

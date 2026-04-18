using ShopProjectWebServer.DataBase;
using ShopProjectWebServer.DataBase.Interface;
using ShopProjectWebServer.Service.Integration.Directory;
using ShopProjectWebServer.Service.Integration.Directory.Interface;
using ShopProjectWebServer.Service.Integration.File.BaseFile;
using ShopProjectWebServer.Service.Integration.File.BaseFile.Interface;
using ShopProjectWebServer.Service.Modules.Setting;
using ShopProjectWebServer.Service.Modules.Setting.Interface;
using ShopProjectWebServer.Services.Infrastructure.Logging;
using ShopProjectWebServer.Services.Infrastructure.Logging.Interface;
using ShopProjectWebServer.Services.Modules.Authorization;
using ShopProjectWebServer.Services.Modules.Authorization.Interface;
using ShopProjectWebServer.Services.Modules.Domain.Discount;
using ShopProjectWebServer.Services.Modules.Domain.ElectronicSignatureKey;
using ShopProjectWebServer.Services.Modules.Domain.GiftCertificates;
using ShopProjectWebServer.Services.Modules.Domain.MediaAccessControl;
using ShopProjectWebServer.Services.Modules.Domain.ObjectOwner;
using ShopProjectWebServer.Services.Modules.Domain.Operation;
using ShopProjectWebServer.Services.Modules.Domain.OperationRecorder;
using ShopProjectWebServer.Services.Modules.Domain.OperationRecordersAndUser;
using ShopProjectWebServer.Services.Modules.Domain.Order;
using ShopProjectWebServer.Services.Modules.Domain.Product;
using ShopProjectWebServer.Services.Modules.Domain.ProductCodeUKTZED;
using ShopProjectWebServer.Services.Modules.Domain.ProductUnit;
using ShopProjectWebServer.Services.Modules.Domain.User;
using ShopProjectWebServer.Services.Modules.Domain.User.Interface;
using ShopProjectWebServer.Services.Modules.Domain.UserRole;
using ShopProjectWebServer.Services.Modules.Domain.WorkingShift;

namespace ShopProjectWebServer.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddAppApiServices(this IServiceCollection services)
        {
            services.AddScoped<IMediaAccessContolServise, MediaAccessControlService>();
            services.AddScoped<IObjectOwnerServise, ObjectOwnerService>();
            services.AddScoped<IOperationRecordersAndUserServise,OperationRecordersAndUserService>();
            services.AddScoped<IOperationRecorderServise, OperationRecorderService>();
            services.AddScoped<IOperationServise, OperationServise>();
            services.AddScoped<IOrderServise, OrderService>();
            services.AddScoped<IProductServise, ProductService>();
            services.AddScoped<IProductCodeUKTZEDServise, ProductCodeUKTZEDService>(); 
            services.AddScoped<IProductUnitServise, ProductUnitService>(); 
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserRoleServise, UserRoleService>();
            services.AddScoped<IWorkingShiftServise, WorkingShiftService>(); 
            services.AddScoped<IElectronicSignatureKeyServise, ElectronicSignatureKeyService>();
            services.AddScoped<IDiscountServise, DiscountService>();
            services.AddScoped<IGiftCertificatesServise, GiftCertificatesService>();
        }
        public static void AddDataBaseServices(this IServiceCollection services)
        {
            services.AddScoped<IAuthorizationService, AuthorizationService>();
            services.AddScoped<IDataBaseService, DataBaseService>();
            services.AddScoped<DataBaseService>();
            services.AddScoped<AuthorizationService>(); 
        }

        public static void AppAppServices(this IServiceCollection services)
        {
            services.AddTransient<IDirectoryService, DirectoryService>();
            services.AddTransient<IFileService, FileService>();
            services.AddTransient<ISettingService, SettingService>();
            services.AddTransient<ILoggerService, FileLoggerService>();
        }
    }
}

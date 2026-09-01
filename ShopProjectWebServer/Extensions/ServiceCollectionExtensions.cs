using ShopProjectWebServer.Api.DtoModels.Product;
using ShopProjectWebServer.Api.DtoModels.ProductCodeUKTZED;
using ShopProjectWebServer.Api.DtoModels.ProductUnit;
using ShopProjectWebServer.Api.DtoModels.User;
using ShopProjectWebServer.Api.Validation.ProductCodeUKTZEDValidation;
using ShopProjectWebServer.Api.Validation.ProductUnitValidation;
using ShopProjectWebServer.Api.Validation.ProductValidation;
using ShopProjectWebServer.Api.Validation.User;
using ShopProjectWebServer.DataBase;
using ShopProjectWebServer.DataBase.DataBaseSQLAccessLayer.Context;
using ShopProjectWebServer.DataBase.Interface;
using ShopProjectWebServer.Service.Integration.Directory;
using ShopProjectWebServer.Service.Integration.Directory.Interface;
using ShopProjectWebServer.Service.Integration.File.BaseFile;
using ShopProjectWebServer.Service.Integration.File.BaseFile.Interface;
using ShopProjectWebServer.Service.Modules.Setting;
using ShopProjectWebServer.Service.Modules.Setting.Interface;
using ShopProjectWebServer.Services.Infrastructure.ContolWebServer;
using ShopProjectWebServer.Services.Infrastructure.ContolWebServer.Interface;
using ShopProjectWebServer.Services.Infrastructure.Logging;
using ShopProjectWebServer.Services.Infrastructure.Logging.Interface;
using ShopProjectWebServer.Services.Modules.Authorization;
using ShopProjectWebServer.Services.Modules.Authorization.Interface;
using ShopProjectWebServer.Services.Modules.BootStrap;
using ShopProjectWebServer.Services.Modules.BootStrap.Interface;
using ShopProjectWebServer.Services.Modules.Domain.Discount;
using ShopProjectWebServer.Services.Modules.Domain.Discount.Interface;
using ShopProjectWebServer.Services.Modules.Domain.ElectronicSignatureKey;
using ShopProjectWebServer.Services.Modules.Domain.MediaAccessControl;
using ShopProjectWebServer.Services.Modules.Domain.Operation;
using ShopProjectWebServer.Services.Modules.Domain.Operation.Interface;
using ShopProjectWebServer.Services.Modules.Domain.OperationRecorder;
using ShopProjectWebServer.Services.Modules.Domain.OperationRecorder.Interface;
using ShopProjectWebServer.Services.Modules.Domain.OperationRecordersAndUser;
using ShopProjectWebServer.Services.Modules.Domain.Order;
using ShopProjectWebServer.Services.Modules.Domain.Order.Interface;
using ShopProjectWebServer.Services.Modules.Domain.Product;
using ShopProjectWebServer.Services.Modules.Domain.Product.Interface;
using ShopProjectWebServer.Services.Modules.Domain.ProductCodeUKTZED;
using ShopProjectWebServer.Services.Modules.Domain.ProductCodeUKTZED.Interface;
using ShopProjectWebServer.Services.Modules.Domain.ProductUnit;
using ShopProjectWebServer.Services.Modules.Domain.ProductUnit.Interface;
using ShopProjectWebServer.Services.Modules.Domain.TaxObject;
using ShopProjectWebServer.Services.Modules.Domain.TaxObject.Interface;
using ShopProjectWebServer.Services.Modules.Domain.User;
using ShopProjectWebServer.Services.Modules.Domain.User.Interface;
using ShopProjectWebServer.Services.Modules.Domain.UserRole;
using ShopProjectWebServer.Services.Modules.Domain.UserRole.Interface;
using ShopProjectWebServer.Services.Modules.Domain.WorkingShift;
using ShopProjectWebServer.Services.Modules.Domain.WorkingShift.Interface;

namespace ShopProjectWebServer.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddAppApiServices(this IServiceCollection services)
        {
            services.AddScoped<IMediaAccessContolServise, MediaAccessControlService>();
            services.AddScoped<ITaxObjectService, TaxObjectService>();
            services.AddScoped<IOperationRecordersAndUserServise,OperationRecordersAndUserService>();
            services.AddScoped<IOperationRecorderService, OperationRecorderService>();
            services.AddScoped<IOperationService, OperationService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IProductCodeUKTZEDService, ProductCodeUKTZEDService>(); 
            services.AddScoped<IProductUnitService, ProductUnitService>(); 
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserRoleServiсe, UserRoleService>();
            services.AddScoped<IWorkingShiftService, WorkingShiftService>(); 
            services.AddScoped<IElectronicSignatureKeyServise, ElectronicSignatureKeyService>();
            services.AddScoped<IDiscountService, DiscountService>();
            services.AddScoped<IBootStrapService, BootStrapService>();
           // services.AddScoped<IGiftCertificatesServise, GiftCertificatesService>();

            services.AddScoped<ShopProjectWebServer.Api.Validation.Interface.IValidator<CreateProductDto>,CreateProductValidator>();
            services.AddScoped<ShopProjectWebServer.Api.Validation.Interface.IValidator<UpdateProductDto>, UpdateProductValidator>();
            services.AddScoped<ShopProjectWebServer.Api.Validation.Interface.IValidator<CreateProductUKTZEDDto>, CreateProductCodeUTKZEDValidator>();
            services.AddScoped<ShopProjectWebServer.Api.Validation.Interface.IValidator<UpdateProductCodeUKTZEDDto>, UpdateProductCoduUKTZEDValidator>();
            services.AddScoped<ShopProjectWebServer.Api.Validation.Interface.IValidator<CreateProductUnitDto>, CreateProductUnitValidator>();
            services.AddScoped<ShopProjectWebServer.Api.Validation.Interface.IValidator<UpdateProductUnitDto>, UpdateProductUnitValidator>();
            services.AddScoped<ShopProjectWebServer.Api.Validation.Interface.IValidator<CreateUserDto>, CreateUserValidator>();
            services.AddScoped<ShopProjectWebServer.Api.Validation.Interface.IValidator<UpdateUserDto>, UpdateUserValidator>();
            services.AddScoped<ShopProjectWebServer.Api.Validation.Interface.IValidator<UserDto>, AuthorizationUserValidator>();
        }
        public static void AddDataBaseServices(this IServiceCollection services)
        {
            services.AddScoped<DataBaseSQLContextFactory>();
            services.AddScoped<IAuthorizationService, AuthorizationService>();
            services.AddScoped<IDataBaseService, DataBaseService>();
            services.AddScoped<DataBaseService>();
            services.AddScoped<AuthorizationService>(); 
        }

        public static void AppAppServices(this IServiceCollection services)
        {
            services.AddTransient<IControlWebServerService, ControlWebServerService>();
            services.AddTransient<IDirectoryService, DirectoryService>();
            services.AddTransient<IFileService, FileService>();
            services.AddTransient<ISettingService, SettingService>();
            services.AddTransient<ILoggerService, FileLoggerService>();
        }
    }
}

using ShopProjectWebServer.Api.Interface.Services;
using ShopProjectWebServer.Api.Services;

namespace ShopProjectWebServer.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IMediaAccessContolServise, MediaAccessControlServise>();
            services.AddScoped<IObjectOwnerServise, ObjectOwnerServise>();
            services.AddScoped<IOperationRecordersAndUserServise,OperationRecordersAndUserServise>();
            services.AddScoped<IOperationRecorderServise, OperationRecorderServise>();
            services.AddScoped<IOperationServise, OperationServise>();
            services.AddScoped<IOrderServise, OrderServise>();
            services.AddScoped<IProductServise, ProductServise>();
            services.AddScoped<IProductCodeUKTZEDServise, ProductCodeUKTZEDServise>();
<<<<<<< HEAD
            services.AddScoped<IProductUnitServise, ProductUnitServise>(); 
            services.AddScoped<IUserServise, UserServise>();
            services.AddScoped<IUserRoleServise, UserRoleServise>();
            services.AddScoped<IWorkingShiftServise, WorkingShiftServise>(); 
            services.AddScoped<IElectronicSignatureKeyServise, ElectronicSignatureKeyServise>();
=======
            services.AddScoped<IProductUnitServise, ProductUnitServise>();
            services.AddScoped<IUserServise, UserServise>();
            services.AddScoped<IWorkingShiftServise, WorkingShiftServise>(); 
>>>>>>> 306da6b87d87ea969d9567c60bf1dbf9a079baf4
        }
    }
}

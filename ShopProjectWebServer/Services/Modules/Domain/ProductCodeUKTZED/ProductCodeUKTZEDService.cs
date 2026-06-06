using ShopProjectDataBase.Entities;
using ShopProjectDataBase.Helper;   
using ShopProjectWebServer.DataBase; 
using ShopProjectWebServer.Services.Common;
using ShopProjectWebServer.Services.Common.Enum;
using ShopProjectWebServer.Services.Modules.Domain.ProductCodeUKTZED.Interface;
using ShopProjectWebServer.Services.Modules.Mapping.ProductCodeUKTZED; 
using ProductCodeUKTZEDModel = ShopProjectWebServer.Models.Domain.ProductCodeUKTZED.ProductCodeUKTZED;

namespace ShopProjectWebServer.Services.Modules.Domain.ProductCodeUKTZED
{
    internal class ProductCodeUKTZEDService : IProductCodeUKTZEDService
    {
        private DataBaseService _controller; 

        public ProductCodeUKTZEDService(DataBaseService controller)
        {
            _controller = controller; 
        }
        public async Task<OperationResult<ProductCodeUKTZEDModel>> AddAsync(ProductCodeUKTZEDModel item)
        {
            try
            {
                var valid = await CreateValidation(item);
                if (valid.IsError)
                {
                    return valid;
                }

                var result = await _controller.DataBaseAccess.ProductCodeUKTZEDTable.AddAsync(item.ToProductCodeUKTZEDEntity());
                return OperationResult<ProductCodeUKTZEDModel>.Success(result.ToProductCodeUKTZED());
            }
            catch (Exception ex)
            {
                return OperationResult<ProductCodeUKTZEDModel>.Fail(ex.Message, ErrorType.Server, ErrorSource.Database);
            }
        }
        private async Task<OperationResult<ProductCodeUKTZEDModel>> CreateValidation(ProductCodeUKTZEDModel model)
        {
            if (await _controller.DataBaseAccess.ProductCodeUKTZEDTable.ExistsByCodeAsync(model.Code))
            {
                return new OperationResult<ProductCodeUKTZEDModel>()
                {
                    Status = ResultStatus.Error,
                    ErrorType = ErrorType.ObjectExists,
                    Source = ErrorSource.Database,
                    ErrorMessage = "Товарний код існує"
                };
            }
            return new OperationResult<ProductCodeUKTZEDModel>()
            {
                Status = ResultStatus.Success,
            };
        }

        public async Task<OperationResult<bool>> UpdateAsync(ProductCodeUKTZEDModel codeUKTZED)
        {
            try
            {
                await _controller.DataBaseAccess.ProductCodeUKTZEDTable.UpdateAsync(codeUKTZED.ToProductCodeUKTZEDEntity());
                return OperationResult<bool>.Success(true);
            }
            catch(Exception ex)
            {
                return OperationResult<bool>.Fail(ex.Message, ErrorType.Server, ErrorSource.Database);
            } 
        }

        public async Task<OperationResult<bool>> UpdateParameterAsync(string parameter, string value, ProductCodeUKTZEDModel codeUKTZEDE)
        {
            try
            {
                await _controller.DataBaseAccess.ProductCodeUKTZEDTable.UpdateParameterAsync(codeUKTZEDE.ToProductCodeUKTZEDEntity(), parameter, value);
                return OperationResult<bool>.Success(true);
            }
            catch (Exception ex)
            {
                return OperationResult<bool>.Fail(ex.Message, ErrorType.Server, ErrorSource.Database);
            } 
        }
         
        public OperationResult<bool> Delete(int id)
        {
            try
            {
                _controller.DataBaseAccess.ProductCodeUKTZEDTable.Delete(new ShopProjectDataBase.Entities.ProductCodeUKTZEDEntity() { ID = id });
                return OperationResult<bool>.Success(true);
            }
            catch (Exception ex)
            {
                return OperationResult<bool>.Fail(ex.Message, ErrorType.Server, ErrorSource.Database);
            } 
        }

        public OperationResult<IEnumerable<ProductCodeUKTZEDModel>> GetAll()
        {
            try
            {
                var result = _controller.DataBaseAccess.ProductCodeUKTZEDTable.GetAll();
                return OperationResult<IEnumerable<ProductCodeUKTZEDModel>>.Success(result.ToProductUKTZED());
            }
            catch (Exception ex)
            {
                return OperationResult<IEnumerable<ProductCodeUKTZEDModel>>.Fail(ex.Message, ErrorType.Server, ErrorSource.Database);
            }
        }

        public OperationResult<ShopProjectWebServer.Models.Domain.Paginator.Paginator<ProductCodeUKTZEDModel,int>> GetByNamePageColumn(string name, ShopProjectWebServer.Models.Domain.Paginator.Paginator<ProductCodeUKTZEDModel, int> paginator)
        {
            try
            {
                var productCodeUKTZEDs = _controller.DataBaseAccess.ProductCodeUKTZEDTable.GetByNameAndStatus(name, (TypeStatusCodeUKTZED)paginator.DataType);

                if (productCodeUKTZEDs.Any())
                {
                    var result = ShopProjectWebServer.Models.Domain.Paginator.Paginator<ProductCodeUKTZEDEntity, int>.CreationPaginator(productCodeUKTZEDs, paginator.Page, paginator.CountItemPage, (int)paginator.DataType);
                    return OperationResult<Models.Domain.Paginator.Paginator<ProductCodeUKTZEDModel, int>>
                        .Success(new ShopProjectWebServer.Models.Domain.Paginator.Paginator<ProductCodeUKTZEDModel, int>(result.Page, result.Pages, result.CountItemPage, productCodeUKTZEDs.ToProductUKTZED(), (int)result.DataType));
                }
                else
                {
                    return OperationResult<ShopProjectWebServer.Models.Domain.Paginator.Paginator<ProductCodeUKTZEDModel, int>>.Fail("Список товарних одиниць пустий", ErrorType.NotFound, ErrorSource.Database);
                }
            }
            catch(Exception ex)
            {
                return OperationResult<ShopProjectWebServer.Models.Domain.Paginator.Paginator<ProductCodeUKTZEDModel, int>>.Fail(ex.Message, ErrorType.Server, ErrorSource.Database);
            }
        }

        public OperationResult<ShopProjectWebServer.Models.Domain.Paginator.Paginator<ProductCodeUKTZEDModel, int>> GetByCode(string code, ShopProjectWebServer.Models.Domain.Paginator.Paginator<ProductCodeUKTZEDModel, int> paginator)
        {
            try
            {
                var result = _controller.DataBaseAccess.ProductCodeUKTZEDTable.GetByCode(int.Parse(code), (TypeStatusCodeUKTZED)paginator.DataType);

                if (result.Any())
                {
                    return OperationResult<ShopProjectWebServer.Models.Domain.Paginator.Paginator<ProductCodeUKTZEDModel, int>>
                        .Success(Models.Domain.Paginator.Paginator<ProductCodeUKTZEDModel, int>.CreationPaginator(result.Reverse().ToProductUKTZED(), paginator.Page, paginator.CountItemPage, (int)paginator.DataType)); 
                }
                else
                {
                    return OperationResult<ShopProjectWebServer.Models.Domain.Paginator.Paginator<ProductCodeUKTZEDModel, int>>.Fail("Список товарних одиниць пустий", ErrorType.NotFound, ErrorSource.Database);
                } 
            }
            catch (Exception ex) 
            {
                return OperationResult<ShopProjectWebServer.Models.Domain.Paginator.Paginator<ProductCodeUKTZEDModel, int>>.Fail(ex.Message, ErrorType.Server, ErrorSource.Database);
            } 
        }

        public OperationResult<ShopProjectWebServer.Models.Domain.Paginator.Paginator<ProductCodeUKTZEDModel, int>> GetPageColumn(ShopProjectWebServer.Models.Domain.Paginator.Paginator<ProductCodeUKTZEDModel, int> paginator)
            => GetByNamePageColumn(string.Empty, paginator);

   
    }
}

using ShopProjectDataBase.Entities;
using ShopProjectDataBase.Helper;
using ShopProjectWebServer.Api.Mappings;
using ShopProjectWebServer.DataBase; 
using ShopProjectWebServer.Services.Common;
using ShopProjectWebServer.Services.Common.Enum; 
using ShopProjectWebServer.Services.Modules.Domain.ProductUnit.Interface; 
using ShopProjectWebServer.Services.Modules.Mapping.ProductUnit; 
using ProductUnitModel = ShopProjectWebServer.Models.Domain.ProductUnit.ProductUnit;

namespace ShopProjectWebServer.Services.Modules.Domain.ProductUnit
{
    internal class ProductUnitService : IProductUnitService
    {
        private DataBaseService _controller; 

        public ProductUnitService(DataBaseService controller)
        {
            _controller = controller; 
        }
        public async Task<OperationResult<ProductUnitModel>> Add(ProductUnitModel item)
        {
            try
            {
                var valid = await CreateValidation(item);
                if (valid.IsError)
                {
                    return valid;
                }

                var result = await _controller.DataBaseAccess.ProductUnitTable.AddAsync(item.ToProductUnitEntity());
                return OperationResult<ProductUnitModel>.Success(result.ToProductUnit());
            }
            catch(Exception ex)
            {
                return OperationResult<ProductUnitModel>.Fail(ex.Message, ErrorType.Server, ErrorSource.Database);
            } 
        }

        private async Task<OperationResult<ProductUnitModel>> CreateValidation(ProductUnitModel model)
        {
            if (await _controller.DataBaseAccess.ProductUnitTable.ExistsByBarCode(model.Number))
            {
                return new OperationResult<ProductUnitModel>()
                {
                    Status = ResultStatus.Error,
                    ErrorType = ErrorType.ObjectExists,
                    Source = ErrorSource.Database,
                    ErrorMessage = "Товарна одиниця існує"
                };
            }
            return new OperationResult<ProductUnitModel>()
            {
                Status = ResultStatus.Success,
            };
        }

        public async Task<OperationResult<bool>> Update(ProductUnitModel unit)
        {
            try
            {
                await _controller.DataBaseAccess.ProductUnitTable.UpdateAsync(unit.ToProductUnitEntity());
                return OperationResult<bool>.Success(true);
            }
            catch(Exception ex)
            {
                return OperationResult<bool>.Fail(ex.Message, ErrorType.Server, ErrorSource.Database);
            }
           
        }

        public async Task<OperationResult<bool>> UpdateParameter(string parameter, string value, ProductUnitModel unit)
        {
            try
            {
                await _controller.DataBaseAccess.ProductUnitTable.UpdateParameterAsync(unit.ToProductUnitEntity(), parameter, value);
                return OperationResult<bool>.Success(true);
            }
            catch(Exception ex)
            {
                return OperationResult<bool>.Fail(ex.Message, ErrorType.Server, ErrorSource.Database);
            }
        }

        public async Task<OperationResult<bool>> Delete(int id)
        {
            try
            {
                await _controller.DataBaseAccess.ProductUnitTable.DeleteAsync(id);
                return OperationResult<bool>.Success(true);
            }
            catch(Exception ex)
            {
                return OperationResult<bool>.Fail(ex.Message, ErrorType.Server, ErrorSource.Database);
            } 
        }

        public OperationResult<ShopProjectWebServer.Models.Domain.Paginator.Paginator<ProductUnitModel, int>> GetByCodePageColumn(int code, ShopProjectWebServer.Models.Domain.Paginator.Paginator<ProductUnitModel, int> paginator)
        {
            try
            {
                var items = _controller.DataBaseAccess.ProductUnitTable.GetByCode(code, (TypeStatusUnit)paginator.DataType);

                var result = ShopProjectWebServer.Models.Domain.Paginator.Paginator<ProductUnitEntity, TypeStatusUnit>.CreationPaginator(items.Reverse(), paginator.Page, paginator.CountItemPage, (TypeStatusUnit)paginator.DataType);
                if (result.Data != null)
                {

                    return OperationResult<ShopProjectWebServer.Models.Domain.Paginator.Paginator<ProductUnitModel, int>>.Success(new ShopProjectWebServer.Models.Domain.Paginator.Paginator<ProductUnitModel, int>(result.Page, result.Pages, result.CountItemPage, result.Data.ToProductUnit(), (int)result.DataType));
                }
                else
                {
                    return OperationResult<ShopProjectWebServer.Models.Domain.Paginator.Paginator<ProductUnitModel, int>>.Fail("Невдалося завантажити товари", ErrorType.NotFound, ErrorSource.Database);
                }
            }
            catch (Exception ex)
            {
                return OperationResult<ShopProjectWebServer.Models.Domain.Paginator.Paginator<ProductUnitModel, int>>.Fail(ex.Message, ErrorType.Server, ErrorSource.Database);
            }
        }

        public OperationResult<IEnumerable<ProductUnitModel>> GetAll()
        {
            try
            {
                var result = _controller.DataBaseAccess.ProductUnitTable.GetAll().ToProductUnit();
                return OperationResult<IEnumerable<ProductUnitModel>>.Success(result);
            }
            catch(Exception ex)
            {
                return OperationResult<IEnumerable<ProductUnitModel>>.Fail(ex.Message, ErrorType.Server, ErrorSource.Database);
            } 
        }

        public OperationResult<ShopProjectWebServer.Models.Domain.Paginator.Paginator<ProductUnitModel, int>> GetByNamePageColumn(string name, ShopProjectWebServer.Models.Domain.Paginator.Paginator<ProductUnitModel, int> paginator)
        {
            try
            {
                var items = _controller.DataBaseAccess.ProductUnitTable.GetByNameAndStatus(name, (TypeStatusUnit)paginator.DataType);

                var result = ShopProjectWebServer.Models.Domain.Paginator.Paginator<ProductUnitEntity, TypeStatusUnit>.CreationPaginator(items.Reverse(), paginator.Page, paginator.CountItemPage, (TypeStatusUnit)paginator.DataType);
                if (result.Data != null)
                {

                    return OperationResult<ShopProjectWebServer.Models.Domain.Paginator.Paginator<ProductUnitModel, int>>.Success(new ShopProjectWebServer.Models.Domain.Paginator.Paginator<ProductUnitModel, int>(result.Page, result.Pages, result.CountItemPage, result.Data.ToProductUnit(), (int)result.DataType));
                }
                else
                {
                    return OperationResult<ShopProjectWebServer.Models.Domain.Paginator.Paginator<ProductUnitModel, int>>.Fail("Невдалося завантажити товари", ErrorType.NotFound, ErrorSource.Database);
                }
            }
            catch(Exception ex)
            {
                return OperationResult<ShopProjectWebServer.Models.Domain.Paginator.Paginator<ProductUnitModel, int>>.Fail(ex.Message, ErrorType.Server, ErrorSource.Database);
            } 
        }

        public OperationResult<ShopProjectWebServer.Models.Domain.Paginator.Paginator<ProductUnitModel, int>> GetPageColumn(ShopProjectWebServer.Models.Domain.Paginator.Paginator<ProductUnitModel, int> paginator)
            => GetByNamePageColumn(string.Empty, paginator);

        

       
    }
}

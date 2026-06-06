using ShopProjectDataBase.Entities;
using ShopProjectDataBase.Helper;  
using ShopProjectWebServer.DataBase.Interface; 
using ShopProjectWebServer.Models.Domain.Product;
using ShopProjectWebServer.Services.Common;
using ShopProjectWebServer.Services.Common.Enum; 
using ShopProjectWebServer.Services.Modules.Domain.Product.Interface;
using ShopProjectWebServer.Services.Modules.Mapping.Product;
using System.Threading.Tasks;
using ProductModel = ShopProjectWebServer.Models.Domain.Product.Product; 

namespace ShopProjectWebServer.Services.Modules.Domain.Product
{
    public class ProductService : IProductService
    {
        private IDataBaseService _controller; 

        public ProductService(IDataBaseService controller)
        {
            _controller = controller; 
        }
        public async Task<OperationResult<ProductModel>> AddAsync(ProductModel product)
        {
            try
            {
                var valid = await CreateValidation(product);
                if (valid.IsError)
                {
                    return valid;
                }

                var result = await _controller.DataBaseAccess.ProductTable.AddAsync(product.ToProductEntity());
                return OperationResult<ProductModel>.Success(result.ToProduct());
            } 
            catch(Exception ex)
            {
                return OperationResult<ProductModel>.Fail(ex.Message,ErrorType.Server, ErrorSource.Database);
            } 
        }

        private async Task<OperationResult<ProductModel>> CreateValidation(ProductModel model)
        {
            if (await _controller.DataBaseAccess.ProductTable.ExistsByBarCode(model.Code))
            {
                return new OperationResult<ProductModel>()
                {
                    Status = ResultStatus.Error,
                    ErrorType = ErrorType.ObjectExists,
                    Source = ErrorSource.Database,
                    ErrorMessage = "Товар існує"
                };
            }
            return new OperationResult<ProductModel>()
            {
                Status = ResultStatus.Success,
            };
        }


        public async Task<OperationResult<bool>> AddRangeAsync(IEnumerable<ProductModel> items)
        {
            try
            { 
                await _controller.DataBaseAccess.ProductTable.AddRangeAsync(items.ToProductEntity());
                return OperationResult<bool>.Success(true); 
            }
            catch (Exception ex) 
            {
                return OperationResult<bool>.Fail(ex.Message, ErrorType.Server, ErrorSource.Database);
            }  
        }

        public async Task<OperationResult<bool>> UpdateAsync(ProductModel product)
        {
            try
            {
                await _controller.DataBaseAccess.ProductTable.UpdateAsync(product.ToProductEntity());
                return OperationResult<bool>.Success(true);
            }
            catch (Exception ex) 
            {
                return OperationResult<bool>.Fail(ex.Message, ErrorType.Server, ErrorSource.Database);
            }
        }
        public async Task<OperationResult<bool>> UpdateParameterAsync(string parameter, string value, ProductModel product)
        {
            try
            {
                await _controller.DataBaseAccess.ProductTable.UpdateParameterAsync(product.ToProductEntity(), parameter, value);
                return OperationResult<bool>.Success(true);
            }
            catch(Exception ex)
            {
                return OperationResult<bool>.Fail(ex.Message, ErrorType.Server, ErrorSource.Database);
            }
        }
        public async Task<OperationResult<bool>> UpdateRangeAsync(IEnumerable<ProductModel> product)
        {
            try
            {
                await _controller.DataBaseAccess.ProductTable.UpdateRangeAsync(product.ToProductEntity());
                return OperationResult<bool>.Success(true); 
            }
            catch(Exception ex)
            {
                return OperationResult<bool>.Fail(ex.Message, ErrorType.Server, ErrorSource.Database);
            }
        }

        public OperationResult<ShopProjectWebServer.Models.Domain.Paginator.Paginator<ProductModel, int>> GetByNamePageColumn(string name, ShopProjectWebServer.Models.Domain.Paginator.Paginator<ProductModel, int> paginator)
        {
            try
            {
                var products = _controller.DataBaseAccess.ProductTable.GetByNameAndStatus(name, (TypeStatusProduct)paginator.DataType);

                var result = ShopProjectWebServer.Models.Domain.Paginator.Paginator<ProductEntity, TypeStatusProduct>.CreationPaginator(products.Reverse(), paginator.Page, paginator.CountItemPage, (TypeStatusProduct)paginator.DataType);
                if (result.Data != null)
                {

                    return OperationResult<ShopProjectWebServer.Models.Domain.Paginator.Paginator<ProductModel, int>>.Success(new ShopProjectWebServer.Models.Domain.Paginator.Paginator<ProductModel, int>(result.Page, result.Pages, result.CountItemPage, result.Data.ToProduct(), (int)result.DataType));
                }
                else
                {
                    return OperationResult<ShopProjectWebServer.Models.Domain.Paginator.Paginator<ProductModel, int>>.Fail("Невдалося завантажити товари", ErrorType.NotFound, ErrorSource.Database); 
                }
            }
            catch(Exception ex)
            {
                return OperationResult<ShopProjectWebServer.Models.Domain.Paginator.Paginator<ProductModel, int>>.Fail(ex.Message, ErrorType.Server, ErrorSource.Database); 
            }
        }
        public OperationResult<ShopProjectWebServer.Models.Domain.Paginator.Paginator<ProductModel, int>> GetPageColumn(ShopProjectWebServer.Models.Domain.Paginator.Paginator<ProductModel, int> paginator) 
            => GetByNamePageColumn(string.Empty, paginator);
         
        public OperationResult<ProductModel> GetProductByBarCode(string barCode, TypeStatusProduct status)
        {
            try
            {
                var result = _controller.DataBaseAccess.ProductTable.GetByBarCode(barCode, status);
                if (result != null)
                {
                    return OperationResult<ProductModel>.Success(result.ToProduct());
                }
                else
                {
                    return OperationResult<ProductModel>.Fail("Товар не знайдено",ErrorType.NotFound, ErrorSource.Database);
                }
            }
            catch(Exception ex)
            {
                return OperationResult<ProductModel>.Fail(ex.Message, ErrorType.Server, ErrorSource.Database);
            }  
        }     
        public OperationResult<ShopProjectWebServer.Models.Domain.Paginator.Paginator<ProductModel, int>> GetByBarCode(string barCode, ShopProjectWebServer.Models.Domain.Paginator.Paginator<ProductModel, int> paginator)
        {
            try
            {
                var result = _controller.DataBaseAccess.ProductTable.GetAllByBarCode(barCode, (TypeStatusProduct)paginator.DataType);
                if(result.Any())
                {
                    return OperationResult<ShopProjectWebServer.Models.Domain.Paginator.Paginator<ProductModel, int>>
                        .Success(ShopProjectWebServer.Models.Domain.Paginator.Paginator<ProductModel, int>.CreationPaginator(result.Reverse().ToProduct(), paginator.Page, paginator.CountItemPage, paginator.DataType));
                }
                else
                {
                    return OperationResult<ShopProjectWebServer.Models.Domain.Paginator.Paginator<ProductModel, int>>.Fail("Товарів не знайдено", ErrorType.NotFound, ErrorSource.Database);
                }
            }
            catch (Exception ex)
            {
                return OperationResult<ShopProjectWebServer.Models.Domain.Paginator.Paginator<ProductModel, int>>.Fail(ex.Message, ErrorType.Server, ErrorSource.Database);
            }
        } 
        
        public OperationResult<ProductModel> GetByBarCode(string barCode , int status = 0)
        {
            try
            {
                var result = _controller.DataBaseAccess.ProductTable.GetByBarCode(barCode,(TypeStatusProduct)status);
                if (result!=null)
                {
                    return OperationResult<ProductModel> .Success(result.ToProduct());
                }
                else
                {
                    return OperationResult<ProductModel>.Fail("Товар не знайдено", ErrorType.NotFound, ErrorSource.Database);
                }
            }
            catch (Exception ex)
            {
                return OperationResult<ProductModel>.Fail(ex.Message, ErrorType.Server, ErrorSource.Database);
            }
        }

        public OperationResult<ProductsInfo> GetInfoProducts()
        {
            try
            {
                var result = new ProductsInfo();
                result.CountProductAllStatus = _controller.DataBaseAccess.ProductTable.GetCountStatusProduct(TypeStatusProduct.Unknown);
                result.CountProductInStockStatus = _controller.DataBaseAccess.ProductTable.GetCountStatusProduct(TypeStatusProduct.InStock);
                result.CountProductOutStockStatus = _controller.DataBaseAccess.ProductTable.GetCountStatusProduct(TypeStatusProduct.OutStock);
                result.CountProductArchivedStauts = _controller.DataBaseAccess.ProductTable.GetCountStatusProduct(TypeStatusProduct.Archived);
                return OperationResult<ProductsInfo>.Success(result);
            }
            catch (Exception ex)
            {
                return OperationResult<ProductsInfo>.Fail(ex.Message, ErrorType.Server, ErrorSource.Database);
            }
        }

    }
}

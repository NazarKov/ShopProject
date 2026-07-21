using ShopProjectDataBase.Entities;  
using ShopProjectWebServer.DataBase.Interface; 
using ShopProjectWebServer.Services.Common;
using ShopProjectWebServer.Services.Common.Enum; 
using ShopProjectWebServer.Services.Modules.Domain.TaxObject.Interface;
using ShopProjectWebServer.Services.Modules.Mapping.OperationRecorder;
using ShopProjectWebServer.Services.Modules.Mapping.TaxObject;
using ShopProjectWebServer.Services.Modules.Mapping.TaxObjectUser;
using ShopProjectWebServer.Services.Modules.Mapping.User; 
using TaxObjectModel = ShopProjectWebServer.Models.Domain.TaxObject.TaxObject;

namespace ShopProjectWebServer.Services.Modules.Domain.ObjectOwner
{
    internal class TaxObjectService : ITaxObjectService
    {
        private IDataBaseService _dataBaseService; 

        public TaxObjectService(IDataBaseService dataBaseService)
        {
            _dataBaseService = dataBaseService; 
        }
        public bool Delete(string token, string id)
        {
            _dataBaseService.DataBaseAccess.TaxObjectTable.Delete(new TaxObjectEntity() { ID = Guid.Parse(id) });

            return true;
        }

        public OperationResult<ShopProjectWebServer.Models.Domain.Paginator.Paginator<TaxObjectModel, int>> GetByNamePageColumn(string name, 
            ShopProjectWebServer.Models.Domain.Paginator.Paginator<TaxObjectModel, int> paginator)
        {
            try
            {
                var users = _dataBaseService.DataBaseAccess.TaxObjectTable.GetByNameAndStatus(name, (ShopProjectDataBase.Helper.TypeStatusTaxObject)paginator.DataType);

                var result = ShopProjectWebServer.Models.Domain.Paginator.Paginator<TaxObjectEntity, Models.Domain.Enum.TypeStatusTaxObject>.CreationPaginator(users.Reverse(), paginator.Page, paginator.CountItemPage, (Models.Domain.Enum.TypeStatusTaxObject)paginator.DataType);
                if (result.Data != null)
                {

                    return OperationResult<ShopProjectWebServer.Models.Domain.Paginator.Paginator<TaxObjectModel, int>>.Success(new ShopProjectWebServer.Models.Domain.Paginator.Paginator<TaxObjectModel, int>(result.Page, result.Pages, result.CountItemPage, result.Data.ToTaxObject(), (int)result.DataType));
                }
                else
                {
                    return OperationResult<ShopProjectWebServer.Models.Domain.Paginator.Paginator<TaxObjectModel, int>>.Fail("Невдалося завантажити товари", ErrorType.NotFound, ErrorSource.Database);
                }
            }
            catch (Exception ex)
            {
                return OperationResult<ShopProjectWebServer.Models.Domain.Paginator.Paginator<TaxObjectModel, int>>.Fail(ex.Message, ErrorType.Server, ErrorSource.Database);
            }
        }



        public OperationResult<ShopProjectWebServer.Models.Domain.Paginator.Paginator<TaxObjectModel, int>> GetPageColumn(ShopProjectWebServer.Models.Domain.Paginator.Paginator<TaxObjectModel, int> paginator)
            => GetByNamePageColumn(string.Empty, paginator);

        public async Task<OperationResult<TaxObjectModel>> Add(TaxObjectModel taxObject)
        {

            try
            {
                var valid = await CreateValidation(taxObject);
                if (valid.IsError)
                {
                    return valid;
                }

                var result = await _dataBaseService.DataBaseAccess.TaxObjectTable.AddAsync(taxObject.ToTaxObjectEntity());
                return OperationResult<TaxObjectModel>.Success(result.ToTaxObject());
            }
            catch (Exception ex)
            {
                return OperationResult<TaxObjectModel>.Fail(ex.Message, ErrorType.Server, ErrorSource.Database);
            }
        }

        public async Task<OperationResult<IEnumerable<TaxObjectModel>>> AddRange(IEnumerable<TaxObjectModel> taxObjects)
        {

            try
            {
                //var valid = await CreateValidation(taxObject);
                //if (valid.IsError)
                //{
                //    return valid;
                //}

                var result = await _dataBaseService.DataBaseAccess.TaxObjectTable.AddRangeAsync(taxObjects.ToTaxObjectEntity());
                return OperationResult<IEnumerable<TaxObjectModel>>.Success(result.ToTaxObject());
            }
            catch (Exception ex)
            {
                return OperationResult<IEnumerable<TaxObjectModel>>.Fail(ex.Message, ErrorType.Server, ErrorSource.Database);
            }
        }

        private async Task<OperationResult<TaxObjectModel>> CreateValidation(TaxObjectModel item)
        {
            if (await _dataBaseService.DataBaseAccess.TaxObjectTable.ExistsByName(item.NameObject))
            {
                return new OperationResult<TaxObjectModel>()
                {
                    Status = ResultStatus.Error,
                    ErrorType = ErrorType.ObjectExists,
                    Source = ErrorSource.Database,
                    ErrorMessage = "Обєкт існує"
                };
            }
            return new OperationResult<TaxObjectModel>()
            {
                Status = ResultStatus.Success,
            };
        } 

        public async Task<OperationResult<bool>> AddBindingOpearationRecorderToTaxObject(Guid idTaxObject , IEnumerable<ShopProjectWebServer.Models.Domain.OperationRecorder.OperationRecorder> operationRecorders)
        { 
            try
            {
                await _dataBaseService.DataBaseAccess.TaxObjectTable.AddBindingOperationRecorderToTaxObject(idTaxObject, operationRecorders.ToOperationRecorderEntity());
                 
                return OperationResult<bool>.Success(true);
            }
            catch (Exception ex)
            {
                return OperationResult<bool>.Fail(ex.Message, ErrorType.Server, ErrorSource.Database);
            }
        }

        public async Task<OperationResult<bool>> AddBindingUserToTaxObject(Guid idTaxObject, IEnumerable<Models.Domain.User.User> users)
        {
            try
            {
                await _dataBaseService.DataBaseAccess.TaxObjectTable.AddBindingUserToTaxObject(idTaxObject, users.ToUserEntity());

                return OperationResult<bool>.Success(true);
            }
            catch (Exception ex)
            {
                return OperationResult<bool>.Fail(ex.Message, ErrorType.Server, ErrorSource.Database);
            }
        }

        public OperationResult<IEnumerable<ShopProjectWebServer.Models.Domain.TaxObjectUser.TaxObjectUser>> GetTaxObjectsAssignedUser(Guid userId)
        {
            try
            {
                var result = _dataBaseService.DataBaseAccess.TaxObjectTable.GetTaxObjectsAssignedUser(userId);

                return OperationResult<IEnumerable<ShopProjectWebServer.Models.Domain.TaxObjectUser.TaxObjectUser>>.Success(result.ToTaxObjectUser());
            }
            catch(Exception ex)
            {
                return OperationResult<IEnumerable<ShopProjectWebServer.Models.Domain.TaxObjectUser.TaxObjectUser>>.Fail(ex.Message, ErrorType.Server, ErrorSource.Database);
            }
        }
    }
}

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

namespace ShopProjectWebServer.Services.Modules.Domain.TaxObject
{
    internal class TaxObjectService : ITaxObjectService
    {
        private IDataBaseService _dataBaseService;

        public TaxObjectService(IDataBaseService dataBaseService)
        {
            _dataBaseService = dataBaseService;
        }

        public OperationResult<Models.Domain.Paginator.Paginator<TaxObjectModel, int>> GetByNamePageColumn(string name,
            Models.Domain.Paginator.Paginator<TaxObjectModel, int> paginator)
        {
            try
            {
                var users = _dataBaseService.DataBaseAccess.TaxObjectTable.GetByNameAndStatus(name, (ShopProjectDataBase.Helper.TypeStatusTaxObject)paginator.DataType);

                var result = ShopProjectWebServer.Models.Domain.Paginator.Paginator<TaxObjectEntity, Models.Domain.Enum.TypeStatusTaxObject>.CreationPaginator(users.Reverse(), paginator.Page, paginator.CountItemPage, (Models.Domain.Enum.TypeStatusTaxObject)paginator.DataType);
                if (result.Data != null)
                {

                    return OperationResult<Models.Domain.Paginator.Paginator<TaxObjectModel, int>>.Success(new Models.Domain.Paginator.Paginator<TaxObjectModel, int>(result.Page, result.Pages, result.CountItemPage, result.Data.ToTaxObject(), (int)result.DataType));
                }
                else
                {
                    return OperationResult<Models.Domain.Paginator.Paginator<TaxObjectModel, int>>.Fail("Невдалося завантажити товари", ErrorType.NotFound, ErrorSource.Database);
                }
            }
            catch (Exception ex)
            {
                return OperationResult<Models.Domain.Paginator.Paginator<TaxObjectModel, int>>.Fail(ex.Message, ErrorType.Server, ErrorSource.Database);
            }
        }



        public OperationResult<Models.Domain.Paginator.Paginator<TaxObjectModel, int>> GetPageColumn(Models.Domain.Paginator.Paginator<TaxObjectModel, int> paginator)
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
                var valid = await CreateValidation(taxObjects);

                var objcets = new List<TaxObjectModel>();
                for (int i = 0; i < valid.Count; i++)
                {
                    if (valid.ElementAt(i).IsSuccess)
                    {
                        objcets.Add(taxObjects.ElementAt(i));
                    }
                }

                if (objcets.Count == 0)
                {
                    return OperationResult<IEnumerable<TaxObjectModel>>.Fail("Всі обрані обєкти вже добавлено", ErrorType.ObjectExists, ErrorSource.Database);
                }

                var result = await _dataBaseService.DataBaseAccess.TaxObjectTable.AddRangeAsync(objcets.ToTaxObjectEntity());
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
        private async Task<List<OperationResult<TaxObjectModel>>> CreateValidation(IEnumerable<TaxObjectModel> items)
        {
            List<OperationResult<TaxObjectModel>> result = new List<OperationResult<TaxObjectModel>>();
            foreach (var item in items)
            {
                if (await _dataBaseService.DataBaseAccess.TaxObjectTable.ExistsByName(item.NameObject))
                {
                    result.Add(new OperationResult<TaxObjectModel>()
                    {
                        Status = ResultStatus.Error,
                        ErrorType = ErrorType.ObjectExists,
                        Source = ErrorSource.Database,
                        ErrorMessage = "Обєкт існує"
                    });
                }
                else
                {
                    result.Add(new OperationResult<TaxObjectModel>()
                    {
                        Status = ResultStatus.Success
                    });
                }
            }
            return result;
        }

        public async Task<OperationResult<bool>> AddBindingOpearationRecorderToTaxObject(Guid idTaxObject, IEnumerable<Models.Domain.OperationRecorder.OperationRecorder> operationRecorders)
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

        public OperationResult<IEnumerable<Models.Domain.TaxObjectUser.TaxObjectUser>> GetTaxObjectsAssignedUser(Guid userId)
        {
            try
            {
                var result = _dataBaseService.DataBaseAccess.TaxObjectTable.GetTaxObjectsAssignedUser(userId);

                return OperationResult<IEnumerable<Models.Domain.TaxObjectUser.TaxObjectUser>>.Success(result.ToTaxObjectUser());
            }
            catch (Exception ex)
            {
                return OperationResult<IEnumerable<Models.Domain.TaxObjectUser.TaxObjectUser>>.Fail(ex.Message, ErrorType.Server, ErrorSource.Database);
            }
        }

        public async Task<OperationResult<bool>> UpdateParameter(string id, string nameParameter, object value)
        {
            try
            {
                await _dataBaseService.DataBaseAccess.TaxObjectTable.UpdateParameterAsync(new Guid(id), nameParameter, value);
                return OperationResult<bool>.Success(true);
            }
            catch (Exception ex)
            {
                return OperationResult<bool>.Fail(ex.Message, ErrorType.Server, ErrorSource.Database);
            }
        }

        public async Task<OperationResult<bool>> Update(TaxObjectModel taxObject)
        {
            try
            {  
                await _dataBaseService.DataBaseAccess.TaxObjectTable.UpdateAsync(taxObject.ToTaxObjectEntity());
                return OperationResult<bool>.Success(true);
            }
            catch (Exception ex)
            {
                return OperationResult<bool>.Fail(ex.Message, ErrorType.Server, ErrorSource.Database);
            }
        } 
    }
}

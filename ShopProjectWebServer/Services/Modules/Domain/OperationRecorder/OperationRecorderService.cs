
using ShopProjectDataBase.Entities; 
using ShopProjectWebServer.Api.DtoModels.OperationRecorder;  
using ShopProjectWebServer.DataBase.Interface;
using ShopProjectWebServer.Models.Domain.TaxObject;
using ShopProjectWebServer.Services.Common;
using ShopProjectWebServer.Services.Common.Enum;
using ShopProjectWebServer.Services.Modules.Domain.OperationRecorder.Interface;
using ShopProjectWebServer.Services.Modules.Mapping.OperationRecorder;
using System.Threading.Tasks;
using OperationRecorderModel = ShopProjectWebServer.Models.Domain.OperationRecorder.OperationRecorder;

namespace ShopProjectWebServer.Services.Modules.Domain.OperationRecorder
{
    internal class OperationRecorderService : IOperationRecorderService
    {
        private IDataBaseService _dataBaseService; 

        public OperationRecorderService(IDataBaseService dataBaseService)
        {
            _dataBaseService = dataBaseService; 
        }

        public async Task<OperationResult<OperationRecorderModel>> Add(OperationRecorderModel operationsRecorder)
        {
            try
            {
                var valid = await CreateValidation(operationsRecorder);
                if (valid.IsError)
                {
                    return valid;
                }

                var result = await _dataBaseService.DataBaseAccess.OperationRecorderTable.AddAsync(operationsRecorder.ToOperationRecorderEntity());
                return OperationResult<OperationRecorderModel>.Success(result.ToOperationRecorder());
            }
            catch (Exception ex)
            {
                return OperationResult<OperationRecorderModel>.Fail(ex.Message, ErrorType.Server, ErrorSource.Database);
            }
        }

        public async Task<OperationResult<IEnumerable<OperationRecorderModel>>> AddRange(IEnumerable<OperationRecorderModel> operationsRecorder)
        {
            try
            {  
                var result = await _dataBaseService.DataBaseAccess.OperationRecorderTable.AddRangeAsync(operationsRecorder.ToOperationRecorderEntity());
                return OperationResult<IEnumerable<OperationRecorderModel>>.Success(result.ToOperationRecorder());
            }
            catch (Exception ex)
            {
                return OperationResult<IEnumerable<OperationRecorderModel>>.Fail(ex.Message, ErrorType.Server, ErrorSource.Database);
            }
        }


        private async Task<OperationResult<OperationRecorderModel>> CreateValidation(OperationRecorderModel item)
        {
            if (await _dataBaseService.DataBaseAccess.OperationRecorderTable.ExistsByName(item.Name))
            {
                return new OperationResult<OperationRecorderModel>()
                {
                    Status = ResultStatus.Error,
                    ErrorType = ErrorType.ObjectExists,
                    Source = ErrorSource.Database,
                    ErrorMessage = "Обєкт існує"
                };
            }
            return new OperationResult<OperationRecorderModel>()
            {
                Status = ResultStatus.Success,
            };
        }

        public OperationResult<ShopProjectWebServer.Models.Domain.Paginator.Paginator<OperationRecorderModel, int>> GetByNamePageColumn(string name,
                   ShopProjectWebServer.Models.Domain.Paginator.Paginator<OperationRecorderModel, int> paginator)
        {
            try
            {
                var users = _dataBaseService.DataBaseAccess.OperationRecorderTable.GetByNameAndStatus(name, (ShopProjectDataBase.Helper.TypeStatusOperationRecorder)paginator.DataType);

                var result = ShopProjectWebServer.Models.Domain.Paginator.Paginator<OperationsRecorderEntity, Models.Domain.Enum.TypeStatusOperationRecorder>.CreationPaginator(users.Reverse(), paginator.Page, paginator.CountItemPage, (Models.Domain.Enum.TypeStatusOperationRecorder)paginator.DataType);
                if (result.Data != null)
                {

                    return OperationResult<ShopProjectWebServer.Models.Domain.Paginator.Paginator<OperationRecorderModel, int>>.Success(new ShopProjectWebServer.Models.Domain.Paginator.Paginator<OperationRecorderModel, int>(result.Page, result.Pages, result.CountItemPage, result.Data.ToOperationRecorder(), (int)result.DataType));
                }
                else
                {
                    return OperationResult<ShopProjectWebServer.Models.Domain.Paginator.Paginator<OperationRecorderModel, int>>.Fail("Невдалося завантажити товари", ErrorType.NotFound, ErrorSource.Database);
                }
            }
            catch (Exception ex)
            {
                return OperationResult<ShopProjectWebServer.Models.Domain.Paginator.Paginator<OperationRecorderModel, int>>.Fail(ex.Message, ErrorType.Server, ErrorSource.Database);
            }
        }
        public OperationResult<ShopProjectWebServer.Models.Domain.Paginator.Paginator<OperationRecorderModel, int>> GetPageColumn(ShopProjectWebServer.Models.Domain.Paginator.Paginator<OperationRecorderModel, int> paginator)
           => GetByNamePageColumn(string.Empty, paginator);

        

        public bool AddBindingOperationRecorder(string token, string idoperationrecoreder, string idobjectowner)
        { 
           // _controller.DataBaseAccess.OperationRecorderTable.AddBinding(Guid.Parse(idoperationrecoreder),Guid.Parse(idobjectowner));
            return true;
        } 
    }
}

using ShopProjectWebServer.DataBase.Interface;
using ShopProjectWebServer.Models.Domain.WorkingShift;
using ShopProjectWebServer.Services.Common;
using ShopProjectWebServer.Services.Common.Enum; 
using ShopProjectWebServer.Services.Modules.Domain.WorkingShift.Interface;
using ShopProjectWebServer.Services.Modules.Mapping.MediaAccessControl;
using ShopProjectWebServer.Services.Modules.Mapping.WorkingShift;
using WorkingShiftModel = ShopProjectWebServer.Models.Domain.WorkingShift.WorkingShift;

namespace ShopProjectWebServer.Services.Modules.Domain.WorkingShift
{
    internal class WorkingShiftService : IWorkingShiftService
    {
        private IDataBaseService _dataBaseService;

        public WorkingShiftService(IDataBaseService dataBaseService)
        {
            _dataBaseService = dataBaseService;
        }
        public async Task<OperationResult<WorkingShiftModel>> Add(WorkingShiftModel item)
        {
            try
            {
                var result = await _dataBaseService.DataBaseAccess.WorkingShiftTable.Add(item.ToWorkingShiftEntity());
                return OperationResult<WorkingShiftModel>.Success(result.ToWorkicingShift());
            }
            catch (Exception ex)
            {
                return OperationResult<WorkingShiftModel>.Fail(ex.Message, ErrorType.Server, ErrorSource.Database);
            }
        }
        public async Task<OperationResult<WorkingShiftModel>> Update(WorkingShiftModel item)
        {
            try
            {
                var result = await _dataBaseService.DataBaseAccess.WorkingShiftTable.Update(item.ToWorkingShiftEntity());
                return OperationResult<Models.Domain.WorkingShift.WorkingShift>.Success(result.ToWorkicingShift());
            }
            catch (Exception ex)
            {
                return OperationResult<WorkingShiftModel>.Fail(ex.Message, ErrorType.Server, ErrorSource.Database);
            }
        }


        public async Task<OperationResult<WorkingShiftModel>> GetById(int id)
        {
            try
            {
                var result = _dataBaseService.DataBaseAccess.WorkingShiftTable.GetById(id);

                if (result == null)
                {
                    return OperationResult<WorkingShiftModel>.Fail("Невдалося завантажити зміну", ErrorType.Server, ErrorSource.Database);
                    throw new Exception("Невдалося завантажити зміну");
                }
                return OperationResult<WorkingShiftModel>.Success(result.ToWorkicingShift());
            }
            catch (InvalidOperationException invalidOperationException)
            {
                return OperationResult<WorkingShiftModel>.Fail(invalidOperationException.Message, ErrorType.NotFound, ErrorSource.Database);
            }
            catch (Exception ex)
            {
                return OperationResult<WorkingShiftModel>.Fail(ex.Message, ErrorType.Server, ErrorSource.Database);
            }
        }

        public async Task<OperationResult<WorkingShiftResourse>> GetResourseByWorkingShift(string fisclaNumberRRo)
        {
            try
            {
                int id = _dataBaseService.DataBaseAccess.WorkingShiftTable.GetLastId(fisclaNumberRRo); 
                var lastMac = _dataBaseService.DataBaseAccess.MediaAccessControlTable.GetLast(id);
                var lastOperation = _dataBaseService.DataBaseAccess.OperationTable.GetLastItem(id);
                 
                if(lastMac == null)
                {
                    lastMac = new ShopProjectDataBase.Entities.MediaAccessControlEntity();
                }
                if(lastOperation == null)
                {
                    lastOperation = new ShopProjectDataBase.Entities.OperationEntity();
                } 
                return OperationResult<WorkingShiftResourse>.Success(new WorkingShiftResourse() { OperationNumber = lastOperation.NumberPayment,MediaAccessControl = lastMac.ToMediaAccessControl()});
            }
            catch (InvalidOperationException exeption)
            {
                if(exeption.Message == "Sequence contains no elements")
                {
                    return OperationResult<WorkingShiftResourse>.Success(new WorkingShiftResourse() { MediaAccessControl = new Models.Domain.MediaAccessControl.MediaAccessControl()});
                }
                throw;
            } 
            catch (Exception ex)
            {
                return OperationResult<WorkingShiftResourse>.Fail(ex.Message, ErrorType.Server, ErrorSource.Database);
            }
        }
    }
}

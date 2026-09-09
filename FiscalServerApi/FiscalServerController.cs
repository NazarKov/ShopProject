using FiscalServerApi.ExceptionServer;
using FiscalServerApi.Helpers;
using FiscalServerApi.Services.Common;
using Google.Protobuf;
using GreetClient;
using Grpc.Core;
using Grpc.Net.Client;
using System.Threading.Channels;
using static GreetClient.ChkIncomeService;

namespace FiscalServerApi
{
    public class FiscalServerController
    {

        private string _apiAddress = "https://prro.tax.gov.ua:443";
        private string _apiTestAddress = "https://cabinet.tax.gov.ua:9443"; 
        private string _pathFile = "C:\\ProgramData\\ShopProject\\Temp\\Chek.xml.p7s"; 
        private CallOptions _callOptions; 
        private readonly Dictionary<string, ChkIncomeService.ChkIncomeServiceClient> _clients = new(); 
        public FiscalServerController()  
        { 
            _clients.Add("clientTest", new ChkIncomeService.ChkIncomeServiceClient(GrpcChannel.ForAddress(_apiTestAddress)));
            _clients.Add("clientProd", new ChkIncomeService.ChkIncomeServiceClient(GrpcChannel.ForAddress(_apiAddress)));
        }


        private OperationResult<string> SendMessage(Messages message,TypeMessage types,double second = 100)
        { 
            try
            {  
                var result = new CheckResponse();

                var client = message.test ? _clients["clientTest"] : _clients["clientProd"];

                ByteString CheckSign = ReadFile(_pathFile);
                _callOptions = new CallOptions().WithDeadline(DateTime.UtcNow.AddSeconds(second));

                switch (types)
                {
                    case TypeMessage.sendChk2:
                        {
                            result = sendChkV2(message, client, CheckSign);
                            break;
                        }
                    case TypeMessage.ping:
                        {
                            result = ping(message, client, CheckSign);
                            break;
                        }
                }
                return AuditErrorServer(result);
            } 
            catch (Exception exeption)
            { 
                return OperationResult<string>.Fail(exeption.Message);
            }
        }
        
        private CheckResponse sendChkV2(Messages message, ChkIncomeServiceClient client, ByteString CheckSign)
        {
            var reply = client.sendChkV2(new Check()
            {
                CheckSign = CheckSign,
                CheckType = message.type,
                DateTime = message.date,
                RroFn = message.rroFn,
                LocalNumber = message.localNumber,
            }, _callOptions);
            return reply;
        }
        private CheckResponse ping(Messages message, ChkIncomeServiceClient client, ByteString CheckSign)
        {
            var reply = client.ping(new Check()
            {
                CheckSign = CheckSign,
                CheckType = message.type,
                DateTime = message.date,
                RroFn = message.rroFn,
                LocalNumber = message.localNumber,
            }, _callOptions);
            return reply;
        }


        public OperationResult<string> SendFiscalCheck(long date, int localNumber, string rroFN , bool test = true)
        {
            return SendMessage(new Messages() 
            {
                date = date,
                localNumber = localNumber,
                rroFn = rroFN,
                test = test,
                type = Check.Types.Type.Chk
            },TypeMessage.sendChk2); 
        }

        public OperationResult<string> SendServiceCheck(long date, int localNumber, string rroFN , bool test = true)
        {
            return SendMessage(new Messages()
            {
                date = date,
                localNumber = localNumber,
                rroFn = rroFN,
                test = test,
                type = Check.Types.Type.Servicechk,
            }, TypeMessage.sendChk2); 

        }
        
        public OperationResult<string> SendZReport(long date, int localNumber, string rroFN , bool test = true)
        {
            return SendMessage(new Messages()
            {
                date = date,
                localNumber = localNumber,
                rroFn = rroFN,
                test = test,
                type = Check.Types.Type.Zreport,
            }, TypeMessage.sendChk2); 
        }
        
        public OperationResult<string> Ping(long date, int localNumber, string rroFN , bool test = true)
        {
            return SendMessage(new Messages()
            {
                date = date,
                localNumber = localNumber,
                rroFn = rroFN,
                test = test,
                type = Check.Types.Type.Zreport,
            }, TypeMessage.ping); 
        }

        private OperationResult<string> AuditErrorServer(CheckResponse response)
        {
            if (response != null)
            {
                switch (response.Status)
                {
                    case CheckResponse.Types.Status.Unknown:
                        {
                            return OperationResult<string>.Fail("Не вдалося відправити чек"); 
                        }
                    case CheckResponse.Types.Status.Ok:
                        {
                            return OperationResult<string>.Success(response.Id); 
                        }
                    case CheckResponse.Types.Status.ErrorVerefy:
                        {
                            return OperationResult<string>.Fail("Помилка перевірки підпису,перевірте наявність встановленого ключа ФОП"); 
                        }
                    case CheckResponse.Types.Status.ErrorCheck:
                        {
                            switch (response.ErrorMessage)
                            {
                                case ExceptionCheckShiftIsArlreadyOpen.ShiftIsAlreadyOpen:
                                    {
                                        return OperationResult<string>.Fail("Зміна вже відкрита"); 
                                    }
                                case ExceptionCheck.ThereCanBeOnlyOneSignatoryWithinAShift:
                                    {
                                        return OperationResult<string>.Fail("У зміні може бути лише один підписант"); 
                                    }
                                case ExceptionCheck.ThereCanBeOnlyOneSignatoryWithinAShiftClosingCanBeASenior:
                                    {
                                        return OperationResult<string>.Fail("У зміні може бути лише один підписант,\n закриття зміни може бути здійснене старшим касиром"); 
                                    }
                                case ExceptionCheck.ThisKeyOpensAShiftOnAnotherDeviceFn:
                                    {
                                        return OperationResult<string>.Fail("Цим підписом відкрита зміна на іншому ПРРО"); 
                                    }
                                case ExceptionCheck.PermittedToUseOnlyAfter:
                                    {
                                        return OperationResult<string>.Fail("можливо використовувати тільки з 01.10. 2021"); 
                                    }
                            }
                            return OperationResult<string>.Fail("Помилка перевірки РРО"); 
                        }
                    case CheckResponse.Types.Status.ErrorSave:
                        {
                            if (response.ErrorMessage.Equals(ExceptionSave.IncorrectHash))
                            {
                                var result = OperationResult<string>.Fail("Невірний хеш попереднього чеку,\n або дубль чека",Services.Common.Enum.ErrorType.IncorrectHash);
                                result.Data = response.ErrorMessage;
                                return result;
                            }
                            return OperationResult<string>.Fail("Помилка запису"); 
                        }
                    case CheckResponse.Types.Status.ErrorUnknown:
                        {
                            return OperationResult<string>.Fail("Загальна помилка"); 
                        }
                    case CheckResponse.Types.Status.ErrorType:
                        {
                            return OperationResult<string>.Fail("Помилка типу посилки"); 
                        }
                    case CheckResponse.Types.Status.ErrorNotPrevZreport:
                        {
                            return OperationResult<string>.Fail("Нема Z-звіту за попередній день"); 
                        }
                    case CheckResponse.Types.Status.ErrorXml:
                        {
                            return OperationResult<string>.Fail("Невірний формат XML ( структура , фіскальний номер)"); 
                        }
                    case CheckResponse.Types.Status.ErrorXmlDate:
                        {
                            return OperationResult<string>.Fail("Невірний формат XML дата не відповідає Check.date \nПеревірте чи підключений ключ ФОП до програми"); 
                        }
                    case CheckResponse.Types.Status.ErrorXmlChk:
                        {
                            return OperationResult<string>.Fail("Невірний формат XML чеку"); 
                        }
                    case CheckResponse.Types.Status.ErrorXmlZreport:
                        {
                            return OperationResult<string>.Fail("Невірний формат Z-звіту"); 
                        }
                    case CheckResponse.Types.Status.ErrorOffline168:
                        {
                            return OperationResult<string>.Fail("РРО заблокований, перевищено ліміт 168 годин офлайну"); 
                        }
                    case CheckResponse.Types.Status.ErrorBadHashPrev:
                        {
                            var result = OperationResult<string>.Fail("Невірний хеш попереднього чеку",Services.Common.Enum.ErrorType.ErrorBadHashPrev);
                            result.Data = response.ErrorMessage;
                            return result; 
                        }
                    case CheckResponse.Types.Status.ErrorNotRegisteredRro:
                        {
                            return OperationResult<string>.Fail("Не зареєстровано ПРРО"); 
                        }
                    case CheckResponse.Types.Status.ErrorNotRegisteredSigner:
                        {
                            return OperationResult<string>.Fail("Не зареєстрований підписант"); 
                        }
                    case CheckResponse.Types.Status.ErrorNotOpenShift:
                        {
                            return OperationResult<string>.Fail("Не відкрита зміна"); 
                        }
                    case CheckResponse.Types.Status.ErrorOfflineId:
                        {
                            return OperationResult<string>.Fail("Невірний офлайн ID"); 
                        }
                    default:
                        {
                            return OperationResult<string>.Fail("Невдалося зберегти фіксальний чек");  
                        }
                } 
            }
            return OperationResult<string>.Fail("Невдалося зберегти фіксальний чек");
        }
        private ByteString ReadFile(string path)
        {
            byte[] bytes = File.ReadAllBytes(path);
            if(bytes == null)
            {
                throw new Exception("файл не знайдено");
            }
            return ByteString.CopyFrom(bytes);
        }

    }
   
}

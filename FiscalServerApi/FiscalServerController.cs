using FiscalServerApi.ExceptionServer;
using FiscalServerApi.Helpers;
using Google.Protobuf;
using GreetClient;
using Grpc.Core;
using Grpc.Net.Client;
using static GreetClient.ChkIncomeService;

namespace FiscalServerApi
{
    public class FiscalServerController
    {

        private string _apiAddress = "https://prro.tax.gov.ua:443";
        private string _apiTestAddress = "https://cabinet.tax.gov.ua:9443";

        private string _pathFile = "C:\\ProgramData\\ShopProject\\Temp\\Chek.xml.p7s";

        private CallOptions _callOptions;

        public FiscalServerController() 
        {

        }

        private CheckResponse SendMessage(Messages message,TypeMessage types, string api = "")
        {
            if(message.test)
            {
                api = _apiTestAddress;
            }
            else
            {
                api = _apiAddress;
            }
            
            return SendMessageRecursive(api,message,types,10,0,5);
        }
        private CheckResponse SendMessageRecursive(string api, Messages message, TypeMessage types, double second, int depth, int maxDepth)
        {
            try
            {
                if (depth >= maxDepth)
                {
                    return new CheckResponse();
                }

                using (var channel = GrpcChannel.ForAddress(api))
                {

                    _callOptions = new CallOptions().WithDeadline(DateTime.UtcNow.AddSeconds(second));

                    var client = new ChkIncomeService.ChkIncomeServiceClient(channel);

                    ByteString CheckSign = ReadFile(_pathFile);

                    switch(types)
                    {
                        case TypeMessage.sendChk2:
                            {
                                return sendChkV2(message, client, CheckSign);
                            }
                        case TypeMessage.ping:
                            {
                                return ping(message, client, CheckSign);
                            }
                    }
                }
                return null;
            }
            catch(Grpc.Core.RpcException)
            {
                throw new Exception("Відсутьнє підключення до інтернету\nперевірте підключення до інтернету");
            }
            catch (Exception)
            {
                return SendMessageRecursive(api,message,types,second + 5,depth + 1,5);
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


        public void SendFiscalCheck(long date, int localNumber, string rroFN , bool test = true)
        {
            CheckResponse response = SendMessage(new Messages() 
            {
                date = date,
                localNumber = localNumber,
                rroFn = rroFN,
                test = test,
                type = Check.Types.Type.Chk
            },TypeMessage.sendChk2);

            AuditErrorServer(response);
        }

        public void SendServiceCheck(long date, int localNumber, string rroFN , bool test = true)
        {
            CheckResponse response = SendMessage(new Messages()
            {
                date = date,
                localNumber = localNumber,
                rroFn = rroFN,
                test = test,
                type = Check.Types.Type.Servicechk,
            }, TypeMessage.sendChk2);

            AuditErrorServer(response);

        }
        
        public void SendZReport(long date, int localNumber, string rroFN , bool test = true)
        {
            CheckResponse response = SendMessage(new Messages()
            {
                date = date,
                localNumber = localNumber,
                rroFn = rroFN,
                test = test,
                type = Check.Types.Type.Zreport,
            }, TypeMessage.sendChk2);

            AuditErrorServer(response);
        }
        
        public void Ping(long date, int localNumber, string rroFN , bool test = true)
        {
            CheckResponse response = SendMessage(new Messages()
            {
                date = date,
                localNumber = localNumber,
                rroFn = rroFN,
                test = test,
                type = Check.Types.Type.Zreport,
            }, TypeMessage.ping);

            AuditErrorServer(response);
        }

        private void AuditErrorServer(CheckResponse response)
        {
            if (response != null)
            {
                switch (response.Status)
                {
                    case CheckResponse.Types.Status.Unknown:
                        {
                            throw new Exception("Не вдалося відправити чек");
                        }
                    case CheckResponse.Types.Status.Ok:
                        {
                            throw new ExceptionOK("OK",response.Id);
                        }
                    case CheckResponse.Types.Status.ErrorVerefy:
                        {
                            throw new Exception("Помилка перевірки підпису,перевірте наявність встановленого ключа ФОП");
                        }
                    case CheckResponse.Types.Status.ErrorCheck:
                        {
                            switch (response.ErrorMessage)
                            {
                                case ExceptionCheck.ShiftIsAlreadyOpen:
                                    {
                                        throw new ExceptionCheck("Зміна вже відкрита");
                                    }
                                case ExceptionCheck.ThereCanBeOnlyOneSignatoryWithinAShift:
                                    {
                                        throw new ExceptionCheck("У зміні може бути лише один підписант");
                                    }
                                case ExceptionCheck.ThereCanBeOnlyOneSignatoryWithinAShiftClosingCanBeASenior:
                                    {
                                        throw new ExceptionCheck("У зміні може бути лише один підписант,\n закриття зміни може бути здійснене старшим касиром");
                                    }
                                case ExceptionCheck.ThisKeyOpensAShiftOnAnotherDeviceFn:
                                    {
                                        throw new ExceptionCheck("Цим підписом відкрита зміна на іншому ПРРО");
                                    }
                                case ExceptionCheck.PermittedToUseOnlyAfter:
                                    {
                                        throw new ExceptionCheck("можливо використовувати тільки з 01.10. 2021");
                                    }
                            }
                            throw new ExceptionCheck("Помилка перевірки РРО");
                        }
                    case CheckResponse.Types.Status.ErrorSave:
                        {
                            if (response.ErrorMessage.Equals(ExceptionSave.IncorrectHash))
                            {
                                throw new ExceptionSave("Невірний хеш попереднього чеку,\n або дубль чека", response.ErrorMessage);
                            }
                            throw new Exception("Помилка запису");
                        }
                    case CheckResponse.Types.Status.ErrorUnknown:
                        {
                            throw new Exception("Загальна помилка");
                        }
                    case CheckResponse.Types.Status.ErrorType:
                        {
                            throw new Exception("Помилка типу посилки");
                        }
                    case CheckResponse.Types.Status.ErrorNotPrevZreport:
                        {
                            throw new Exception("Нема Z-звіту за попередній день");
                        }
                    case CheckResponse.Types.Status.ErrorXml:
                        {
                            throw new Exception("Невірний формат XML ( структура , фіскальний номер)");
                        }
                    case CheckResponse.Types.Status.ErrorXmlDate:
                        {
                            throw new Exception("Невірний формат XML дата не відповідає Check.date \nПеревірте чи підключений ключ ФОП до програми");
                        }
                    case CheckResponse.Types.Status.ErrorXmlChk:
                        {
                            throw new Exception("Невірний формат XML чеку");
                        }
                    case CheckResponse.Types.Status.ErrorXmlZreport:
                        {
                            throw new Exception("Невірний формат Z-звіту");
                        }
                    case CheckResponse.Types.Status.ErrorOffline168:
                        {
                            throw new Exception("РРО заблокований, перевищено ліміт 168 годин офлайну");
                        }
                    case CheckResponse.Types.Status.ErrorBadHashPrev:
                        {

                            throw new ExceptionBadHashPrev("Невірний хеш попереднього чеку",response.ErrorMessage);
                        }
                    case CheckResponse.Types.Status.ErrorNotRegisteredRro:
                        {
                            throw new Exception("Не зареєстровано ПРРО");
                        }
                    case CheckResponse.Types.Status.ErrorNotRegisteredSigner:
                        {
                            throw new Exception("Не зареєстрований підписант");
                        }
                    case CheckResponse.Types.Status.ErrorNotOpenShift:
                        {
                            throw new Exception("Не відкрита зміна");
                        }
                    case CheckResponse.Types.Status.ErrorOfflineId:
                        {
                            throw new Exception("Невірний офлайн ID");
                        }


                }
            }
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

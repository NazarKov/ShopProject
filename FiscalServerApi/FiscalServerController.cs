using Google.Protobuf;
using GreetClient;
using Grpc.Core;
using Grpc.Net.Client;

namespace FiscalServerApi
{
    public class FiscalServerController
    {

        private string _apiAdress = "https://prro.tax.gov.ua:443";
        private string _apiTestAdress = "https://cabinet.tax.gov.ua:9443";

        private string _pathFile = "..\\..\\..\\..\\ShopProject\\Resource\\BufferStorage\\Chek.xml.p7s";

        private CallOptions _callOptions;

        public FiscalServerController() { }

        public string SendChek(long date, int localNumber, string rroFN)
        {
            CheckResponse response = SendMessages(new ChekMesseges()
            {
                date = date,
                localNumber = localNumber,
                rroFn = rroFN,
                type = Check.Types.Type.Chk,
            });

            if (AuditErrorServer(response) == "OK")
            {
                return response.Id;
            }
            return string.Empty;
        }
        public bool SendServicechk(long date, int localNumber, string rroFN)
        {
            CheckResponse response = SendMessages(new ChekMesseges()
            {
                date = date,
                localNumber = localNumber,
                rroFn = rroFN,
                type = Check.Types.Type.Servicechk,
            });

            if (AuditErrorServer(response) == "OK")
            {
                return true;
            }
            return false;
        }
        public bool SendZreport(long date, int localNumber, string rroFN)
        {
            CheckResponse response = SendMessages(new ChekMesseges()
            {
                date = date,
                localNumber = localNumber,
                rroFn = rroFN,
                type = Check.Types.Type.Zreport,
            });

            if (AuditErrorServer(response) == "OK")
            {
                return true;
            }
            return false;
        }
        public bool Ping(long date, int localNumber, string rroFN)
        {
            CheckResponse response = Ping(new ChekMesseges()
            {
                date = date,
                localNumber = localNumber,
                rroFn = rroFN,
                type = Check.Types.Type.Zreport,
            });

            if (AuditErrorServer(response) == "OK")
            {
                return true;
            }
            return false;
        }

        private CheckResponse SendMessages(ChekMesseges messeges)
        {
            using (var channel = GrpcChannel.ForAddress(_apiTestAdress))
            {

                _callOptions = new CallOptions().WithDeadline(DateTime.UtcNow.AddSeconds(10));

                var client = new ChkIncomeService.ChkIncomeServiceClient(channel);

                ByteString CheckSign = ReadFile(_pathFile);

                var reply = client.sendChkV2(new Check()
                {
                    CheckSign = CheckSign,
                    CheckType = messeges.type,
                    DateTime = messeges.date,
                    RroFn = messeges.rroFn,
                    LocalNumber = messeges.localNumber,
                }, _callOptions);
                return reply;
            }
        }
        private CheckResponse Ping(ChekMesseges messeges)
        {
            using (var channel = GrpcChannel.ForAddress(_apiTestAdress))
            {

                _callOptions = new CallOptions().WithDeadline(DateTime.UtcNow.AddSeconds(10));

                var client = new ChkIncomeService.ChkIncomeServiceClient(channel);

                ByteString CheckSign = ReadFile(_pathFile);

                var reply = client.ping(new Check()
                {
                    CheckSign = CheckSign,
                    CheckType = messeges.type,
                    DateTime = messeges.date,
                    RroFn = messeges.rroFn,
                    LocalNumber = messeges.localNumber,
                }, _callOptions);
                return reply;
            }
        }
        public string SendServiseChekAdjustmentHash(long date,string rroFN)
        {
            CheckResponse response = SendMessages(new ChekMesseges()
            {
                date = date,
                localNumber = 0,
                rroFn = rroFN,
                type = Check.Types.Type.Servicechk,
            });

            return response.ErrorMessage.Split(" ").ElementAt(3);
        }
        public string SendChekAdjustmentHash(long date, string rroFN)
        {
            CheckResponse response = SendMessages(new ChekMesseges()
            {
                date = date,
                localNumber = 0,
                rroFn = rroFN,
                type = Check.Types.Type.Chk,
            });

            return response.ErrorMessage.Split(" ").ElementAt(3);
        }
        public string SendZtreportAdjustmentHash(long date, string rroFN)
        {
            CheckResponse response = SendMessages(new ChekMesseges()
            {
                date = date,
                localNumber = 0,
                rroFn = rroFN,
                type = Check.Types.Type.Zreport,
            });

            return response.ErrorMessage.Split(" ").ElementAt(3);
        }


        private string AuditErrorServer(CheckResponse response)
        {
            if (response != null)
            {
                switch (response.Status)
                {
                    case CheckResponse.Types.Status.Unknown:
                        {
                            throw new Exception("Не визначений");
                            break;
                        }
                    case CheckResponse.Types.Status.Ok:
                        {
                            return "OK";
                            break;
                        }
                    case CheckResponse.Types.Status.ErrorVerefy:
                        {
                            throw new Exception("Чек фіскалізовано, надано номер");
                            break;
                        }
                    case CheckResponse.Types.Status.ErrorCheck:
                        {
                            throw new Exception("помилка перевірки РРО");
                            break;
                        }
                    case CheckResponse.Types.Status.ErrorSave:
                        {
                            throw new Exception("Помилка запису");
                            break;
                        }
                    case CheckResponse.Types.Status.ErrorUnknown:
                        {
                            throw new Exception("Загальна помилка");
                            break;
                        }
                    case CheckResponse.Types.Status.ErrorType:
                        {
                            throw new Exception("Помилка типу посилки");
                            break;
                        }
                    case CheckResponse.Types.Status.ErrorNotPrevZreport:
                        {
                            throw new Exception("Нема Z-звіту за попередній день");
                            break;
                        }
                    case CheckResponse.Types.Status.ErrorXml:
                        {
                            throw new Exception("Невірний формат XML ( структура , фіскальний номер)");
                            break;
                        }
                    case CheckResponse.Types.Status.ErrorXmlDate:
                        {
                            throw new Exception("Невірний формат XML дата не відповідає Check.date");
                            break;
                        }
                    case CheckResponse.Types.Status.ErrorXmlChk:
                        {
                            throw new Exception("Невірний формат XML чеку");
                            break;
                        }
                    case CheckResponse.Types.Status.ErrorXmlZreport:
                        {
                            throw new Exception("Невірний формат Z-звіту");
                            break;
                        }
                    case CheckResponse.Types.Status.ErrorOffline168:
                        {
                            throw new Exception("РРО заблокований, перевищено ліміт 168 годин офлайну");
                            break;
                        }
                    case CheckResponse.Types.Status.ErrorBadHashPrev:
                        {
                            throw new Exception("Невірний хеш попереднього чеку");
                            break;
                        }
                    case CheckResponse.Types.Status.ErrorNotRegisteredRro:
                        {
                            throw new Exception("Не зареєстровано ПРРО");
                            break;
                        }
                    case CheckResponse.Types.Status.ErrorNotRegisteredSigner:
                        {
                            throw new Exception("Не зареєстрований підписант");
                            break;
                        }
                    case CheckResponse.Types.Status.ErrorNotOpenShift:
                        {
                            throw new Exception("Не відкрита зміна");
                            break;
                        }
                    case CheckResponse.Types.Status.ErrorOfflineId:
                        {
                            throw new Exception("Невірний оффлайн ID");
                            break;
                        }


                }

            }
            return null;
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

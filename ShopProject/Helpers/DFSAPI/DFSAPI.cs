using GreetClient;
using Grpc.Core;
using Grpc.Net.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ShopProject.Helpers.DFSAPI
{
    internal class DFSAPI
    {
        public DFSAPI() { }

        public void set()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            using var channel = GrpcChannel.ForAddress("https://cabinet.tax.gov.ua:9443");
            var options = new CallOptions().WithDeadline(DateTime.UtcNow.AddSeconds(100));

            var client = new ChkIncomeService.ChkIncomeServiceClient(channel);

            //CheckResponse reply = client.sendChkV2(new Check()
            //{
            //    CheckSign = bytes1,
            //    CheckType = Check.Types.Type.Zreport,
            //    DateTime = long.Parse("20230703135900"),
            //    RroFn = "4000512773",
            //    LocalNumber = 1
            //});

        }
    }
}

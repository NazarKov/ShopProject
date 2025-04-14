using ShopProject.Helpers.NetworkServise;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ShopProject.Model.HomePage
{
    internal class StartModel
    {
        public StartModel() { }


        public async Task<string> GetUrl(string iprouter, int port, int minAddress, int maxAddress)
        {
            try
            {
                NetworkScanner.IpRouter = iprouter;
                NetworkScanner.Port=port;
                NetworkScanner.MinIPAddress=minAddress;
                NetworkScanner.MaxIPAddress=maxAddress;

                var URl = await NetworkScanner.SearchDataBaseURLAsync();

                return URl;

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                return string.Empty;
            }
        }
    }
}

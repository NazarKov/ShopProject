using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ShopProject.Helpers.NetworkServise.ElectronicTaxAccountPublicApi
{
    internal class MainElectronicTaxAccountController
    {
        private readonly string _api = "https://cabinet.tax.gov.ua/ws/public_api/payer_card";
        private readonly string _pathFile = "C:\\ProgramData\\ShopProject\\Temp\\Key.txt.p7s";


        public MainElectronicTaxAccountController() { }

        public async Task<string> Send()
        {
            try
            {
                var base64Certificate = ReadFile(_pathFile);
                using (HttpClient client = new HttpClient())
                {

                    client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");
                    client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", base64Certificate);
                    client.DefaultRequestHeaders.TryAddWithoutValidation("Accept", "application/json");


                    HttpResponseMessage response = await client.GetAsync(_api);

                    if (response.IsSuccessStatusCode)
                    {
                        return await response.Content.ReadAsStringAsync();
                    }
                    else
                    {
                        throw new Exception("Помилка при виконанні запиту. Код статусу: " + (int)response.StatusCode);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        public static string ReadFile(string path)
        {
            byte[] bytes = File.ReadAllBytes(path);
            if (bytes == null)
            {
                throw new Exception("файл не знайдено");
            }

            return Convert.ToBase64String(bytes);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ShopProject.Helpers.NetworkServise.ElectronicTaxAccountPublicApi.Model
{
    public class DataJsonHttpResponse
    {
        public int idGroup { get; set; }
        public string title { get; set; }
        public int type { get; set; }
        public Header headers { get; set; }
        public Values values { get; set; }
        public List<ListValue> listValues { get; set; }

        public string ToJson()
        {
            return JsonSerializer.Serialize(this);
        }

        public static DataJsonHttpResponse FromJson(string json)
        {
            return JsonSerializer.Deserialize<DataJsonHttpResponse>(json);
        }
        public static List<DataJsonHttpResponse> FromJsonList(string json)
        {
            return JsonSerializer.Deserialize<List<DataJsonHttpResponse>>(json);
        }
    }
}

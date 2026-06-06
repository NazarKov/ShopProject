using ShopProject.Model.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ShopProject.Services.Integration.Network.WebServerApi.DtoModels.Paginator
{
    public class PaginatorDto<TData,TStatusData>
    {
        [JsonPropertyName("Page")]
        public int Page { get; set; }
        [JsonPropertyName("Pages")]
        public int Pages { get; set; }
        [JsonPropertyName("CountItemPage")]
        public int CountItemPage { get; set; }
        [JsonPropertyName("Data")]
        public IEnumerable<TData>? Data { get; set; }
        [JsonPropertyName("DataType")]
        public TStatusData DataType { get; set; }
        public PaginatorDto() { }
        public PaginatorDto(int page, int pages,int countItemPage, IEnumerable<TData> data , TStatusData typeData)
        {
            Page = page;
            Pages = pages;
            CountItemPage = countItemPage;
            Data = data;
            DataType = typeData;
        }
    }
}

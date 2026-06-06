using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ShopProjectDataBase.Helper;
using System.Text.Json.Serialization;

namespace ShopProjectWebServer.Api.Common
{
    public class PaginatorDto<TData, TStatusData>
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
    }
}

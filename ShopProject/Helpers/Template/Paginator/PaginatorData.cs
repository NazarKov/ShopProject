
using ShopProjectDataBase.Helper; 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ShopProject.Helpers.Template.Paginator
{
    public class PaginatorData<T>
    {
        [JsonPropertyName("page")]
        public int Page { get; set; }
        [JsonPropertyName("pages")]
        public int Pages { get; set; }
        [JsonPropertyName("data")]
        public IEnumerable<T>? Data { get; set; }
        public TypeStatusProduct DataType { get; set; }

        public PaginatorData() { }
        public PaginatorData(int page, int pages, IEnumerable<T> data)
        {
            Page = page;
            Pages = pages;
            Data = data;
        }
    }
}

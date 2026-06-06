using ShopProject.Model.Enum; 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ShopProject.Model.Domain.Paginator
{
    public class Paginator<TData , TDataType>
    { 
        public int Page { get; set; } 
        public int Pages { get; set; } 
        public IEnumerable<TData>? Data { get; set; }
        public TDataType? DataType { get; set; }

        public Paginator() { }
        public Paginator(int page, int pages, IEnumerable<TData> data , TDataType type)
        {
            Page = page;
            Pages = pages;
            Data = data;
            DataType = type;
        }
    }
}

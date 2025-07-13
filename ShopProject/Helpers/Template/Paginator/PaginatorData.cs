
using ShopProjectSQLDataBase.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Helpers.Template.Paginator
{
    public class PaginatorData<T>
    {
        public int Page { get; set; }
        public int Pages { get; set; }
        public List<T>? Data { get; set; }
        public TypeStatusProduct DataType { get; set; }

        public PaginatorData() { }
        public PaginatorData(int page, int pages, List<T> data)
        {
            Page = page;
            Pages = pages;
            Data = data;
        }
    }
}

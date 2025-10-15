using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ShopProjectDataBase.Helper;

namespace ShopProjectWebServer.Api.Common
{
    public class PaginatorDto<T>
    {
        public int Page { get; set; }
        public int Pages { get; set; }
        public IEnumerable<T>? Data { get; set; } 
        public PaginatorDto(int page, int pages, IEnumerable<T> data)
        {
            Page = page;
            Pages = pages;
            Data = data; 
        }

        public static PaginatorDto<T> CreationPaginator(IEnumerable<T> values , int page, int column)
        {

            double pages = 0;

            int countEnd = (int)(page * column);
            int countStart = (int)(countEnd - column);
            var data = values.OrderBy(i => i)
                                   .Skip(countStart)
                                   .Take((int)column);

            pages = values.Count() / column;
            int pagesCount = 0;
            if (!(pages % 2 == 0))
            {
                pagesCount = (int)pages;
                pagesCount++;
            }

            return new PaginatorDto<T>(page,pagesCount , data);
        }
    }
}

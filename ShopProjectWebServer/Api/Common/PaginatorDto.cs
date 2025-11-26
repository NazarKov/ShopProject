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
            var data = values.Skip(countStart).Take((int)column); 

            pages = values.Count() / column;
            int pagesCount = 0;
            if ((pages % 2 == 0))
            {
                pagesCount = (int)pages;
                pagesCount++;
            }
            else
            {
                pagesCount = (int)pages;
            }

            return new PaginatorDto<T>(page, pagesCount, data);
        } 

        public static PaginatorDto<T> CreationPaginator<T, TKey>(IEnumerable<T> values, int page, int column, Func<T, TKey> orderBySelector = null)
        {

            double pages = 0;

            int countEnd = (int)(page * column);
            int countStart = (int)(countEnd - column);

            IEnumerable<T> ordered = orderBySelector != null ? values.OrderBy(orderBySelector) : values;

            var data = ordered.Skip(countStart)
                              .Take((int)column);

            pages = values.Count() / column;
            int pagesCount = 0;
            if (!(pages % 2 == 0))
            {
                pagesCount = (int)pages;
                pagesCount++;
            }

            return new PaginatorDto<T>(page, pagesCount, data);
        } 
    }
}

using System.Text.Json.Serialization;

namespace ShopProjectWebServer.Models.Domain.Paginator
{
    public class Paginator<TData, TStatusData>
    { 
        public int Page { get; set; } 
        public int Pages { get; set; } 
        public int CountItemPage { get; set; } 
        public IEnumerable<TData>? Data { get; set; } 
        public TStatusData DataType { get; set; }

        public Paginator() { }
        public Paginator(int page, int pages, int countItemPage, IEnumerable<TData> data, TStatusData typeData)
        {
            Page = page;
            Pages = pages;
            CountItemPage = countItemPage;
            Data = data;
            DataType = typeData;
        }

        public static Paginator<TData, TStatusData> CreationPaginator(IEnumerable<TData> values, int page, int column, TStatusData dataType)
        {

            double pages = 0;

            int countEnd = (int)(page * column);
            int countStart = (int)(countEnd - column);
            var data = values.Skip(countStart).Take((int)column);

            pages = (double)values.Count() / (double)column;
            int pagesCount = 0;
            var surplus = pages - (int)pages;

            if (surplus > 0)
            {
                pagesCount = (int)pages;
                pagesCount++;
            }
            else
            {
                pagesCount = (int)pages;
            }

            return new Paginator<TData, TStatusData>(page, pagesCount, column, data, dataType);
        }

        public static Paginator<TData, TStatusData> CreationPaginator<TData, TKey>(IEnumerable<TData> values, int page, int column, TStatusData dataType, Func<TData, TKey> orderBySelector = null)
        {

            double pages = 0;

            int countEnd = (int)(page * column);
            int countStart = (int)(countEnd - column);

            IEnumerable<TData> ordered = orderBySelector != null ? values.OrderBy(orderBySelector) : values;

            var data = ordered.Skip(countStart)
                              .Take((int)column);

            pages = values.Count() / column;
            int pagesCount = 0;
            if (!(pages % 2 == 0))
            {
                pagesCount = (int)pages;
                pagesCount++;
            }

            return new Paginator<TData, TStatusData>(page, pagesCount, column, data, dataType);
        }
    }
}

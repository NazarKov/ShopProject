using ShopProjectSQLDataBase.Entities;
using System; 
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.UIModel
{
    public class UIOrderModel
    { 
        public int ID { get; set; }
        /// <summary>
        /// кількість товару
        /// </summary>
        public int Count { get; set; } = 0;
        /// <summary>
        /// товар
        /// </summary>
        public ProductEntity? Product { get; set; }
        /// <summary>
        /// операція до якої належить товар
        /// </summary>
        public UIOperationModel? Operation { get; set; }
    }
}

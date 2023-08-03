
using ShopProject.DataBase.Interfaces;
using ShopProject.DataBase.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ShopProject.Model.ToolsPage
{
    internal class UpdateProductAllModel
    {
        //private IEntityAccessor<Goods, TypeParameterSetTableProduct> _productRepository;

        public UpdateProductAllModel()
        {
           // _productRepository = new ProductTableRepository();
        }

        public bool UpdateProduct(List<Goods> list)
        {
            try
            {
                for(int i  = 0; i < list.Count;i++)
                {
                  //  _productRepository.Update(list[i]);
                }
                return true;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

    }
}

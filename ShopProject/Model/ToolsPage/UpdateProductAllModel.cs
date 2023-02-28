using NPOI.SS.UserModel;
using ShopProject.DataBase.Context;
using ShopProject.DataBase.Model;
using ShopProject.Interfaces.InterfacesRepository;
using ShopProject.Model.ModelRepository;
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
        private ITableRepository<Product, TypeParameterSetTableProduct> _productRepository;

        public UpdateProductAllModel()
        {
            _productRepository = new ProductTableRepository();
        }

        public bool UpdateProduct(List<Product> list)
        {
            try
            {
                for(int i  = 0; i < list.Count;i++)
                {
                    _productRepository.Update(list[i]);
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

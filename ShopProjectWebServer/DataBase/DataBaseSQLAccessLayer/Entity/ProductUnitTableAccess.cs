using ShopProjectDataBase.DataBase.Context;
using ShopProjectDataBase.DataBase.Model;
using ShopProjectWebServer.DataBase.Interface.EntityInterface;
using System.Data.Entity;

namespace ShopProjectWebServer.DataBase.DataBaseSQLAccessLayer.Entity
{
    public class ProductUnitTableAccess : IProductUnitTableAccess<ProductUnitEntity>
    {
        private string _connectionString;
        public ProductUnitTableAccess(string ConnectionString)
        {
            _connectionString = ConnectionString;
        }

        public void Add(ProductUnitEntity item)
        {
            throw new NotImplementedException();
        }

        public void Delete(ProductUnitEntity item)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ProductUnitEntity> GetAll()
        {
            using (ContextDataBase context = new ContextDataBase(_connectionString))
            {
                if (context != null)
                {
                    context.ProductUnits.Load(); 

                    if (context.ProductUnits.Count() != 0)
                    {
                        return context.ProductUnits.ToList();
                    }
                    else
                    {
                        return new List<ProductUnitEntity>();
                    }
                }
                return null;
            }
        }

        public void Update(ProductUnitEntity item)
        {
            throw new NotImplementedException();
        }
    }
}

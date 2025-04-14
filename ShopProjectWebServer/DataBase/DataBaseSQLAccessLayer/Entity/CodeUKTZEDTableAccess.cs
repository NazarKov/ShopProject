using ShopProjectDataBase.DataBase.Context;
using ShopProjectDataBase.DataBase.Model;
using ShopProjectWebServer.DataBase.Interface.EntityInterface;
using System.Data.Entity;

namespace ShopProjectWebServer.DataBase.DataBaseSQLAccessLayer.Entity
{
    public class CodeUKTZEDTableAccess : ICodeUKTZEDTableAccess<CodeUKTZEDEntity>
    {
        private string _connectionString;
        public CodeUKTZEDTableAccess(string ConnectionString)
        {
            _connectionString = ConnectionString;
        }

        public void Add(CodeUKTZEDEntity item)
        {
            throw new NotImplementedException();
        }

        public void Delete(CodeUKTZEDEntity item)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CodeUKTZEDEntity> GetAll()
        {
            using (ContextDataBase context = new ContextDataBase(_connectionString))
            {
                if (context != null)
                {
                    context.CodeUKTZED.Load();
 
                    if (context.CodeUKTZED.Count() != 0)
                    {
                        return context.CodeUKTZED.ToList();
                    }
                    else
                    {
                        return new List<CodeUKTZEDEntity>();
                    }
                }
                return null;
            }
        }

        public void Update(CodeUKTZEDEntity item)
        {
            throw new NotImplementedException();
        }
    }
}

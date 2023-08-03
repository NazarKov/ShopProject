using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.DataBase.Interfaces
{
    internal interface IDataAccess
    {
        public void Create();
        //public void CreateConnections(string nameDataBase);
        public void Clear();
        public void Delete();
    }
}

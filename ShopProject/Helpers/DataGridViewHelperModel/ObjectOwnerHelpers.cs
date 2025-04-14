using ShopProjectDataBase.DataBase.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Helpers.DataGridViewHelperModel
{
    public class ObjectOwnerHelpers
    {
        public ObjectOwnerEntity item { get; set; }
        public bool isActive { get; set; }

        public ObjectOwnerHelpers(ObjectOwnerEntity item)
        {
            this.item = item;
            isActive = true;
        }

    }
}

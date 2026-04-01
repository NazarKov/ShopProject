using ShopProject.Model.Domain.ObjectOwner;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Model.UI.ObjectOwner
{
    public class ObjectOwnerDialogWindowModel
    {
        public Domain.ObjectOwner.ObjectOwner ObjectOwner { get; set; }
        public bool isActive { get; set; }

        public ObjectOwnerDialogWindowModel(Domain.ObjectOwner.ObjectOwner objectOwner)
        {
            this.ObjectOwner = objectOwner;
            isActive = true;
        }
    }
}

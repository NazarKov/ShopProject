using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.UIModel.ObjectOwnerPage
{
    public class ObjectOwnerDialogWindow
    {
        public ObjectOwner item { get; set; }
        public bool isActive { get; set; }

        public ObjectOwnerDialogWindow(ObjectOwner item)
        {
            this.item = item;
            isActive = true;
        }
    }
}

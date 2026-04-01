using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Services.Modules.Resourse.Interface
{
    internal interface IResourseService
    {
        public bool IsInitSystemFolders();
        public Task LoadSessionResourse();
    }
}

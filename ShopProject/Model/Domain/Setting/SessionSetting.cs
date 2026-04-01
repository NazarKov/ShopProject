using ShopProject.Model.Domain.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Model.Domain.Setting
{
    internal class SessionSetting
    {
        public Model.Domain.User.User User { get; set; } = new Model.Domain.User.User();
    }
}

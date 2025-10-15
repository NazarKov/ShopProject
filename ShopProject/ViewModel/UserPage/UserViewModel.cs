using ShopProject.Model.UserPage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.ViewModel.UserPage
{
    internal class UserViewModel :ViewModel<UserViewModel>
    {
        private UserModel _model;

        public UserViewModel()
        {
            _model = new UserModel();
        }
    }
}

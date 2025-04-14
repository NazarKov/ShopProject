using ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ShopProject.Model.HomePage
{
    internal class HomeModel
    {
        public HomeModel(){}

        public void Init()
        {
            Task task = Task.Run(() =>
            {
                MainWebServerController.Init();
            });
        }
    } 
}

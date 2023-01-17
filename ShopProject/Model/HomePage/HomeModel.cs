using ShopProject.DataBase.Context;
using ShopProject.DataBase.Model;
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
        ShopContext db;
        List<User> sellers;

        public HomeModel ()
        {
            db = new ShopContext();
            sellers = new List<User>();
        }

        public string GetName(int i )
        {
            LoadDb();
            return sellers.ElementAt(i).name;
        }

        void LoadDb()
        {
            db = new ShopContext();
            db.Database.BeginTransaction();
            db.user.Load();
            sellers = new List<User>();
            sellers.AddRange(db.user.Local.ToList());
        }
        

        public string Authorization(string name,string password)
        {
            LoadDb();

            for (int i = 0, max = sellers.Count; i < max; i++)
            {
                if(name==null)
                {
                    MessageBox.Show("Ведіть імя","Eror",MessageBoxButton.OK,MessageBoxImage.Information);
                }
                else
                {
                    if(password==null)
                    {
                        MessageBox.Show("Ведіть пароль", "Eror", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        if (name.Equals(sellers.ElementAt(i).name))
                        {
                            if (password.Equals(sellers.ElementAt(i).password))
                            {
                                return sellers.ElementAt(i).name;
                            }
                            else
                            {
                                MessageBox.Show("Невірний пароль", "Eror", MessageBoxButton.OK, MessageBoxImage.Information);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Невірне імя", "Eror", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                    }
                }
            }

            return "0";
        }
    }
}

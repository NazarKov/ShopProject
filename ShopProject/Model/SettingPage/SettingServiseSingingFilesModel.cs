using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ShopProject.Model.SettingPage
{
    internal class SettingServiseSingingFilesModel
    {
        //private MainContollerTcp _mainController;
        public SettingServiseSingingFilesModel()
        {
            //_mainController = new MainContollerTcp();
        }

        public void StartServise()
        {
            try
            {
                //_mainController.StartServise();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void ConnectServise()
        {
            try
            {
                //_mainController.ConnectService();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //public UserCommand? SendCommand(TypeCommand type)
        //{
        //    try
        //    {
        //        return _mainController.SendingCommand(new UserCommand() { TypeCommand = type , Time = DateTime.Now });
        //    }
        //    catch(Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //        return null;
        //    }
        //}



    }
}

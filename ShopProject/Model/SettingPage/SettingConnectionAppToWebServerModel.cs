using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ShopProject.Model.SettingPage
{
    internal class SettingConnectionAppToWebServerModel
    {
       // private SettingWebServerApiController _controller;


        public SettingConnectionAppToWebServerModel()
        {
            //_controller = HttpDataBaseAPIController.SettingWebServerApi;
        }


        //public int GetPort()
        //{
        //   / return NetworkScanner.Port;
        //}
        //public string GetApiRouter()
        //{
        //    return NetworkScanner.IpRouter;
        //}
        //public async Task<string> FindIpServer(string ipRouter, int port)
        //{
        //    try
        //    {
        //        NetworkScanner.IpRouter = ipRouter;
        //        NetworkScanner.Port = port;

        //        return await NetworkScanner.SearchDataBaseURLAsync();
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //        return string.Empty;
        //    }
        //}

        //public async Task<string> Ping()
        //{
        //    try
        //    {
        //        //if (_controller == null)
        //        //{
        //        //    throw new Exception("Немає доступу до серверу виконайте перепідключення до серверу");
        //        //}
        //        //return await _controller.Ping();
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //        return string.Empty;
        //    }
        //}

        public void Reconnect(string url)
        {
            //HttpDataBaseAPIController.Init(url);
            //_controller = HttpDataBaseAPIController.SettingWebServerApi;
        }

        public async Task ConnectUser(string login, string password, string adminPassword)
        {
            try
            {
                //if (_controller == null)
                //{
                    throw new Exception("Перевірте підключення до серверу");
                //}
              //  await _controller.ConnectServerUser(login, password, adminPassword);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //public async Task<UserSettingServer> GetUser(string token)
        //{
        //    try
        //    {
        //        if (_controller != null)
        //        {
        //            var user = await _controller.GetUserInfo(token);
        //            return user;
        //        }
        //        return null;
        //    }
        //    catch(Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //        return null;
        //    }
        //}
        //public async Task<List<UserSettingServer>> GetUsers(string token)
        //{
        //    try
        //    {
        //        var listUser = await _controller.GetAllUserInfo(token);
        //        //List<UserSettingServer> result = new List<UserSettingServer>();
        //        foreach (var user in listUser)
        //        {
        //            if (user.Token != null)
        //            {
        //            //    result.Add(user);
        //            }
        //        }
        //      //  return result;
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //       // return new List<UserSettingServer>();
        //    }
        //}

        public async Task DisconnectUser(string token)
        {
            try
            {
               // await _controller.DisconnectServerUser(token);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}

﻿using ShopProject.Helpers;
using ShopProject.Helpers.DataGridViewHelperModel;
using ShopProject.Helpers.NetworkServise.ElectronicTaxAccountPublicApi;
using ShopProject.Helpers.NetworkServise.ElectronicTaxAccountPublicApi.Model;
using ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi;
using ShopProjectDataBase.DataBase.Entities;
using SigningFileLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using ZXing;

namespace ShopProject.Model.AdminPage
{
    internal class ObjectOwnerShipModel
    {
        private SigningFileContoller _signingFileController;
        private MainElectronicTaxAccountController _accountController;

        private List<ObjectOwnerEntity> _objectOwnerList;

        public ObjectOwnerShipModel()
        {
            _objectOwnerList = new List<ObjectOwnerEntity>();

            _signingFileController = new SigningFileContoller();
            _accountController = new MainElectronicTaxAccountController();
            _signingFileController.Initialize(false);

        }


        public List<ObjectOwnerEntity> GetAll() 
        {
            Task t = Task.Run(async () =>
            {
                _objectOwnerList = (await MainWebServerController.MainDataBaseConntroller.ObjectOwnerController.GetObjectsOwners(Session.Token)).ToList();
            });
            t.Wait();
            return _objectOwnerList;
        }
        public List<ObjectOwnerEntity> SearchObject(string item)
        {
            try
            {
                var items = GetAll();
                if (items != null)
                {
                    if (item != " ")
                    {
                        return items.Where(i => i.NameObject.ToLower().Contains(item.ToLower())).ToList();
                    }
                }
                return new List<ObjectOwnerEntity>();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return new List<ObjectOwnerEntity>();
            }
        }

        public async Task<bool> GetServerObjectOwner(string pathFile, string passwordKey)
        {
            try
            {

                _objectOwnerList = new List<ObjectOwnerEntity>();
                if (_signingFileController.GetDataKey(pathFile, passwordKey))
                {
                   
                    DataJsonHttpResponse data = new DataJsonHttpResponse();
                    var response = await _accountController.Send();

                    List<DataJsonHttpResponse> infoUser = DataJsonHttpResponse.FromJsonList(response);

                    var nameUser = infoUser.ElementAt(0).values.FULL_NAME;

                    foreach (var item in infoUser.ElementAt(8).listValues)
                    {

                        ObjectOwnerEntity objectOwner = new ObjectOwnerEntity()
                        {
                            NameObject = item.NAME,
                            Status = item.STAN_OBJECT,
                            Address = item.ADDRESS,
                            C_DISTR = item.C_DISTR,
                            TypeOfRights = item.TYPE_OF_RIGHTS,
                            C_TERRIT = item.C_TERRIT.ToString(),
                            KATOTTG = item.KATOTTG,
                            REG_NUM_OBJ = item.REG_NUM_OBJ,
                            CodeObject = item.TO_CODE.ToString(),
                            TypeObjectName = item.TYPE_OF_RIGHTS.ToString(),

                        };

                        var time = item.D_ACC_START;
                        if (time != null)
                        {
                            objectOwner.D_ACC_START = DateTime.Parse(item.D_ACC_START);
                        }
                        time = item.D_LAST_CH;
                        if (time != null)
                        {
                            objectOwner.D_LAST_CH = DateTime.Parse(item.D_LAST_CH);
                        }
                        time = item.D_ACC_END;
                        if (time != null)
                        {
                            objectOwner.D_ACC_END = DateTime.Parse(item.D_ACC_END);
                        }



                        _objectOwnerList.Add(objectOwner);
                    }

                 
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }


        }

        public bool SaveDataBaseItem(List<ObjectOwnerHelpers> objectOwnerHelpers)
        {
            try
            {

                List<ObjectOwnerEntity> result = new List<ObjectOwnerEntity>();
                for (int i = 0; i < objectOwnerHelpers.Count; i++)
                {
                    if (objectOwnerHelpers.ElementAt(i).isActive)
                    {
                        result.Add(objectOwnerHelpers.ElementAt(i).item);
                    }

                }
                bool response = false;
                Task t = Task.Run(async () =>
                {
                    response = await MainWebServerController.MainDataBaseConntroller.ObjectOwnerController.AddObjectsOwners(Session.Token, result);
                });

                t.Wait();
                return response;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }

        }

        public List<ObjectOwnerEntity> GetListObjecyOwner()
        {
            return _objectOwnerList;
        }
        public bool deleteItemDataBase(ObjectOwnerEntity item)
        {
            try
            {
                //_objectTable.Delete(item);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }
    }
}

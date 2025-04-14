using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ShopProject.Model.StatisticsPage
{
    internal class StatisticsModel
    {
        //private IEntityAccess<UserEntiti> _userTable;
        //private IEntityAccess<OperationsRecorderEntiti> _operationsRecorderTable;
        //private IEntityAccess<OperationEntiti> _operationsTable;

        //public StatisticsModel() 
        //{

        //    _userTable = new UserTableAccess();
        //    _operationsRecorderTable = new OperationsRecorderTableAccess();
        //    _operationsTable = new OperationTableAccess();
        //}

        //public List<UserEntiti> GetUserAll ()
        //{
        //    try
        //    {
        //        var users = _userTable.GetAll().ToList();
        //        if(users!=null)
        //        {
        //            return users;
        //        }
        //        throw new Exception("Невдалося завантажити користувачі");
        //    }
        //    catch(Exception ex) 
        //    {
        //        MessageBox.Show(ex.Message);
        //        return new List<UserEntiti>();
        //    }
        //}

        //public List<OperationsRecorderEntiti> GetOperationsRecorderAll()
        //{
        //    try
        //    {
        //        var operationRecorder = _operationsRecorderTable.GetAll().ToList();
        //        if (operationRecorder != null)
        //        {
        //            return operationRecorder;
        //        }
        //        throw new Exception("Невдалося завантажити розрахункові пристрої");
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //        return new List<OperationsRecorderEntiti>();
        //    }
        //}

        //public List<OperationEntiti> GetOperaionAll()
        //{
        //    try
        //    {
        //        var operations = _operationsTable.GetAll().Where(item => item.TypeOperation==113).ToList();
        //        if (operations != null)
        //        {
        //            return operations;
        //        }
        //        throw new Exception("Невдалося завантажити дані");
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //        return new List<OperationEntiti>();
        //    }
        //}

        //public List<OperationEntiti> Search(DateTime createAt , DateTime finishedAt, UserEntiti user , OperationsRecorderEntiti operationsRecorder)
        //{

        //    List<OperationEntiti> result = new List<OperationEntiti>();
        //    try
        //    {
        //        var operations = _operationsTable.GetAll().Where(item => item.TypeOperation == 113).ToList();
        //        if (operations != null)
        //        {
        //            foreach (var operation in operations)
        //            {
        //                if(operation.CreatedAt.Date >= createAt.Date && operation.CreatedAt.Date <= finishedAt.Date)
        //                {
        //                    if (operation.User!=null && operation.User.ID== user.ID)
        //                    {
        //                        if (operation.FiscalNumberRRO == operationsRecorder.FiscalNumber)
        //                        {
        //                            result.Add(operation);
        //                        }

        //                    }
        //                }
        //            }
        //        }
        //        return result;
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //        return new List<OperationEntiti>();
        //    }
        //}
    }
}

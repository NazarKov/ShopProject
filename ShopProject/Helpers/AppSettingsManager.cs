using MathNet.Numerics;
using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static NPOI.SS.Formula.PTG.ArrayPtg;

namespace ShopProject.Helpers
{
    public enum TypeConnections
    {
        DEVELOPER = 1,
        EXPRESS = 2,
    }

    internal static class AppSettingsManager
    {
        public static void SetConnectionDataBase(string name, TypeConnections type)
        {
            switch (type)
            {
                case TypeConnections.DEVELOPER:
                    {
                        Properties.Settings.Default.ConnectionString = "Data Source=" + Environment.MachineName + ";Initial Catalog=" + name + ";Integrated Security=True";
                        Properties.Settings.Default.Save();
                        break;
                    }
                case TypeConnections.EXPRESS:
                    {
                        Properties.Settings.Default.ConnectionString = "Data Source=" + Environment.MachineName + "\\SQLEXPRESS;Initial Catalog=" + name + ";Integrated Security=True";
                        Properties.Settings.Default.Save();
                        break;
                    }
            }
        }
        public static void SetParameterFile(string key, string value)
        {
            Properties.Settings.Default[key] = value;
            Properties.Settings.Default.Save();
        }
        public static void SetParameterFile(string key, bool value)
        {
            Properties.Settings.Default[key] = value;
            Properties.Settings.Default.Save();
        }
        public static object GetParameterFiles(string key)
        {
            return Properties.Settings.Default[key];
        }
    }
}

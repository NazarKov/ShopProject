using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Model
{
    public class ResourceModel
    {
       
        public string? NameDb { get; set; }
        public string? DescriptionDb { get; set; }

        public ResourceModel(){}

        public void SetNameDb(string name)
        {
            this.NameDb = name;
        }
        public string? GetNameDb()
        {
             return NameDb;
        }

        public void SetDescriptionDb(string description)
        {
            this.DescriptionDb = description;
        }

        public string? GetDescriptionDb()
        {
            return DescriptionDb;
        }

   

    }
}

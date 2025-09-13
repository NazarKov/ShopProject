using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProjectDataBase.Context
{
    public class ContextDataBaseFactory : IDesignTimeDbContextFactory<ContextDataBase>
    {
        public ContextDataBase CreateDbContext(string[] args)
        {
             
            var optionsBuilder = new DbContextOptionsBuilder<ContextDataBase>();
            optionsBuilder.UseSqlServer("Server = localhost; Database = nazar; User Id = ShopAdmin; Password = Admin; TrustServerCertificate = True;");

            return new ContextDataBase(optionsBuilder.Options);
        }
    }
}

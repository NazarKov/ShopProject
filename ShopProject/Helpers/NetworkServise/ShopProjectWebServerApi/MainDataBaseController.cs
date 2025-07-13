using ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi.Controller;
using ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi.DataBaseController;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi
{
    internal class MainDataBaseController
    {
        private string _url;

        public UserController UserController { get; set; }
        public ProductController ProductController { get; set; }
        public ProductUnitController ProductUnitController { get; set; }
        public CodeUKTZEDController CodeUKTZEDController { get; set; }
        public UserRoleController UserRoleController { get; set; }
        public ObjectOwnerController ObjectOwnerController { get; set; }
        public OperationRecorederController OperationRecorederController { get; set; }
        public OperationRecorderAndUserController OperationRecorderAndUserController { get; set; }
        public OperationController OperationController { get; set; }
        public OrderController OrderController { get; set; }

        public MainDataBaseController(string url)
        {
            _url = url;
            UserController = new UserController(url);
            ProductController = new ProductController(url);
            ProductUnitController = new ProductUnitController(url);
            CodeUKTZEDController = new CodeUKTZEDController(url);
            UserRoleController = new UserRoleController(url);
            ObjectOwnerController = new ObjectOwnerController(url);
            OperationRecorederController = new OperationRecorederController(url);
            OperationRecorderAndUserController = new OperationRecorderAndUserController(url);
            OperationController = new OperationController(url);
            OrderController = new OrderController(url);
        }
    }
}

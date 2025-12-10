using ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi.Controller;
using ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi.Controller.DataBaseController;
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
        public UserController UserController { get; set; }
        public ProductController ProductController { get; set; }
        public ProductUnitController ProductUnitController { get; set; }
        public ProductCodeUKTZEDController ProductCodeUKTZEDController { get; set; }
        public UserRoleController UserRoleController { get; set; }
        public ObjectOwnerController ObjectOwnerController { get; set; }
        public OperationRecorderController OperationRecorederController { get; set; }
        public OperationRecorderAndUserController OperationRecorderAndUserController { get; set; }
        public OperationController OperationController { get; set; }
        public OrderController OrderController { get; set; }
        public MediaAccessControlController MediaAccessControlController { get; set; }
        public WorkingShiftContoller WorkingShiftContoller { get; set; }
        public SignatureKeyController SignatureKeyController { get; set; }
        public DiscountController DiscountController { get; set; }
        public MainDataBaseController(string url)
        { 
            UserController = new UserController(url);
            ProductController = new ProductController(url);
            ProductUnitController = new ProductUnitController(url);
            ProductCodeUKTZEDController = new ProductCodeUKTZEDController(url);
            UserRoleController = new UserRoleController(url);
            ObjectOwnerController = new ObjectOwnerController(url);
            OperationRecorederController = new OperationRecorderController(url);
            OperationRecorderAndUserController = new OperationRecorderAndUserController(url);
            OperationController = new OperationController(url);
            OrderController = new OrderController(url);
            MediaAccessControlController = new MediaAccessControlController(url);
            WorkingShiftContoller = new WorkingShiftContoller(url);
            SignatureKeyController = new SignatureKeyController(url);
            DiscountController = new DiscountController(url);
        }
    }
}

using ShopProject.Services.Integration.Network.ShopProjectWebServerApi.Controller.DataBaseController;
using ShopProject.Services.Integration.Network.ShopProjectWebServerApi.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Services.Integration.Network.ShopProjectWebServerApi
{
    public class MainDataBaseFacade : IMainDataBaseFacade
    { 
        public UserController UserController { get; private set; }
        public ProductController ProductController { get; private set; }
        public ProductUnitController ProductUnitController { get; private set; }
        public ProductCodeUKTZEDController ProductCodeUKTZEDController { get; private set; }
        public UserRoleController UserRoleController { get; private set; }
        public ObjectOwnerController ObjectOwnerController { get; private set; }
        public OperationRecorderController OperationRecorederController { get; private  set; }
        public OperationRecorderAndUserController OperationRecorderAndUserController { get; private set; }
        public OperationController OperationController { get; private set; }
        public OrderController OrderController { get; private set; }
        public MediaAccessControlController MediaAccessControlController { get; private set; }
        public WorkingShiftContoller WorkingShiftContoller { get; private set; }
        public SignatureKeyController SignatureKeyController { get; private set; }
        public DiscountController DiscountController { get; private set; } 
        public GiftCertificatesController GiftCertificatesController { get; private set; }
        public MainDataBaseFacade(string url)
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
            GiftCertificatesController = new GiftCertificatesController(url);
        }
    }
}

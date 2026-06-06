using ShopProject.Services.Integration.Network.WebServerApi.Controller.DataBaseController;
using ShopProject.Services.Integration.Network.WebServerApi.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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
        public MainDataBaseFacade(HttpClient client)
        { 
            UserController = new UserController(client);
            ProductController = new ProductController(client);
            ProductUnitController = new ProductUnitController(client);
            ProductCodeUKTZEDController = new ProductCodeUKTZEDController(client);
            UserRoleController = new UserRoleController(client);
            ObjectOwnerController = new ObjectOwnerController(client);
            OperationRecorederController = new OperationRecorderController(client);
            OperationRecorderAndUserController = new OperationRecorderAndUserController(client);
            OperationController = new OperationController(client);
            OrderController = new OrderController(client);
            MediaAccessControlController = new MediaAccessControlController(client);
            WorkingShiftContoller = new WorkingShiftContoller(client);
            SignatureKeyController = new SignatureKeyController(client);
            DiscountController = new DiscountController(client);
            GiftCertificatesController = new GiftCertificatesController(client);
        }
    }
}

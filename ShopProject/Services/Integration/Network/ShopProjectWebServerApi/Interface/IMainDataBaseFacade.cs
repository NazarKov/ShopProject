using ShopProject.Services.Integration.Network.ShopProjectWebServerApi.Controller.DataBaseController;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Services.Integration.Network.ShopProjectWebServerApi.Interface
{
    public interface IMainDataBaseFacade
    {
        public UserController UserController { get;}
        public ProductController ProductController { get; }
        public ProductUnitController ProductUnitController { get; }
        public ProductCodeUKTZEDController ProductCodeUKTZEDController { get;  }
        public UserRoleController UserRoleController { get;}
        public ObjectOwnerController ObjectOwnerController { get; }
        public OperationRecorderController OperationRecorederController { get; }
        public OperationRecorderAndUserController OperationRecorderAndUserController { get; }
        public OperationController OperationController { get; }
        public OrderController OrderController { get; }
        public MediaAccessControlController MediaAccessControlController { get; }
        public WorkingShiftContoller WorkingShiftContoller { get; }
        public SignatureKeyController SignatureKeyController { get; }
        public DiscountController DiscountController { get; }
        public GiftCertificatesController GiftCertificatesController { get; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Model.Navigation
{
    public enum NavigationButton
    {
        None, 
        RedirectToChangePassword, 
        ReloadProduct,
        ReloadUser,
        ReloadTaxObject,
        ReloadOperationRecroder,
        CountingSumaOrder,
        RemoveProduct,
        ReloadGiftCertificates,
        RedirectToAssignedPointsOfSalePage,

        RedirectToWorkShiftMenuPage,
        RedirectToOperationsRecorderPage,
        RedirectToDashBoadPage,
        RedirectToAuthorizationPage,
        RedirectToAuthorizationPageWhenErrorUnAuthorization,
        RedirectServerSelectionPage,
        RedirectStartPage,
        RedirectToRegisterWindwoServicePage,
        ExitApp, 
    }
}

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
        RedirectToAuthorizationView, 
        RedirectToChangePassword,
        RedirectToTitleView,
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
        RedirectServerSelectionPage,
        RedirectStartPage,
        RedirectToRegisterWindwoServicePage,
        ExitApp, 
    }
}

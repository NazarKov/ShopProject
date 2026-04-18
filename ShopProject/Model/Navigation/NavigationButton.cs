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
        CountingSumaOrder,
        RemoveProduct,
        ReloadGiftCertificates,


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

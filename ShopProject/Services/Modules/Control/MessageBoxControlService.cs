using ShopProject.Controls.MessegeBox;
using ShopProject.Controls.MessegeBox.Enum;
using ShopProject.Services.Infrastructure.Mediator;
using ShopProject.Services.Modules.Control.Interface;
using System;
using System.Threading.Tasks;
using System.Windows;

namespace ShopProject.Services.Modules.Control
{
    internal class MessageBoxControlService : IMessageBoxControlService
    {
        private readonly ResourceDictionary _resources;

        public MessageBoxControlService()
        {
            _resources = new ResourceDictionary
            {
                Source = new Uri(
                "..\\..\\..\\Resource\\Theme\\LightTheme\\ButtonStyle.xaml",
                UriKind.Relative)
            };
        }

        public async Task<bool> Show(string message,string title = "Повідомлення", MessageBoxType type = MessageBoxType.Information,string shadowPage ="")
        {
            if (!string.IsNullOrEmpty(shadowPage))
            {
                await MediatorService.ExecuteEventAsync("VisibilitiShadowSetVisible");
                await MediatorService.ExecuteEventAsync(shadowPage + "SetVissible");
            }
            var result = false;

            var cancelbutton = _resources["ExitButton"] as Style;
            switch (type) 
            {
                case MessageBoxType.Information:
                    {
                        var style = _resources["GeneralButton"] as Style;
                        result = MessegeBoxView.Show(message, style, cancelbutton, title, type);
                        break;
                    }
                case MessageBoxType.Success:
                    {
                        var style = _resources["SuccessButton"] as Style;
                        result = MessegeBoxView.Show(message, style, cancelbutton, title, type);
                        break;
                    }
                case MessageBoxType.Question:
                    {
                        var style = _resources["GeneralButton"] as Style;
                        result = MessegeBoxView.Show(message, style, cancelbutton, title, type);
                        break;
                    }
                case MessageBoxType.Warning:
                    {
                        var style = _resources["WarningButton"] as Style;
                        result = MessegeBoxView.Show(message, style, cancelbutton, title, type);
                        break;
                    }
                case MessageBoxType.Error:
                    {
                        result = MessegeBoxView.Show(message, cancelbutton, cancelbutton, title, type);
                        break;
                    }
            }

            if (!string.IsNullOrEmpty(shadowPage))
            {
                await MediatorService.ExecuteEventAsync("VisibilitiShadowSetHidden");
                await MediatorService.ExecuteEventAsync(shadowPage + "SetCollapsed");
            }

            return result;
        }
    }
}
